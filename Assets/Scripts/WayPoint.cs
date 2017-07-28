using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

	void OnDrawGizmos() {
	
		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position, new Vector3(.3f, .3f, .3f));
		
	}

	void OnDrawGizmosSelected() {
	
			Gizmos.color = Color.cyan;
			Gizmos.DrawCube(transform.position, new Vector3(.3f, .3f, .3f));
		
	}
	
}
