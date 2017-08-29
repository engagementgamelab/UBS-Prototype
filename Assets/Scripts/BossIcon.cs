using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossIcon : MonoBehaviour
{

	public Text label;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseUp() {
		
		Destroy(gameObject);
		Events.instance.Raise (new BossIconEvent());

	}
}
