using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObject : SpawnObject {
	
	public GameObject[] bubblePlaceholders;

	int placeholderIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		base.Update();

	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.GetComponent<Bubble>() == null)
			return;

		if(GetComponentsInChildren(typeof(Bubble)).Length == 4)
			return;

		collider.gameObject.GetComponent<Bubble>().followPlayer = false;
		collider.transform.SetParent(transform);

		collider.transform.localPosition = bubblePlaceholders[placeholderIndex].transform.localPosition;
		placeholderIndex = Mathf.Clamp(placeholderIndex+1, 1, 3); 
	}

}