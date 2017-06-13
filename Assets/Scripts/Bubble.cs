using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Bubble : MonoBehaviour {
	
	public Transform target;
	public float smoothTime = 51F;
	private Vector3 velocity = Vector3.zero;

	float xPos;

	void Awake() {
		xPos = Random.Range(-1f, 1f);
	}
	
	void FixedUpdate() {

	  Vector3 targetPosition = target.TransformPoint(new Vector3(Random.Range(-.5f, .5f), Random.Range(-1, 1), 2));
	  transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

	}

	void OnTriggerEnter(Collider collider)
  {
		Events.instance.Raise (new HitEvent(collider));  
  }
}
