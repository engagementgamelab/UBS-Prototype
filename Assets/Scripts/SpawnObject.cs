using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnObject : MonoBehaviour {

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
  public float percentsPerSecond = 0.5f;

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
	
	MeshRenderer rend; 

  public Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = Camera.main.ScreenToWorldPoint(vector);
		pos.z = 0;

  	return pos;

  }
	
	// Update is called once per frame
	public void Update () {

		if(!moveEnabled)
			return;

		if(_MoveSpeed == 0)
			return;

		if(spawnType != "fly") {

			Vector3 target = transform.position;
			
			if(movementDir == "up")
				target.y += _MoveSpeed;
			else if(movementDir == "right")
				target.x += _MoveSpeed;
			else if(movementDir == "left")
				target.x -= _MoveSpeed;
			else
				target.y -= _MoveSpeed;

			transform.position = Vector3.Lerp(transform.position, target, .2f);

			if(Camera.main.WorldToViewportPoint(transform.position).y < 0) {

				if(!isDestroyed)
					Events.instance.Raise (new ScoreEvent(1, ScoreEvent.Type.Bad));

				Destroy(gameObject);
			}

		}
		
	}

}