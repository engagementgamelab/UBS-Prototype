using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool movesLeft;

	public void OnPointerDown(PointerEventData eventData)
  {
		Events.instance.Raise (new MovementEvent(movesLeft ? "left" : "right"));      
  }
	
	public void OnPointerUp(PointerEventData eventData)
  {
		Events.instance.Raise (new MovementEvent(movesLeft ? "left" : "right", true));     
  }
}
