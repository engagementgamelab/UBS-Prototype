using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{

	public bool sandBoxMode;

	// Use this for initialization
	void Awake ()
	{
		GameConfig.sandboxMode = sandBoxMode;
	}
	
}
