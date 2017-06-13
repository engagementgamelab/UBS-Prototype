using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
	public GameObject bubblePrefab;
	public Toggle toggleMovement;
	public float startingLifeAmount = 100.0f;

	public float movementSpeed = 5;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
  
  Vector3 screenPoint;
  Vector3 offset;

  GameObject lastBubble;
	GameObject gameOverText;

	Button moveLeftBtn;
	Button moveRightBtn;

  List<GameObject> currentBubbles;

  bool freeMovement = false;
  bool mouseDrag = false;
  bool moveLeft = false;
  bool moveRight = false;
  bool moveDelta = false;

  Vector3 deltaMovement;

  Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = Camera.main.WorldToViewportPoint(vector);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		pos.z = screenPoint.z;

  	return Camera.main.ViewportToWorldPoint(pos);

  }

  void OnSwipeEvent(SwipeEvent e) {

		float xVelocity = -e.velocity;

		if(e.dir == TKSwipeDirection.Right)
			xVelocity = -xVelocity;

		deltaMovement = ClampToScreen(transform.TransformPoint(new Vector3(xVelocity * .5f, 0, 0)));

  }

	void BubbleHitEvent(HitEvent e) {

  	SpawnHit(e.collider);

  }

  void SpawnHit(Collider collider) {

  	if(collider.gameObject.GetComponent<SpawnObject>().IsEnemy) {

  		if(currentBubbles.Count == 0) {
  			gameObject.SetActive(false);
  			gameOverText.SetActive(true);

  			return;
  		}

  		Destroy(lastBubble);
  		currentBubbles.RemoveAt(currentBubbles.Count-1);

  		if(currentBubbles.Count > 0)
	  		lastBubble = currentBubbles[currentBubbles.Count-1];
	  		
  	}

		else {
			Transform target = (lastBubble != null) ? lastBubble.transform : transform;
			
			lastBubble = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
			lastBubble.GetComponent<Bubble>().target = target;
			currentBubbles.Add(lastBubble);
		}

		Destroy(collider.gameObject);

  }

  void MovementToggle(bool value) {

  	freeMovement = value;

		moveLeftBtn.gameObject.SetActive(!value);
		moveRightBtn.gameObject.SetActive(!value);

		if(!freeMovement) {
	    Vector3 lockedPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, 100, transform.position.z));
	    lockedPos.z = 0;

	    transform.position = lockedPos;
	  }

  }

	void OnMovementEvent (MovementEvent e) {

  	mouseDrag = false;

		if(e.Direction == "left")
			moveLeft = !e.EndClick;
		else
			moveRight = !e.EndClick;
	
	}

	void Awake () {

		Events.instance.AddListener<MovementEvent> (OnMovementEvent);
		Events.instance.AddListener<HitEvent> (BubbleHitEvent);
		Events.instance.AddListener<SwipeEvent> (OnSwipeEvent);
	
	}

	// Use this for initialization
	void Start () {

		currentBubbles = new List<GameObject>();

		moveLeftBtn = GameObject.Find("MoveLeft").GetComponent<Button>();
		// moveLeftBtn.gameObject.SetActive(false);

		moveRightBtn = GameObject.Find("MoveRight").GetComponent<Button>();
		// moveRightBtn.gameObject.SetActive(false);

		gameOverText = GameObject.Find("GameOver");
		gameOverText.SetActive(false);

		toggleMovement.onValueChanged.AddListener(MovementToggle);
		
	}

	void Update() {

  	Vector3 targetPosition = transform.TransformPoint(new Vector3((moveLeft ? -movementSpeed : movementSpeed), 0, 0));

		if(moveLeft || moveRight) {
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
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

    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, freeMovement ? Input.mousePosition.y : 100, screenPoint.z);
    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

    transform.position = ClampToScreen(cursorPosition);

  	// Debug.Log(transform.position.x);

  }

	void OnTriggerEnter(Collider collider)
  {

  	SpawnHit(collider);

  }
}
