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

		transform.position = Vector3.Lerp(transform.position, target, .2f);
	
		if(Camera.main.WorldToViewportPoint(transform.position).y < 3.5f) {
			GameObject fly = Instantiate(prefab, transform.position, Quaternion.identity);
//			fly.transform.parent.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			Destroy(gameObject);
		}
		
	}
}
