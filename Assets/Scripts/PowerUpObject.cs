using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PowerUpObject : SpawnObject {
	
	// public GameObject[] bubblePlaceholders;
	public RawImage healthBg;
	public RawImage healthFill;

	int placeholderIndex = 0;
	float health = 2;

  IEnumerator RemoveVillager()
  {
      yield return new WaitForSeconds(1);
      Destroy(gameObject);
  }

	public void BubbleHitEvent(Transform t, GameObject bubble) {

	}
	
	// Update is called once per frame
	void Update () {
		
		base.Update();

	}

	void OnTriggerEnter(Collider collider) {

  	if(collider.gameObject.GetComponent<SpawnObject>() != null) {
  		if(collider.gameObject.GetComponent<SpawnObject>().isFly) {
	
				if(health < 5) {
					Vector2 bgSize = healthBg.rectTransform.sizeDelta;
		  		health += .5f;
					bgSize.x = health;
					healthBg.rectTransform.sizeDelta = bgSize;
				}

	  	}
  		return;
  	}
		
		if(spawnType != "villager")
			return;

		if(collider.tag != "Bubble")
			return;

		// Transform t = (Transform)GetComponentsInChildren(typeof(BubbleSpace))[placeholderIndex].transform;
		placeholderIndex++;

		// BubbleMount(t, collider.gameObject);
		Events.instance.Raise (new HitEvent(HitEvent.Type.PowerUp, collider, collider.gameObject));

		Vector2 v = healthFill.rectTransform.sizeDelta;
		v.x += .5f;
		healthFill.rectTransform.sizeDelta = v;

		if(v.x == health) {

			iTween.ScaleTo(gameObject, Vector3.zero, 1.0f);
			Events.instance.Raise (new ScoreEvent(1, ScoreEvent.Type.Good));	
			StartCoroutine(RemoveVillager());

			isDestroyed = true;
			GameConfig.peopleSaved++;

			return;
		}

	}

}