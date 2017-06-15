using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	public float MoveSpeed {
		get { return _MoveSpeed; }
		set { _MoveSpeed = value; }
	}

	public bool isEnemy = false;	

	float _MoveSpeed;
	MeshRenderer rend; 
	
	// Update is called once per frame
	public void Update () {

		if(_MoveSpeed == 0)
			return;

		float step = 10.0f * Time.deltaTime;

		Vector3 target = transform.position;
		target.y -= _MoveSpeed;

		transform.position = Vector3.Lerp(transform.position, target, .2f);

		if(Camera.main.WorldToViewportPoint(transform.position).y < 0)
			Destroy(gameObject);
		
	}

}