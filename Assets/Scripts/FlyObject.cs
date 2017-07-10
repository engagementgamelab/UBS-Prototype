using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FlyObject : SpawnObject {
	
	int placeholderIndex = 0;
	
	// Use this for initialization
	void Awake () {


		if(!wizardMode) {
			movementPoints = new Vector3[10];
			
			for(int i = 0; i < 9; i++)
				movementPoints[i] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), Random.Range(Screen.height, 0), 0));
			
			movementPoints[9] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), Screen.height+50, 0));
		}

		else {
			movementPoints = new Vector3[4];

			movementPoints[0] = new Vector3(Random.Range(transform.localPosition.x-1, transform.localPosition.x+1), Random.Range(transform.localPosition.y-1, transform.localPosition.y+1), 0);
			movementPoints[1] = new Vector3(Random.Range(transform.localPosition.x-1, transform.localPosition.x+1), transform.localPosition.y, 0);
			movementPoints[2] = new Vector3(transform.localPosition.x, Random.Range(transform.localPosition.y-1, transform.localPosition.y+1), 0);
			movementPoints[3] = new Vector3(Random.Range(transform.localPosition.x-1, transform.localPosition.x+1), transform.localPosition.y, 0);
	   
			iTween.MoveTo(gameObject, iTween.Hash("path", movementPoints, "islocal", true, "time", Random.Range(1, 10), "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInOutSine));
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(wizardMode)
			return;
		
    if(movementPoints.Length > 0) {
    	if(currentPathPercent < 1) {
	        
	      currentPathPercent += percentsPerSecond * Time.deltaTime;

	      iTween.PutOnPath(transform, movementPoints, currentPathPercent);

	      Vector3 lookVector = iTween.PointOnPath(movementPoints, currentPathPercent + 0.05f);
	      Vector3 lookDelta = (lookVector - transform.position);
	      
	      float angle = Mathf.Atan2(lookDelta.x, lookDelta.y);
				transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg );
	    	
    	}
    	else {
    		
				Events.instance.Raise (new ScoreEvent(1, ScoreEvent.Type.Bad));
				Destroy(gameObject);

    	}
    }

	}

}