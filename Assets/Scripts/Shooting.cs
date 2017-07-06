using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour {
	
	public GameObject bubble;
	public float bubbleSpeed;

	void Update () {

	   // if (Input.touchCount > 0) {

	     // if (Input.GetTouch (0).phase == TouchPhase.Began) {

			 if(Input.GetMouseButtonDown(0)) {

					Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					Vector2 dir = touchPos - (new Vector2(transform.position.x, transform.position.y));
					dir.Normalize ();
					
					GameObject projectile = Instantiate (bubble, transform.position, Quaternion.identity) as GameObject;
		      Vector2 lookDelta = (touchPos - new Vector2(projectile.transform.position.x, projectile.transform.position.y));
		      
		      float angle = Mathf.Atan2(lookDelta.x, lookDelta.y);
					projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg );
					projectile.GetComponent<Rigidbody> ().velocity = dir * bubbleSpeed; 

					Destroy(projectile, 2);
	     }

	   // }
	}

}
