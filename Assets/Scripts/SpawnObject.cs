using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	public Material[] materials;
	public bool IsEnemy {
		get { return _IsEnemy; }
		set { 
			_IsEnemy = value;
			rend.sharedMaterial = materials[_IsEnemy ? 1 : 0]; 
		}
	}

	public float MoveSpeed {
		get { return _MoveSpeed; }
		set { _MoveSpeed = value; }
	}

	bool _IsEnemy = false;	
	float _MoveSpeed;
	MeshRenderer rend; 

	// Use this for initialization
	void Awake () {
		
		rend = GetComponent<MeshRenderer>();

	}	
	
	// Update is called once per frame
	void Update () {

		if(_MoveSpeed == 0)
			return;

		float step = 10.0f * Time.deltaTime;

		Vector3 target = transform.position;
		target.y -= _MoveSpeed;

		transform.position = Vector3.Lerp(transform.position, target, .2f);
		
	}

}