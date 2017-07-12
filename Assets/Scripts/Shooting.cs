using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour {
	
	public GameObject bubble;

	public Image meterImage; 
	public float bubbleSpeed;

	// public float shootInterval = 20;
	public bool isStatic;

	float intervalTime = 0;

	bool reloading;

	void Update () {

		if(reloading)
			return;

		if(Input.GetMouseButton(0) && (intervalTime >= GameConfig.numBubblesInterval)) {

			if(meterImage.fillAmount == 0)
				return;

			float yPos =	Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

			if(yPos < -3.9f)
				return;

		 	intervalTime = 0;

		 	float xPos = isStatic ? Camera.main.ScreenToWorldPoint(Input.mousePosition).x : transform.position.x;

			Vector2 touchPos = new Vector2(xPos, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			Vector2 dir = touchPos - (new Vector2(transform.position.x, transform.position.y));
			dir.Normalize ();
			
			GameObject projectile = Instantiate (bubble, transform.position, Quaternion.identity) as GameObject;
		  Vector2 lookDelta = (touchPos - new Vector2(projectile.transform.position.x, projectile.transform.position.y));
		  
		  float angle = Mathf.Atan2(lookDelta.x, lookDelta.y);
			projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg );
			projectile.GetComponent<Rigidbody> ().velocity = dir * bubbleSpeed; 

			Destroy(projectile, 2);

			if(meterImage.fillAmount > 0)
				meterImage.fillAmount -= (meterImage.fillAmount / GameConfig.numBubblesFull);

		}
		else
			intervalTime += Time.deltaTime;

	}
	
  void OnMouseEnter() {

  	reloading = true;

	  if(meterImage != null) {
	  	if(meterImage.fillAmount < 1)
	  		meterImage.fillAmount += (meterImage.fillAmount / GameConfig.numBubblesFull);
		}

  }

  void OnMouseExit() {

	  if(meterImage != null) {
	  	if(meterImage.fillAmount < 1)
	  		meterImage.fillAmount += (meterImage.fillAmount / GameConfig.numBubblesFull);
		}

  	reloading = false;

  }
      
  void OnMouseDrag() {

  }

	void OnTriggerEnter(Collider other)
  {

	  if(other.gameObject.tag == "Spawner") {
	  	if(meterImage.fillAmount < 1)
	  		reloading = true;
	  }

  }

	void OnTriggerExit(Collider other)
  {
		reloading = false;
  }

}
