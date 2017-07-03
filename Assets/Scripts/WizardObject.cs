using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardObject : SpawnObject {
	
	public Canvas healthCanvas;
	public RawImage healthBg;
	public RawImage healthFill;

  public float percentsPerSecond = 0.5f;
  public float health = 5;

	Vector3[] movementPoints = new Vector3[20];
  float currentPathPercent = 0.0f; //min 0, max 1

	int placeholderIndex = 0;

  Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = Camera.main.ScreenToWorldPoint(vector);
		pos.z = 0;

  	return pos;

  }

	// Use this for initialization
	void Awake () {

		for(int i = 0; i < 19; i++)
			movementPoints[i] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), Random.Range(Screen.height, 0), 0));
		
		movementPoints[19] = ClampToScreen(new Vector3(Random.Range(0, Screen.width), -Screen.height-50, 0));

	}
	
	// Update is called once per frame
	void Update () {
		
		// base.Update();

    if(movementPoints.Length > 0 && currentPathPercent < 1) {
        
      currentPathPercent += percentsPerSecond * Time.deltaTime;

      iTween.PutOnPath(transform, movementPoints, currentPathPercent);

    }
    
    if(GetComponentsInChildren(typeof(PowerUpObject)).Length == 0)
	    healthCanvas.gameObject.SetActive(true);

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

			// isDestroyed = true;
			// GameConfig.peopleSaved++;

			return;
		}

	}
}
