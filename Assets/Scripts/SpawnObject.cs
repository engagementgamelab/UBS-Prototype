using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnObject : MonoBehaviour {

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

	[HideInInspector]
	public int _spawnTypeIndex = 0;
	[HideInInspector]
	public string spawnType;

	float _MoveSpeed;
	MeshRenderer rend; 
	
	// Update is called once per frame
	public void Update () {

		if(!moveEnabled)
			return;

		if(_MoveSpeed == 0)
			return;

		if(spawnType != "fly") {

			float step = 10.0f * Time.deltaTime;

			Vector3 target = transform.position;
			target.y -= _MoveSpeed;

			transform.position = Vector3.Lerp(transform.position, target, .2f);

			if(Camera.main.WorldToViewportPoint(transform.position).y < 0) {
				Events.instance.Raise (new ScoreEvent(1, ScoreEvent.Type.Bad));

				Destroy(gameObject);
			}

		}
		
	}

}