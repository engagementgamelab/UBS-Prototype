using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardObject : MonoBehaviour {
	
	public GameObject[] bubblePlaceholders;
	
  public float percentsPerSecond = 0.5f;

	Vector3[] movementPoints = new Vector3[20];
  float currentPathPercent = 0.0f; //min 0, max 1

	int placeholderIndex = 0;

  Vector3 ClampToScreen(Vector3 vector) {

  	Vector3 pos = Camera.main.ScreenToWorldPoint(vector);
		pos.z = 0;

  	return pos;

  }

	void BubbleMount(GameObject bubble) {
  	
		if(GetComponentsInChildren(typeof(Bubble)).Length == 8)
			return;

		int ind = GetComponentsInChildren(typeof(Bubble)).Length;
		Transform t = (Transform)GetComponentsInChildren(typeof(BubbleSpace))[ind].transform;
		placeholderIndex++;

		bubble.GetComponent<Bubble>().followPlayer = false;
		bubble.transform.SetParent(transform);

		bubble.transform.localPosition = t.localPosition;
		
		if(GetComponentsInChildren(typeof(Bubble)).Length == 8) {
			iTween.ScaleTo(gameObject, Vector3.zero, 1.0f);
			Events.instance.Raise (new ScoreEvent(1000));  
		}

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

	}

	void OnTriggerEnter(Collider collider) {

		if(collider.tag != "Bubble")
			return;

		// if(placeholderIndex >= 8)
		// 	return;

		BubbleMount(collider.gameObject);
		Events.instance.Raise (new HitEvent(HitEvent.Type.PowerUp, collider, collider.gameObject));  

	}
}
