using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnObject : MonoBehaviour
{

	public Transform waypointStart;
	public Transform waypointEnd;
	
	public bool wizardMode;

	public float MoveSpeed {
		get { return _MoveSpeed; }
		set { _MoveSpeed = value; }
	}

	public bool isEnemy {
		get { return spawnType == "enemy"; }
	}
	public bool isFly {
		get { return spawnType == "fly"; }
	}
	public bool moveEnabled = true;
	public bool isDestroyed;

	[HideInInspector]
	public int _spawnTypeIndex = 0;
	[HideInInspector]
	public int _direction= 0;
	[HideInInspector]
	public string spawnType;
	
	[HideInInspector]
	public string movementDir;
	
	[HideInInspector]
	public Vector3[] movementPoints = new Vector3[20];
	[HideInInspector]
	public float currentPathPercent = 0.0f; //min 0, max 1

//	[HideInInspector]
	public float _MoveSpeed;
	
	[Range(0, 10f)]
	public float localMoveDuration = 1f;
	
	MeshRenderer rend;

	GameObject parent;
	bool moveToEnd = true;
	List<Vector3> waypoints;

  public Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = Camera.main.ScreenToWorldPoint(vector);
		pos.z = 0;

  	return pos;

  }
	
	void OnDrawGizmosSelected() {

		if(waypointStart != null && waypointEnd != null)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(waypointStart.position, waypointEnd.position);
			
			Gizmos.color = Color.green;
			Gizmos.DrawCube(waypointStart.position, new Vector3(.3f, .3f, .3f));
			Gizmos.color = Color.red;
			Gizmos.DrawCube(waypointEnd.position, new Vector3(.3f, .3f, .3f));
		}
	}

	public void Awake()
	{

		parent = new GameObject("Parent");
		parent.transform.position = transform.position;
		transform.parent = parent.transform;

		if(waypointStart != null && waypointEnd != null)
		{

			Transform[] waypoints = new Transform[2];
			waypointEnd.parent = parent.transform;
			waypointStart.parent = parent.transform;

			waypoints[0] = waypointEnd;
			waypoints[1] = waypointStart;

		} 
		else
		{
		
			waypoints = new List<Vector3>();
			foreach(Transform tr in transform)
			{
				if(tr.tag == "Waypoint")
				{
					waypoints.Add(tr.position);
				}
			}

			if(waypoints.Count > 0)
				iTween.MoveTo(gameObject, iTween.Hash("path", waypoints.ToArray(), "islocal", true, "time", localMoveDuration, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear));
		}
	}

	// Update is called once per frame
	public void Update () {

		if(waypointStart != null && waypointEnd != null)
		{
			if(moveToEnd)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, waypointEnd.localPosition, localMoveDuration * Time.deltaTime);
				if(Vector3.Distance(transform.position, waypointEnd.position) < .1f)
					moveToEnd = false;

			} else
			{

				transform.localPosition = Vector3.MoveTowards(transform.localPosition, waypointStart.localPosition, localMoveDuration * Time.deltaTime);
				if(Vector3.Distance(transform.position, waypointStart.position) < .1f)
					moveToEnd = true;

			}
		} 
		else if(waypoints != null && waypoints.Count > 0)
		{
			
			currentPathPercent = localMoveDuration * Time.deltaTime;
		
			Vector3 lookVector = iTween.PointOnPath(waypoints.ToArray(), Mathf.PingPong(Time.time, localMoveDuration)+.1f);
			Vector3 lookDelta = (lookVector - transform.position);

			float angle = Mathf.Atan2(lookDelta.y, lookDelta.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, angle);	
		}
		
		if(!moveEnabled || GameConfig.gamePaused)
			return;

		if(_MoveSpeed == 0)
			return;
		
		if(parent == null)
			return;

		float speed = _MoveSpeed * GameConfig.gameSpeedModifier;

//		if(spawnType != "fly") {

			Vector3 target = parent.transform.position;
			
			if(movementDir == "up")
				target.y += speed;
			else if(movementDir == "right")
				target.x += speed;
			else if(movementDir == "left")
				target.x -= speed;
			else
				target.y -= speed;

			parent.transform.position = Vector3.Lerp(parent.transform.position, target, Time.deltaTime);
			
			if(Camera.main.WorldToViewportPoint(parent.transform.position).y < -1) {

				if(!isDestroyed)
					Events.instance.Raise (new ScoreEvent(1, ScoreEvent.Type.Bad));

				Destroy(parent.gameObject);
			}

//		}
		
	}

}