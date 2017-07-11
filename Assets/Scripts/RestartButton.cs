using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartButton : MonoBehaviour, IPointerDownHandler {

	public void OnPointerDown(PointerEventData eventData)
  {
  	GameConfig.Reset();
  	Application.LoadLevel("Select");      
  }

}
