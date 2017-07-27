using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
	[Range(1, 20)]
	public float localMoveDuration = 1;
	
	private List<Vector3> waypoints;
	private float currentPathPercent;

	// Use this for initialization
	void Start () {
		
		waypoints = new List<Vector3>();
		foreach(Transform tr in transform)
		{
			if(tr.tag == "Waypoint")
			{
				waypoints.Add(tr.position);
			}
		}
		
		iTween.MoveTo(gameObject, iTween.Hash("path", waypoints.ToArray(), "islocal", true, "time", localMoveDuration, 
																					"looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInOutSine));

		
	}
	
	// Update is called once per frame
	void Update () {
		
		
//	        
		currentPathPercent = localMoveDuration * Time.deltaTime;
		
		Debug.Log(iTween.PointOnPath(waypoints.ToArray(), Mathf.PingPong(Time.time, localMoveDuration)));

//
//		Vector3 lookVector = iTween.PointOnPath(movementPoints, currentPathPercent + 0.05f);
//		Vector3 lookDelta = (lookVector - transform.position);
//	      
//		float angle = Mathf.Atan2(lookDelta.x, lookDelta.y);
//		transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg );
	}
}
