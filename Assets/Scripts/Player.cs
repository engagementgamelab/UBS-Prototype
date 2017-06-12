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

  bool freeMovement = true;
  bool moveLeft = false;
  bool moveRight = false;

	void Awake () {

		Events.instance.AddListener<MovementEvent> (OnMovementEvent);
		// Events.instance.AddListener<EndDragTacticEvent> (OnEndDragTacticEvent);
	
	}

	// Use this for initialization
	void Start () {

		currentBubbles = new List<GameObject>();

		moveLeftBtn = GameObject.Find("MoveLeft").GetComponent<Button>();
		moveLeftBtn.gameObject.SetActive(false);

		moveRightBtn = GameObject.Find("MoveRight").GetComponent<Button>();
		moveRightBtn.gameObject.SetActive(false);

		gameOverText = GameObject.Find("GameOver");
		gameOverText.SetActive(false);

		toggleMovement.onValueChanged.AddListener(MovementToggle);
		
	}

	void Update() {

		if(moveLeft) {
			Vector3 targetPosition = transform.TransformPoint(new Vector3(-movementSpeed, 0, 0));
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
	  }
	  else if(moveRight) {
	  	Vector3 targetPosition = transform.TransformPoint(new Vector3(movementSpeed, 0, 0));
	  	transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
	  }

	}
	
  void OnMouseDown(){

    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

  }

  void OnMouseUp(){
      // if(linePoints != null)
      //     linePoints.Clear();
      // animate = true;
  }
      
  void OnMouseDrag(){

    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, freeMovement ? Input.mousePosition.y : 50, screenPoint.z);
    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

    transform.position = cursorPosition;

  }

	void OnTriggerEnter(Collider collider)
  {

  	if(collider.gameObject.GetComponent<SpawnObject>().IsEnemy) {
  		startingLifeAmount -= 10.0f;

  		if(currentBubbles.Count == 0) {
  			Destroy(gameObject);
  			gameOverText.SetActive(true);

  			return;
  		}

  		Destroy(lastBubble);
  		currentBubbles.RemoveAt(currentBubbles.Count-1);
  		lastBubble = currentBubbles[currentBubbles.Count-1];
  	}

		else {
			startingLifeAmount += 10.0f;
			Transform target = (lastBubble != null) ? lastBubble.transform : transform;
			
			lastBubble = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
			lastBubble.GetComponent<Bubble>().target = target;
			currentBubbles.Add(lastBubble);
		}

		// scoreText.text = "Life: " + startingLifeAmount;

		Destroy(collider.gameObject);


  }

  void MovementToggle(bool value) {

  	freeMovement = value;

		moveLeftBtn.gameObject.SetActive(!value);
		moveRightBtn.gameObject.SetActive(!value);

  }


	void OnMovementEvent (MovementEvent e) {

		if(e.Direction == "left")
			moveLeft = !e.EndClick;
		else
			moveRight = !e.EndClick;
	
	}
}
