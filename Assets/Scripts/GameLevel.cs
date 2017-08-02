using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevel : MonoBehaviour
{

	public bool sandBoxMode;
	
	[Range(0, 30)]
	public float gameSpeed = 1;
	
	public Slider gameSpeedSlider;
	public Text gameSpeedText;

	// Use this for initialization
	void Awake ()
	{
		GameConfig.sandboxMode = sandBoxMode;
		
		if(GameConfig.gameSpeedModifier == 0)
			GameConfig.gameSpeedModifier = gameSpeed;
	}

	public void AdjustSpeed()
	{
		GameConfig.gameSpeedModifier = gameSpeedSlider.value;
		gameSpeedText.text = gameSpeedSlider.value + "";
	}
	
}
