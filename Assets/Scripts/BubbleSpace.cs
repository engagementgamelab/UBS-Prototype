﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpace : MonoBehaviour {

	bool hasBubble;

	void OnTriggerEnter(Collider collider) {

		if(hasBubble)
			return;
  	
  	if(collider.gameObject.tag != "Bubble")
  		return;

  	PowerUpObject parent = transform.parent.GetComponent<PowerUpObject>();
  	parent.BubbleHitEvent(transform, collider.gameObject);

		Events.instance.Raise (new HitEvent(HitEvent.Type.PowerUp, collider, collider.gameObject));  

		hasBubble = true;

	}

}
