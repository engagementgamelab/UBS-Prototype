using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	public GameObject EnemyObject;
	public GameObject FriendObject;
	public GameObject WizardObject;
	public GameObject WizardPeopleObject;
	public GameObject WizardPeopleFliesObject;

	public Button wizardSpawnButton;
	public Button wizardVillagersSpawnButton;
	public Button wizardVillagersFliesSpawnButton;

	public float SpawnIntervalMax = 2.0f;
	public float SpawnIntervalMin = 1.0f;

	public float SpawnMoveSpeedMax = 0.0f;
	public float SpawnMoveSpeedMin = 1.0f;
	public float speedFactor = .1f;
	public float topSpeed = 1f;

	public bool shootingMode = false;

	float NextSpawnInterval = 0.0f;
	float PersonWaitTime = 0.0f;
	float FlyWaitTime = 0.0f;
	float WizardWaitTime = 0.0f;
	float initialSpeedFactor = 1;

	List<GameObject> objList;

	void SpawnObject(float speed, GameObject objToSpawn) {

		Vector3 randomPos = new Vector3(Random.Range(shootingMode ? 150 : 50, Screen.width-(shootingMode ? 150 : 50)), Screen.height, Camera.main.nearClipPlane);
		Vector3 pos = Camera.main.ScreenToWorldPoint(randomPos);
		pos.z = 0;

		speed = Mathf.Clamp(speed, 0, topSpeed);

		GameObject spawnObj = Instantiate(objToSpawn, pos, Quaternion.identity);

		SpawnObject spawnScript = spawnObj.gameObject.GetComponent<SpawnObject>();

		if(spawnScript != null)
			spawnScript.MoveSpeed = speed;
	
	}

	// Use this for initialization
	void Start () {

		NextSpawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);
		
		GameObject[] objArr = {EnemyObject, FriendObject, WizardObject, WizardPeopleObject, WizardPeopleFliesObject};
		objList = new List<GameObject>(objArr);

		for(int i = 0; i < GameConfig.fliesNumberStart; i++)
			SpawnObject(Random.Range(0, GameConfig.fliesSpeedStart), objList[1]);		

		wizardSpawnButton.onClick.AddListener(delegate{SpawnObject(1, objList[2]);});
		wizardVillagersSpawnButton.onClick.AddListener(delegate{SpawnObject(1, objList[3]);});
		wizardVillagersFliesSpawnButton.onClick.AddListener(delegate{SpawnObject(1, objList[4]);});

	}

	void FixedUpdate() {

		if(!GameConfig.peopleInGame && !GameConfig.fliesInGame && !GameConfig.wizardInGame)
			return;

		// Counts up
		PersonWaitTime += Time.deltaTime;
		FlyWaitTime += Time.deltaTime;
		WizardWaitTime += Time.deltaTime;

		float speed = 0;
		float spawnTime = 0;
		float randValue = Random.value;
		bool spawn = false;
			
		GameObject objToSpawn = null;

		if(randValue >= GameConfig.wizardChance && GameConfig.wizardInGame) {
			objToSpawn = objList[2];

			speed = Random.Range(0, GameConfig.wizardSpeedStart);
			spawnTime = 60/GameConfig.wizardsNumberPerMin;
			
			spawn = (WizardWaitTime >= spawnTime);
			if(spawn)
				WizardWaitTime = 0;
		}
		else {
			
			if(randValue > .5f && GameConfig.fliesInGame) {
			
				objToSpawn = objList[1];
				
				speed = Random.Range(0, GameConfig.fliesSpeedStart);
				spawnTime = 60/GameConfig.fliesNumberPerMin;
				
				spawn = (FlyWaitTime >= spawnTime);
				if(spawn)
					FlyWaitTime = 0;
			
			}

			else if(GameConfig.peopleInGame) {
			
				objToSpawn = objList[0];
				
				speed = Random.Range(0, GameConfig.peopleSpeedStart);
				speed *= GameConfig.peopleSpeedCurrent;
				spawnTime = 60/GameConfig.peopleNumberPerMin;
				
				spawn = (PersonWaitTime >= spawnTime);
				if(spawn)
					PersonWaitTime = 0;
			
			}

		}

		// Check if its the right time to spawn the object
		if(spawn) {

			if(objToSpawn == null)
				return;

			SpawnObject(speed, objToSpawn);

			initialSpeedFactor += speedFactor;

		}
		else {
		
		 if(GameConfig.peopleAmountIncreaseFactor > 0)
			GameConfig.peopleNumberPerMin += GameConfig.peopleAmountIncreaseFactor;
		
		 if(GameConfig.fliesAmountIncreaseFactor > 0) 
			GameConfig.fliesNumberPerMin += GameConfig.fliesAmountIncreaseFactor;
		
		 if(GameConfig.wizardAmountIncreaseFactor > 0)
			GameConfig.wizardsNumberPerMin += GameConfig.wizardAmountIncreaseFactor;

		if(GameConfig.peopleSpeedIncreaseFactor > 0)
			GameConfig.peopleSpeedCurrent += GameConfig.peopleSpeedIncreaseFactor;

		}

	}

}
