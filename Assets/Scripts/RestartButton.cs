using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartButton : MonoBehaviour {

	public void Load(string strLevel)
  {
  	GameConfig.Reset();
  	Application.LoadLevel(strLevel);      
  }

}
