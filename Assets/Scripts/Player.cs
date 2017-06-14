﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	public GameObject bubblePrefab;
	public Toggle toggleMovement;
	public Toggle toggleTrail;

	public float startingLifeAmount = 100.0f;
	public float movementSpeed = 5;
	public float smoothTime = 0.3f;
	public float bubbleFollowSpeed = .5f;
	
  GameObject lastBubble;
	GameObject gameOverText;

	Button moveLeftBtn;
	Button moveRightBtn;

	Camera mainCamera;

  List<GameObject> currentBubbles;
  List<Bubble> currentBubbleConfigs;

  bool freeMovement = false;
  bool trailEnabled = true;
  bool mouseDrag = false;
  bool moveLeft = false;
  bool moveRight = false;
  bool moveDelta = false;

	Vector3 velocity = Vector3.zero;
  Vector3 deltaMovement;

  Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = mainCamera.WorldToViewportPoint(vector);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		pos.z = 0;

		Vector3 worldPos = mainCamera.ViewportToWorldPoint(pos);
		worldPos.z = 0;

  	return worldPos;

  }

  void OnSwipeEvent(SwipeEvent e) {

		float xVelocity = -e.velocity;

		if(e.dir == TKSwipeDirection.Right)
			xVelocity = -xVelocity;

		deltaMovement = ClampToScreen(transform.TransformPoint(new Vector3(xVelocity * .5f, 0, 0)));

  }

	void BubbleHitEvent(HitEvent e) {

  	SpawnHit(e.collider, e.bubble);

  }

  void SpawnHit(Collider collider, GameObject bubble=null) {

  	if(collider.gameObject.GetComponent<PowerUpObject>() != null)
  		return;

  	if(collider.gameObject.GetComponent<SpawnObject>().isEnemy) {

  		if(currentBubbles.Count == 0) {
  			gameObject.SetActive(false);
  			gameOverText.SetActive(true);

  			return;
  		}

  		if(bubble != null) {

	  		int indBubble = currentBubbles.IndexOf(bubble.gameObject);
	  		List<GameObject> bubblesRemove = currentBubbles.GetRange(indBubble, currentBubbles.Count-indBubble);

		  	foreach(GameObject thisBubble in bubblesRemove) {
		  		currentBubbles.Remove(thisBubble);
		  		currentBubbleConfigs.Remove(thisBubble.GetComponent<Bubble>());

		  		Destroy(thisBubble);
		  	}

	  		if(currentBubbles.Count > 0)
		  		lastBubble = currentBubbles[currentBubbles.Count-1];
		  	
		  	if(bubbleFollowSpeed+.05f <= 1)
					bubbleFollowSpeed += .05f;
			
			}
  	
  	}

		else {
		
			Transform target = (lastBubble != null) ? lastBubble.transform : transform;
			
			lastBubble = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
			lastBubble.GetComponent<Bubble>().target = target;

			currentBubbles.Add(lastBubble);
			currentBubbleConfigs.Add(lastBubble.GetComponent<Bubble>());

			bubbleFollowSpeed = Mathf.Clamp(bubbleFollowSpeed-.05f, .1f, .5f);
		
		}

		foreach(Bubble config in currentBubbleConfigs) 
			config.speed = bubbleFollowSpeed;

		Destroy(collider.gameObject);

  }

  void MovementToggle(bool value) {

  	freeMovement = value;

		moveLeftBtn.gameObject.SetActive(!value);
		moveRightBtn.gameObject.SetActive(!value);

		if(!freeMovement) {
	    Vector3 lockedPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, 250, transform.position.z));
	    lockedPos.z = 0;

	    transform.position = lockedPos;
	  }

  }

  void TrailToggle(bool value) {

  	trailEnabled = value;

  	foreach(GameObject bubble in currentBubbles)
  		bubble.SetActive(!trailEnabled);

  }

	void OnMovementEvent (MovementEvent e) {

  	mouseDrag = false;

		if(e.Direction == "left")
			moveLeft = !e.EndClick;
		else
			moveRight = !e.EndClick;
	
	}

	void Awake () {

		deltaMovement = transform.position;
		mainCamera = Camera.main;

		Events.instance.AddListener<MovementEvent> (OnMovementEvent);
		Events.instance.AddListener<HitEvent> (BubbleHitEvent);
		Events.instance.AddListener<SwipeEvent> (OnSwipeEvent);
	
	}

	// Use this for initialization
	void Start () {

		currentBubbles = new List<GameObject>();
		currentBubbleConfigs = new List<Bubble>();

		moveLeftBtn = GameObject.Find("MoveLeft").GetComponent<Button>();
		// moveLeftBtn.gameObject.SetActive(false);

		moveRightBtn = GameObject.Find("MoveRight").GetComponent<Button>();
		// moveRightBtn.gameObject.SetActive(false);

		gameOverText = GameObject.Find("GameOver");
		gameOverText.SetActive(false);

		toggleMovement.onValueChanged.AddListener(MovementToggle);
		toggleTrail.onValueChanged.AddListener(TrailToggle);
		
	}

	void Update() {

  	Vector3 targetPosition = transform.TransformPoint(new Vector3((moveLeft ? -movementSpeed : movementSpeed), 0, 0));

		if(moveLeft || moveRight) {
			transform.position = ClampToScreen(Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime));
			deltaMovement = transform.position;
		}
	  else if(!freeMovement && !mouseDrag)
	  	transform.position = Vector3.SmoothDamp(transform.position, deltaMovement, ref velocity, 0.2f);

	}
	
  void OnMouseDown() {

  	mouseDrag = true;

  }
	
  void OnMouseUp() {

  }
      
  void OnMouseDrag() {

    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, freeMovement ? Input.mousePosition.y : 250, 0);
    Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint);

    transform.position = ClampToScreen(cursorPosition);

  }

	void OnTriggerEnter(Collider collider)
  {

  	SpawnHit(collider);

  }
  
}
