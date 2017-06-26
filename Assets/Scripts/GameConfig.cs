﻿using System.Collections;
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

  public static float wizardSpeedStart = 1;
  public static float wizardAmountIncreaseFactor = 0;
  public static float wizardsNumberPerMin = 5;
  public static float wizardSpeedIncreaseFactor = 1;
  public static float wizardChance = 0.45f;

  public static float fliesSpeedStart = 1;
  public static float fliesNumberPerMin = 5;
  public static float fliesAmountIncreaseFactor = 0;
  public static float fliesSpeedIncreaseFactor = 1;
  
  public static bool speedUpToggle;
  public static bool increaseToggle;
  public static bool peopleInGame = true;
  public static bool wizardInGame;
  public static bool fliesInGame;
  
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

		wizardSpeedStart = 1;
		wizardAmountIncreaseFactor = 0;
		wizardSpeedIncreaseFactor = 1;

		fliesSpeedStart = 1;
		fliesNumberPerMin = 5;
		fliesAmountIncreaseFactor = 0;
		fliesSpeedIncreaseFactor = 1;

		wizardChance = 0.15f;
		
		speedUpToggle = false;
		increaseToggle = false;
		peopleInGame = true;
		wizardInGame = false;
		fliesInGame = false;

		powerUpsCount = 0;

	}

}