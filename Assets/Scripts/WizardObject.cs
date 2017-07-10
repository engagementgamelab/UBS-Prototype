using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardObject : SpawnObject {
	
	public Canvas healthCanvas;
	public RawImage healthBg;
	public RawImage healthFill;

	public Transform fliesParent;

  public float health = 5;

	int placeholderIndex = 0;

	// Use this for initialization
	void Awake () {

		for(int i = 0; i < 19; i++)
			movementPoints[i] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), Random.Range(Screen.height, 0), 0));
		
		movementPoints[19] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), -Screen.height-50, 0));

			// iTween.RotateBy(fliesParent.gameObject, iTween.Hash("z", 1, "looptype", iTween.LoopType.loop));


	}
	
	// Update is called once per frame
	void Update () {
		
		// base.Update();

		if(fliesParent != null)
			fliesParent.Rotate(0, 0, -1.0f*20.0f*Time.deltaTime);

    if(movementPoints.Length > 0) {

    	if(currentPathPercent < 1) {        
	      currentPathPercent += percentsPerSecond * Time.deltaTime;
	      iTween.PutOnPath(transform, movementPoints, currentPathPercent);
    	}
    	
    	else {
    		
				Events.instance.Raise (new ScoreEvent(1, ScoreEvent.Type.Bad));
				Destroy(gameObject);

    	}

    }

    if(GetComponentsInChildren(typeof(PowerUpObject)).Length == 0)
	    healthCanvas.gameObject.SetActive(true);

    if(GetComponentsInChildren(typeof(FlyObject)).Length == 0) {
    	foreach(PowerUpObject villager in GetComponentsInChildren(typeof(PowerUpObject)))
		    villager.healthCanvas.gameObject.SetActive(true);
    }

	}

	void OnTriggerEnter(Collider collider) {

    if(GetComponentsInChildren(typeof(PowerUpObject)).Length > 0)
    	return;

		if(spawnType != "wizard")
			return;

		if(collider.tag != "Bubble")
			return;

		placeholderIndex++;
		Events.instance.Raise (new HitEvent(HitEvent.Type.PowerUp, collider, collider.gameObject));

		Vector2 v = healthFill.rectTransform.sizeDelta;
		v.x += .5f;
		healthFill.rectTransform.sizeDelta = v;

		if(v.x == health) {

			iTween.ScaleTo(gameObject, Vector3.zero, 1.0f);
			Events.instance.Raise (new ScoreEvent(1, ScoreEvent.Type.Good));

			return;
		}

	}
}
