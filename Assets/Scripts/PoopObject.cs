using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PoopObject : SpawnObject {
	
//	public float poopMoveSpeed = .1f;

	// Use this for initialization
	void Start ()
	{
		if(_MoveSpeed == 0 && !GameConfig.sandboxMode)
			_MoveSpeed = .1f;
	}
	
	// Update is called once per frame
	void Update () {
		
		base.Update();
		
	}
}
