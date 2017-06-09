using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	public float MoveSpeed = 10.0f;
	public Material[] materials;
	public bool IsEnemy {
		get { return enemy; }
		set { 
			enemy = value;
			rend.sharedMaterial = materials[enemy ? 1 : 0]; 
		}
	}

	bool enemy = false;
	MeshRenderer rend; 

	// Use this for initialization
	void Awake () {
		
		rend = GetComponent<MeshRenderer>();

	}	
	
	// Update is called once per frame
	void Update () {

		float step = MoveSpeed * Time.deltaTime;

		Vector3 target = transform.position;
		target.y -= 1;

		transform.position = Vector3.Lerp(transform.position, target, step);
		
	}

}