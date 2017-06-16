using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	public GameObject bubblePrefab;
	public Toggle toggleMovement;
	public Toggle toggleTrail;

	public Text scoreText;

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

  bool freeMovement = true;
  bool trailEnabled = true;
  bool mouseDrag = false;
  bool moveLeft = false;
  bool moveRight = false;
  bool moveDelta = false;

	Vector3 velocity = Vector3.zero;
  Vector3 deltaMovement;

  float currentScore;
  float targetScore;

  Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = mainCamera.WorldToViewportPoint(vector);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		pos.z = 0;

		Vector3 worldPos = mainCamera.ViewportToWorldPoint(pos);
		worldPos.z = 0;

  	return worldPos;

  }

  void AddBubble() {
		
		Transform target = (currentBubbleConfigs.Count > 0) ? currentBubbleConfigs[currentBubbleConfigs.Count-1].transform : transform;
		
		lastBubble = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
		lastBubble.GetComponent<Bubble>().target = target;

		currentBubbles.Add(lastBubble);
		currentBubbleConfigs.Add(lastBubble.GetComponent<Bubble>());

		bubbleFollowSpeed = Mathf.Clamp(bubbleFollowSpeed-.05f, .1f, .5f);

		foreach(Bubble config in currentBubbleConfigs) 
			config.speed = bubbleFollowSpeed;

  }

  void OnSwipeEvent(SwipeEvent e) {

		float xVelocity = -e.velocity;

		if(e.dir == TKSwipeDirection.Right)
			xVelocity = -xVelocity;

		deltaMovement = ClampToScreen(transform.TransformPoint(new Vector3(xVelocity * .5f, 0, 0)));

  }

	void BubbleHitEvent(HitEvent e) {

		if(e.eventType == HitEvent.Type.Spawn)
			SpawnHit(e.collider, e.bubble);
		else {

			currentBubbles.Remove(e.bubble);
  		currentBubbleConfigs.Remove(e.bubble.GetComponent<Bubble>());

			for(int i = 0; i < currentBubbleConfigs.Count; i++) {
				Transform target = (i > 0) ? currentBubbles[i-1].transform : transform;
				currentBubbleConfigs[i].target = target;
			} 

		}

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
		else
			AddBubble();

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

	void OnScoreEvent (ScoreEvent e) {

		targetScore += e.scoreAmount;

	}

	void Awake () {

		deltaMovement = transform.position;
		mainCamera = Camera.main;

		Events.instance.AddListener<MovementEvent> (OnMovementEvent);
		Events.instance.AddListener<HitEvent> (BubbleHitEvent);
		Events.instance.AddListener<SwipeEvent> (OnSwipeEvent);
		Events.instance.AddListener<ScoreEvent> (OnScoreEvent);
	
	}

	// Use this for initialization
	void Start () {

		currentBubbles = new List<GameObject>();
		currentBubbleConfigs = new List<Bubble>();

		// moveLeftBtn = GameObject.Find("MoveLeft").GetComponent<Button>();
		// moveLeftBtn.gameObject.SetActive(false);

		// moveRightBtn = GameObject.Find("MoveRight").GetComponent<Button>();
		// moveRightBtn.gameObject.SetActive(false);

		gameOverText = GameObject.Find("GameOver");
		gameOverText.SetActive(false);

		toggleMovement.onValueChanged.AddListener(MovementToggle);
		toggleTrail.onValueChanged.AddListener(TrailToggle);

		for(int i = 0; i < 4; i++)
			AddBubble();
		
	}

	void Update() {

  	Vector3 targetPosition = transform.TransformPoint(new Vector3((moveLeft ? -movementSpeed : movementSpeed), 0, 0));

		if(moveLeft || moveRight) {
			transform.position = ClampToScreen(Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime));
			deltaMovement = transform.position;
		}
	  else if(!freeMovement && !mouseDrag)
	  	transform.position = Vector3.SmoothDamp(transform.position, deltaMovement, ref velocity, 0.2f);

	  if(currentScore < targetScore) {
	  	currentScore += targetScore/20;
			scoreText.text = "Score: " + currentScore;
	  }
	}
	
  void OnMouseDown() {

  	mouseDrag = true;
  	// GetComponent("Halo").GetType().enabled = true;
		Behaviour h = (Behaviour)GetComponent("Halo");
		h.enabled = true;
  }
	
  void OnMouseUp() {

  	// GetComponent("Halo").GetType().enabled = false;/**/
		Behaviour h = (Behaviour)GetComponent("Halo");
		h.enabled = false;

  }
      
  void OnMouseDrag() {

    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, freeMovement ? Input.mousePosition.y : 250, 0);
    Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint);

    transform.position = ClampToScreen(cursorPosition);

  }

	void OnTriggerEnter(Collider collider)
  {
  	if(collider.gameObject.tag != "Spawn")
  		return;

  	if(currentBubbles.Count > 0)
	  	SpawnHit(collider, currentBubbles[currentBubbles.Count-1]);
  	else
	  	SpawnHit(collider);

  }
  
}
