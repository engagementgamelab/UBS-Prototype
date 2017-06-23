using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour {

	public Text bubblesToStartValue;
  public Text bubblesGainedValue;
  public Text peopleStartSpeedValue;
  public Text peopleNumberPerMinValue;
  public Text peopleNumberOverTimeValue;
	public Text peopleSpeedOverTimeValue;

	public Button startButton;

  public Slider bubblesToStartSlider;
  public Slider bubblesGainedSlider;
  public Slider peopleStartSpeedSlider;
  public Slider peopleNumberPerMinSlider;
  public Slider peopleNumberOverTimeSlider;
  public Slider peopleSpeedOverTimeSlider;

  public Toggle badWizardToggle;
  public Toggle villagersToggle;

  public RectTransform badWizardGroup;
  public RectTransform villagersGroup;

  public SpawnManager spawnManager;
  public Player player;

	// Use this for initialization
	void Start () {

		bubblesToStartSlider.onValueChanged.AddListener(delegate {OnBubblesToStartSlider(); });
    bubblesGainedSlider.onValueChanged.AddListener(delegate {OnBubblesGainedSlider(); });
    
    peopleStartSpeedSlider.onValueChanged.AddListener(delegate {OnPeopleStartSpeedSlider(); });
    peopleNumberPerMinSlider.onValueChanged.AddListener(delegate {OnPeopleNumberPerMinSlider(); });
    peopleNumberOverTimeSlider.onValueChanged.AddListener(delegate {OnPeopleOverTimeSlider(); });
		peopleSpeedOverTimeSlider.onValueChanged.AddListener(delegate {OnPeopleOverTimeSlider(); });

		startButton.onClick.AddListener(OnStart);

		badWizardToggle.onValueChanged.AddListener(BadWizardToggle);
		villagersToggle.onValueChanged.AddListener(VillagersToggle);
		
	}

	void OnBubblesToStartSlider() {

  	bubblesToStartValue.text = bubblesToStartSlider.value + "";
  	GameConfig.numBubblesToStart = bubblesToStartSlider.value;

  }

	void OnBubblesGainedSlider() {

  	bubblesGainedValue.text = bubblesGainedSlider.value + "";
  	GameConfig.numBubblesGained = bubblesGainedSlider.value;

  }

  void OnPeopleStartSpeedSlider() {

    peopleStartSpeedValue.text = peopleStartSpeedSlider.value + "";
    GameConfig.peopleSpeedStart = peopleStartSpeedSlider.value;

  }

  void OnPeopleNumberPerMinSlider() {

    peopleNumberPerMinValue.text = peopleNumberPerMinSlider.value + "";
    GameConfig.peopleNumberPerMin = peopleNumberPerMinSlider.value;

  }

  void OnPeopleOverTimeSlider() {

    peopleNumberOverTimeValue.text = peopleNumberOverTimeSlider.value + "";
    GameConfig.peopleSpeedIncreaseFactor = peopleNumberOverTimeSlider.value;

  }

  void OnPeopleSpeedOverTimeSlider() {

    peopleSpeedOverTimeValue.text = peopleSpeedOverTimeSlider.value + "";
    GameConfig.peopleSpeedIncreaseFactor = peopleSpeedOverTimeSlider.value;

  }

  void BadWizardToggle(bool value) {

  	// GameConfig.speedUpToggle = value;
    badWizardGroup.gameObject.SetActive(value);

  }

  void VillagersToggle(bool value) {

    GameConfig.peopleInGame = value;
    villagersGroup.gameObject.SetActive(value);

  }

  void OnStart() {
  	// GetComponent
  	gameObject.SetActive(false);
    spawnManager.gameObject.SetActive(true);
  	player.gameObject.SetActive(true);
  }

}