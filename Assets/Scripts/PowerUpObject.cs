using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PowerUpObject : SpawnObject {
	
	int placeholderIndex = 0;
	
	// Use this for initialization
	void Awake ()
	{

		base.Awake();

		if(_MoveSpeed == 0)	
			_MoveSpeed = .1f;

	}
	
	// Update is called once per frame
	void Update () {

		base.Update();

	}

}