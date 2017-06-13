using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SwipeHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var recognizer = new TKSwipeRecognizer(.1f);
		// recognizer.timeToSwipe = .1f; 
		recognizer.gestureRecognizedEvent += ( r ) =>
		{
			TKSwipeDirection sdir = r.completedSwipeDirection;
			// Debug.Log( r.swipeVelocity );
			// Debug.Log( r.startPoint );
			// Debug.Log( r.endPoint );
			// Debug.Log( "=====" );
			Events.instance.Raise (new SwipeEvent(sdir, r.swipeVelocity)); 
		};
		TouchKit.addGestureRecognizer( recognizer );

	}

}
