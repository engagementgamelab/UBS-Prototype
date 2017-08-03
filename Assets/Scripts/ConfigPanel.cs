using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour {

  [Header("Bubbles")]
  public Text bubblesToStartValue;
  public Text bubblesGainedValue;
  public Text bubblesGainedSpeedValue;
  public Text bubblesIntervalValue;
  public Text bubblesCapacityValue;

  public Slider bubblesToStartSlider;
  public Slider bubblesGainedSlider;
  public Slider bubblesGainedSpeedSlider;
  public Slider bubblesIntervalSlider;
  public Slider bubblesCapacitySlider;
  
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
  public Toggle wizardMovementToggle;
  public Slider wizardStartSpeedSlider;
  public Slider wizardNumberPerMinSlider;
  public Slider wizardSpeedOverTimeSlider;
  public Slider wizardNumberOverTimeSlider;

  public Text wizardStartSpeedValue;
  public Text wizardNumberPerMinValue;
  public Text wizardNumberOverTimeValue;
  public Text wizardSpeedOverTimeValue;

  public RectTransform wizardGroup;

  [Header("Powerups")]
  public Toggle powerUpToggle;
  public Slider powerUpChanceSlider;
  public Slider powerUpPerMinSlider;
  public Text powerUpChanceValue;
  public Text powerUpPerMinValue;

  [Header("Poop")]
  public Toggle poopToggle;
  public Slider poopSpeedSlider;
  public Slider poopSizeSlider;
  public Slider poopPerMinSlider;
  public Slider poopChanceSlider;
  public Text poopSpeedValue;
  public Text poopSizeValue;
  public Text poopPerMinValue;
  public Text poopChanceValue;

  [Header("Game")]
  public Button startButton;

  public RectTransform badWizardGroup;
  public SpawnManager spawnManager;
  GameObject player;

	// Use this for initialization
	void Start () {
	
	  player = GameObject.Find("Player");

    if(bubblesCapacitySlider != null)
      bubblesCapacitySlider.onValueChanged.AddListener(delegate {OnBubblesCapacitySlider(); });
    
    peopleStartSpeedSlider.onValueChanged.AddListener(delegate {OnPeopleStartSpeedSlider(); });
    peopleNumberPerMinSlider.onValueChanged.AddListener(delegate {OnPeopleNumberPerMinSlider(); });
    peopleNumberOverTimeSlider.onValueChanged.AddListener(delegate {OnPeopleOverTimeSlider(); });
		peopleSpeedOverTimeSlider.onValueChanged.AddListener(delegate {OnPeopleSpeedOverTimeSlider(); });
    
    fliesStartNumSlider.onValueChanged.AddListener(delegate {OnFliesStartNumSlider(); });
    fliesStartSpeedSlider.onValueChanged.AddListener(delegate {OnFliesStartSpeedSlider(); });
    fliesNumberPerMinSlider.onValueChanged.AddListener(delegate {OnFliesNumberPerMinSlider(); });
    fliesNumberOverTimeSlider.onValueChanged.AddListener(delegate {OnFliesOverTimeSlider(); });
    fliesSpeedOverTimeSlider.onValueChanged.AddListener(delegate {OnFliesSpeedOverTimeSlider(); });
    
    wizardStartSpeedSlider.onValueChanged.AddListener(delegate {OnWizardStartSpeedSlider(); });
    wizardNumberPerMinSlider.onValueChanged.AddListener(delegate {OnWizardNumberPerMinSlider(); });
    // wizardNumberOverTimeSlider.onValueChanged.AddListener(delegate {OnWizardOverTimeSlider(); });
    wizardSpeedOverTimeSlider.onValueChanged.AddListener(delegate {OnWizardSpeedOverTimeSlider(); });

    GameConfig.peopleInGame = villagersToggle.isOn;
    GameConfig.wizardInGame = badWizardToggle.isOn;
    GameConfig.fliesInGame = fliesToggle.isOn;
    
    if(poopToggle != null)
	    GameConfig.poopInGame = poopToggle.isOn;

		startButton.onClick.AddListener(OnStart);

    badWizardToggle.onValueChanged.AddListener(BadWizardToggle);

    if(wizardMovementToggle != null)
  		wizardMovementToggle.onValueChanged.AddListener(WizardMovementToggle);

	  if(powerUpToggle != null)
	  {
	    powerUpToggle.onValueChanged.AddListener(OnPowerUpToggle);
	    GameConfig.powerUpsInGame = powerUpToggle.isOn;
	  }

	  villagersToggle.onValueChanged.AddListener(VillagersToggle);
		fliesToggle.onValueChanged.AddListener(FliesToggle);
	  
	  // HACK
	  gameObject.SetActive(false);
    
	  if(spawnManager != null)
	    spawnManager.gameObject.SetActive(true);
  
	  player.gameObject.SetActive(true);
	  GameConfig.gamePaused = false;
		
	}

	void OnBubblesToStartSlider() {

  	bubblesToStartValue.text = bubblesToStartSlider.value + "";
  	GameConfig.numBubblesToStart = bubblesToStartSlider.value;

  }

	void OnBubblesGainedSlider() {

  	bubblesGainedValue.text = bubblesGainedSlider.value + "";
  	GameConfig.numBubblesGained = bubblesGainedSlider.value;

  }

  public void OnBubblesGainedSpeedSlider() {

    bubblesGainedSpeedValue.text = bubblesGainedSpeedSlider.value + "";
    GameConfig.numBubblesSpeedGained = bubblesGainedSpeedSlider.value;

  }

  public void OnBubblesIntervalSlider() {

    bubblesIntervalValue.text = bubblesIntervalSlider.value + "";
    GameConfig.numBubblesInterval = bubblesIntervalSlider.value;

  }

  void OnBubblesCapacitySlider() {

    bubblesCapacityValue.text = bubblesCapacitySlider.value + "";
    GameConfig.numBubblesFull = bubblesCapacitySlider.value;

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

    wizardStartSpeedValue.text = wizardStartSpeedSlider.value + "";
    GameConfig.wizardSpeedStart = wizardStartSpeedSlider.value;

  }

  void OnWizardNumberPerMinSlider() {

    wizardNumberPerMinValue.text = wizardNumberPerMinSlider.value + "";
    GameConfig.wizardsNumberPerMin = wizardNumberPerMinSlider.value;

  }

  void OnWizardOverTimeSlider() {

    wizardNumberOverTimeValue.text = wizardNumberOverTimeSlider.value + "";
    GameConfig.wizardAmountIncreaseFactor = wizardNumberOverTimeSlider.value;

  }

  void OnWizardSpeedOverTimeSlider() {

    wizardSpeedOverTimeValue.text = wizardSpeedOverTimeSlider.value + "";
    GameConfig.wizardSpeedIncreaseFactor = wizardSpeedOverTimeSlider.value;

  }

  void BadWizardToggle(bool value) {

  	GameConfig.wizardInGame = value;
    // badWizardGroup.gameObject.SetActive(value);

  }
  
  void WizardMovementToggle(bool value) {

    GameConfig.wizardFloatMovement = value;

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

  public void OnPowerUpChanceSlider() {

    powerUpChanceValue.text = powerUpChanceSlider.value + "";
    GameConfig.powerUpChance = powerUpChanceSlider.value;

  }

  public void OnPowerUpPerMinSlider() {

    powerUpPerMinValue.text = powerUpPerMinSlider.value + "";
    GameConfig.powerUpNumberPerMin = powerUpPerMinSlider.value;

  }

  // POOP
  public void OnPoopSpeedSlider() {

    poopSpeedValue.text = poopSpeedSlider.value + "";
    GameConfig.numPoopSpeed = poopSpeedSlider.value;

  }

  public void OnPoopSizeSlider() {

    poopSizeValue.text = poopSizeSlider.value + "";
    GameConfig.numPoopSize = poopSizeSlider.value;

  }

  public void OnPoopChanceSlider() {

    poopChanceValue.text = poopChanceSlider.value + "";
    GameConfig.poopChance = poopChanceSlider.value;

  }

  public void OnPoopPerMinSlider() {

    poopPerMinValue.text = poopPerMinSlider.value + "";
    GameConfig.numPoopPerMin = poopPerMinSlider.value;

  }

  void OnPowerUpToggle(bool value)
  {
    GameConfig.powerUpsInGame = value;
  }


  void OnStart() {
  	// GetComponent
  	gameObject.SetActive(false);
    
    if(spawnManager != null)
      spawnManager.gameObject.SetActive(true);
  
    player.gameObject.SetActive(true);
    GameConfig.gamePaused = false;
  }

}