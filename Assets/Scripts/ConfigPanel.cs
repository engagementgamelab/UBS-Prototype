using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour {

  [Header("Bubbles")]
  public Text bubblesToStartValue;
  public Text bubblesGainedValue;

  public Slider bubblesToStartSlider;
  public Slider bubblesGainedSlider;
  
  [Header("People")]
  public Toggle villagersToggle;

  public Text peopleStartSpeedValue;
  public Text peopleNumberPerMinValue;
  public Text peopleNumberOverTimeValue;
  public Text peopleSpeedOverTimeValue;

  public Slider peopleStartSpeedSlider;
  public Slider peopleNumberPerMinSlider;
  public Slider peopleNumberOverTimeSlider;
  public Slider peopleSpeedOverTimeSlider;

  public RectTransform villagersGroup;

  [Header("Flies")]
  public Toggle fliesToggle;
  public Slider fliesStartNumSlider;
  public Slider fliesStartSpeedSlider;
  public Slider fliesNumberPerMinSlider;
  public Slider fliesNumberOverTimeSlider;
  public Slider fliesSpeedOverTimeSlider;

  public Text fliesStartNumValue;
  public Text fliesStartSpeedValue;
  public Text fliesNumberPerMinValue;
  public Text fliesNumberOverTimeValue;
  public Text fliesSpeedOverTimeValue;

  public RectTransform fliesGroup;

  [Header("Wizard")]
  public Toggle badWizardToggle;
  public Slider wizardStartSpeedSlider;
  public Slider wizardNumberOverTimeSlider;
  public Slider wizardSpeedOverTimeSlider;

  public Text wizardStartSpeedValue;
  public Text wizardNumberOverTimeValue;
  public Text wizardSpeedOverTimeValue;


  public RectTransform wizardGroup;

  [Header("Game")]

  public Button startButton;

  public RectTransform badWizardGroup;
  public SpawnManager spawnManager;
  public Player player;

	// Use this for initialization
	void Start () {

		bubblesToStartSlider.onValueChanged.AddListener(delegate {OnBubblesToStartSlider(); });
    bubblesGainedSlider.onValueChanged.AddListener(delegate {OnBubblesGainedSlider(); });
    
    peopleStartSpeedSlider.onValueChanged.AddListener(delegate {OnPeopleStartSpeedSlider(); });
    peopleNumberPerMinSlider.onValueChanged.AddListener(delegate {OnPeopleNumberPerMinSlider(); });
    peopleNumberOverTimeSlider.onValueChanged.AddListener(delegate {OnPeopleOverTimeSlider(); });
		peopleSpeedOverTimeSlider.onValueChanged.AddListener(delegate {OnPeopleSpeedOverTimeSlider(); });
    
    fliesStartNumSlider.onValueChanged.AddListener(delegate {OnFliesStartNumSlider(); });
    fliesStartSpeedSlider.onValueChanged.AddListener(delegate {OnFliesStartSpeedSlider(); });
    fliesNumberPerMinSlider.onValueChanged.AddListener(delegate {OnFliesNumberPerMinSlider(); });
    fliesNumberOverTimeSlider.onValueChanged.AddListener(delegate {OnFliesOverTimeSlider(); });
    fliesSpeedOverTimeSlider.onValueChanged.AddListener(delegate {OnFliesSpeedOverTimeSlider(); });
    
    // wizardStartSpeedSlider.onValueChanged.AddListener(delegate {OnWizardStartSpeedSlider(); });
    // wizardNumberOverTimeSlider.onValueChanged.AddListener(delegate {OnWizardOverTimeSlider(); });
    // wizardSpeedOverTimeSlider.onValueChanged.AddListener(delegate {OnWizardSpeedOverTimeSlider(); });

    GameConfig.peopleInGame = villagersToggle.isOn;
    GameConfig.wizardInGame = badWizardToggle.isOn;
    GameConfig.fliesInGame = fliesToggle.isOn;

		startButton.onClick.AddListener(OnStart);

		badWizardToggle.onValueChanged.AddListener(BadWizardToggle);
    villagersToggle.onValueChanged.AddListener(VillagersToggle);
		fliesToggle.onValueChanged.AddListener(FliesToggle);
		
	}

	void OnBubblesToStartSlider() {

  	bubblesToStartValue.text = bubblesToStartSlider.value + "";
  	GameConfig.numBubblesToStart = bubblesToStartSlider.value;

  }

	void OnBubblesGainedSlider() {

  	bubblesGainedValue.text = bubblesGainedSlider.value + "";
  	GameConfig.numBubblesGained = bubblesGainedSlider.value;

  }

  // PEOPLE
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
    GameConfig.peopleAmountIncreaseFactor = peopleNumberOverTimeSlider.value;

  }

  void OnPeopleSpeedOverTimeSlider() {

    peopleSpeedOverTimeValue.text = peopleSpeedOverTimeSlider.value + "";
    GameConfig.peopleSpeedIncreaseFactor = peopleSpeedOverTimeSlider.value;

  }

  void VillagersToggle(bool value) {

    GameConfig.peopleInGame = value;
    villagersGroup.gameObject.SetActive(value);

  }

  // WIZARD
  void OnWizardStartSpeedSlider() {

    wizardStartSpeedValue.text = peopleStartSpeedSlider.value + "";
    GameConfig.peopleSpeedStart = peopleStartSpeedSlider.value;

  }

  void OnWizardNumberPerMinSlider() {

    peopleNumberPerMinValue.text = peopleNumberPerMinSlider.value + "";
    GameConfig.peopleNumberPerMin = peopleNumberPerMinSlider.value;

  }

  void OnWizardOverTimeSlider() {

    peopleNumberOverTimeValue.text = peopleNumberOverTimeSlider.value + "";
    GameConfig.peopleSpeedIncreaseFactor = peopleNumberOverTimeSlider.value;

  }

  void OnWizardSpeedOverTimeSlider() {

    peopleSpeedOverTimeValue.text = peopleSpeedOverTimeSlider.value + "";
    GameConfig.peopleSpeedIncreaseFactor = peopleSpeedOverTimeSlider.value;

  }

  void BadWizardToggle(bool value) {

  	GameConfig.wizardInGame = value;
    // badWizardGroup.gameObject.SetActive(value);

  }

  // FLIES
  void OnFliesStartNumSlider() {

    fliesStartNumValue.text = fliesStartNumSlider.value + "";
    GameConfig.fliesNumberStart = fliesStartNumSlider.value;

  }

  void OnFliesStartSpeedSlider() {

    fliesStartSpeedValue.text = fliesStartSpeedSlider.value + "";
    GameConfig.fliesSpeedStart = fliesStartSpeedSlider.value;

  }

  void OnFliesNumberPerMinSlider() {

    fliesNumberPerMinValue.text = fliesNumberPerMinSlider.value + "";
    GameConfig.fliesNumberPerMin = fliesNumberPerMinSlider.value;

  }

  void OnFliesOverTimeSlider() {

    fliesNumberOverTimeValue.text = fliesNumberOverTimeSlider.value + "";
    GameConfig.fliesAmountIncreaseFactor = fliesNumberOverTimeSlider.value;

  }

  void OnFliesSpeedOverTimeSlider() {

    fliesSpeedOverTimeValue.text = fliesSpeedOverTimeSlider.value + "";
    GameConfig.fliesSpeedIncreaseFactor = fliesSpeedOverTimeSlider.value;

  }
  void FliesToggle(bool value) {

    GameConfig.fliesInGame = value;
    fliesGroup.gameObject.SetActive(value);

  }

  void OnStart() {
  	// GetComponent
  	gameObject.SetActive(false);
    spawnManager.gameObject.SetActive(true);
  	player.gameObject.SetActive(true);
  }

}