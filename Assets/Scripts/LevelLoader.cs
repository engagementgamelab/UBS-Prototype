using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
  public void LoadFullConfig() {

  	Application.LoadLevel("Full");

  }
  public void LoadWizard() {

  	Application.LoadLevel("Wizard");

  }
  public void LoadShooting() {

  	Application.LoadLevel("Shooting");

  }
  public void LoadShootingStatic() {

    Application.LoadLevel("ShootingStatic");

  }
}
