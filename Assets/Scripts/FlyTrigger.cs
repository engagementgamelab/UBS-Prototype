using System.Collections;
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

		if(GameConfig.gamePaused)
			return;
		
		Vector3 target = transform.position;
		target.y -= moveSpeed * GameConfig.gameSpeedModifier;

		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
	
		if(Camera.main.WorldToViewportPoint(transform.position).y < 1) {
			GameObject fly = Instantiate(prefab, transform.position, Quaternion.identity);
			
			Destroy(gameObject);
		}
		
	}
}
