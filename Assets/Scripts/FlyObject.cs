using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FlyObject : SpawnObject {
	
  public float percentsPerSecond = 0.5f;

	int placeholderIndex = 0;
	Vector3[] movementPoints = new Vector3[10];
  float currentPathPercent = 0.0f; //min 0, max 1

  Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = Camera.main.ScreenToWorldPoint(vector);
		// pos.x = Mathf.Clamp01(pos.x);
		// pos.y = Mathf.Clamp01(pos.y);
		pos.z = 0;

  	return pos;

  }

	// Use this for initialization
	void Awake () {

		for(int i = 0; i < 9; i++)
			movementPoints[i] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), Random.Range(Screen.height, 0), 0));
		
		movementPoints[9] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), Screen.height+50, 0));

	}
	
	// Update is called once per frame
	void Update () {
		
		base.Update();

    if(movementPoints.Length > 0 && currentPathPercent < 1) {
        
      currentPathPercent += percentsPerSecond * Time.deltaTime;

      iTween.PutOnPath(transform, movementPoints, currentPathPercent);

      Vector3 lookVector = iTween.PointOnPath(movementPoints, currentPathPercent + 0.05f);
      Vector3 lookDelta = (lookVector - transform.position);
      
      float angle = Mathf.Atan2(lookDelta.x, lookDelta.y);
			transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg );

    }

	}

}