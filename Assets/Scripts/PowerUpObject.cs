using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PowerUpObject : SpawnObject {
	
	public GameObject[] bubblePlaceholders;

	int placeholderIndex = 0;

	public void BubbleHitEvent(Transform t, GameObject bubble) {
  	
		if(GetComponentsInChildren(typeof(Bubble)).Length == 4)
			return;

		bubble.GetComponent<Bubble>().followPlayer = false;
		bubble.transform.SetParent(transform);

		bubble.transform.localPosition = t.localPosition;
		
		if(GetComponentsInChildren(typeof(Bubble)).Length == 4) {
			iTween.ScaleTo(gameObject, Vector3.zero, 1.0f);
			Events.instance.Raise (new ScoreEvent(1000));  
		}

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		base.Update();

	}

}