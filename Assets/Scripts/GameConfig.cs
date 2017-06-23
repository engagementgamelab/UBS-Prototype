using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour {

  public static float numBubblesToStart = 4;
  public static float numBubblesGained = 1;
  public static float peopleSpeedStart = 1;
  public static float peopleNumberPerMin = 10;
  public static float peopleAmountIncreaseFactor = .03f;
  public static float peopleSpeedIncreaseFactor = 1;
  public static float powerUpChance = 0.15f;
  
  public static bool speedUpToggle;
  public static bool increaseToggle;
  public static bool peopleInGame = true;
  
  public static int powerUpsCount = 0;

	// Use this for initialization
	void Awake () {
		
		DontDestroyOnLoad(this);

	}

	public static void Reset() {
		
		numBubblesToStart = 4;
		numBubblesGained = 1;

		peopleSpeedStart = 1;
		peopleNumberPerMin = 10;
		peopleSpeedIncreaseFactor = 1;
		peopleAmountIncreaseFactor = .03f;

		powerUpChance = 0.15f;
		
		speedUpToggle = false;
		increaseToggle = false;
		peopleInGame = true;

		powerUpsCount = 0;

	}

}