using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{

	public bool sandBoxMode;
	
	[Range(0, 30)]
	public float gameSpeed = 1;

	// Use this for initialization
	void Awake ()
	{
		GameConfig.sandboxMode = sandBoxMode;
		GameConfig.gameSpeedModifier = gameSpeed;
	}
	
}
