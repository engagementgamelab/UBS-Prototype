using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Text scoreText;
	public float startingLifeAmount = 100.0f;

  Vector3 screenPoint;
  Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

      Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
      Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
      transform.position = cursorPosition;

  }

	void OnTriggerEnter(Collider collider)
  {

  	if(collider.gameObject.GetComponent<SpawnObject>().IsEnemy && startingLifeAmount > 0)
  		startingLifeAmount -= 10.0f;
		else if(startingLifeAmount < 100)
			startingLifeAmount += 10.0f;

		scoreText.text = "Life: " + startingLifeAmount;

		Destroy(collider.gameObject);


  }
}
