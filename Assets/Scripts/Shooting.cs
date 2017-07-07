using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour {
	
	public GameObject bubble;

	public Image meterImage; 
	public float bubbleSpeed;

	public float shootInterval = 20;
	float intervalTime = 0;

	bool reloading;

	void Update () {

		if(reloading)
			return;

		if(Input.GetMouseButton(0) && (intervalTime >= shootInterval)) {

			if(meterImage.fillAmount == 0)
				return;

		 	intervalTime = 0;

			Vector2 touchPos = new Vector2(transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			Vector2 dir = touchPos - (new Vector2(transform.position.x, transform.position.y));
			dir.Normalize ();
			
			GameObject projectile = Instantiate (bubble, transform.position, Quaternion.identity) as GameObject;
		  Vector2 lookDelta = (touchPos - new Vector2(projectile.transform.position.x, projectile.transform.position.y));
		  
		  float angle = Mathf.Atan2(lookDelta.x, lookDelta.y);
			projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg );
			projectile.GetComponent<Rigidbody> ().velocity = dir * bubbleSpeed; 

			Destroy(projectile, 2);

			if(meterImage.fillAmount > 0)
				meterImage.fillAmount -= .05f;

		}
		else
			intervalTime += Time.deltaTime;

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
