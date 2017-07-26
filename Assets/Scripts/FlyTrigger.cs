﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTrigger : MonoBehaviour {
	
	public float moveSpeed = .5f;

	public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float step = 10.0f * Time.deltaTime;

		Vector3 target = transform.position;
		target.y -= moveSpeed;

		transform.position = Vector3.Lerp(transform.position, target, .2f);
		

		if(Camera.main.WorldToViewportPoint(transform.position).y < 1) {
			Instantiate(prefab, Vector3.zero, Quaternion.identity);
			Destroy(gameObject);
		}
		
	}
}
