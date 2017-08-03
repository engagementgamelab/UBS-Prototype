using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndTrigger : SpawnObject
{

	public RectTransform endPanel;
	private bool playerDead;

	// Use this for initialization
	void Start () {
		
		Events.instance.AddListener<DeathEvent> (OnDeathEvent);

		
	}
	// Update is called once per frame
	void Update ()
	{
		
		base.Update();

		if(playerDead)
			return;
		
		Debug.Log(Camera.main.WorldToViewportPoint(transform.position).y);
	
		if(Camera.main.WorldToViewportPoint(transform.position).y < 1) {
			
			Events.instance.Raise (new DeathEvent(true));  
			
			endPanel.gameObject.SetActive(true);	
			Destroy(gameObject);
		}
		
		
	}

	void OnDeathEvent(DeathEvent e)
	{
		playerDead = true;
	}
}
