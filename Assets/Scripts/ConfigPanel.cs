using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour {

	public Text numText;
	public Text speedText;
	public Text powerUpText;

	public Button startButton;

  public Slider numSlider;
  public Slider speedSlider;
  public Slider powerUpSlider;

  public Toggle speedToggle;
  public Toggle increaseToggle;

  public SpawnManager spawnManager;

	// Use this for initialization
	void Start () {

		numSlider.onValueChanged.AddListener(delegate {OnNumSlider(); });
		speedSlider.onValueChanged.AddListener(delegate {OnSpeedSlider(); });
		powerUpSlider.onValueChanged.AddListener(delegate {OnPowerUpSlider(); });

		startButton.onClick.AddListener(OnStart);
		speedToggle.onValueChanged.AddListener(SpeedToggle);
		increaseToggle.onValueChanged.AddListener(IncreaseToggle);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnNumSlider() {
  	numText.text = numSlider.value + "";

  	GameConfig.numObjects = numSlider.value;
  }

	void OnSpeedSlider() {
  	speedText.text = Mathf.Round(speedSlider.value) + "";

  	GameConfig.speedFactor = Mathf.Round(speedSlider.value);
  }

	void OnPowerUpSlider() {

  	powerUpText.text = powerUpSlider.value + "%";
  	GameConfig.powerUpChance = powerUpSlider.value * 0.01f;
  
  }

  void SpeedToggle(bool value) {

  	GameConfig.speedUpToggle = value;

  }

  void IncreaseToggle(bool value) {

  	GameConfig.increaseToggle = value;

  }

  void OnStart() {
  	// GetComponent
  	gameObject.SetActive(false);
  	spawnManager.gameObject.SetActive(true);
  }

}