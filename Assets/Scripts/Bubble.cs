﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Bubble : MonoBehaviour {
	
	public Transform target;
	public float speed;

	public bool followPlayer = true;
	// public bool inBossBattle = false;

	private Vector3 velocity = Vector3.zero;

	float xPos;

	void Awake() {
		xPos = Random.Range(-1f, 1f);

		// Events.instance.AddListener<MovementEvent> (OnMovementEvent);
	}
	
	void FixedUpdate() {

		if(!followPlayer)
			return;

		if(target == null)
			return;

	  Vector3 targetPosition = target.TransformPoint(new Vector3(Random.Range(-.5f, .5f), Random.Range(-1, 1), 0));
	  transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);

	}

	void OnTriggerEnter(Collider collider) {
		
  	if(collider.gameObject.tag != "Spawn")
  		return;

		Events.instance.Raise (new HitEvent(HitEvent.Type.Spawn, collider, gameObject));  

  }
}
