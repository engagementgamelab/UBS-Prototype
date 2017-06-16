using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour {

	public Text numText;
	public Text speedText;

  public Slider numSlider;
  public Slider speedSlider;

  public static float NumObjects;

	// Use this for initialization
	void Start () {

		numSlider.onValueChanged.AddListener(delegate {OnNumSlider(); });
		speedSlider.onValueChanged.AddListener(delegate {OnSpeedSlider(); });
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnNumSlider() {
  	numText.text = numSlider.value + "";
  	NumObjects = numSlider.value;
  }

	void OnSpeedSlider() {
  	speedText.text = speedSlider.value + "";
  }

}