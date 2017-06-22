using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour {

  public static float numObjects = 10;
  public static float speedFactor = 1;
  public static float powerUpChance = 0.15f;
  
  public static bool speedUpToggle;
  public static bool increaseToggle;
  
  public static int powerUpsCount = 0;

	// Use this for initialization
	void Awake () {
		
		DontDestroyOnLoad(this);

	}

	public static void Reset() {
		numObjects = 10;
		speedFactor = 1;
		powerUpChance = 0.15f;
		
		speedUpToggle = false;
		increaseToggle = false;

		powerUpsCount = 0;

	}

}