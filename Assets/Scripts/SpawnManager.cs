using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public GameObject EnemyObject;
	public GameObject FriendObject;
	public GameObject PowerupObject;

	public float SpawnIntervalMax = 2.0f;
	public float SpawnIntervalMin = 1.0f;

	public float SpawnMoveSpeedMax = 0.0f;
	public float SpawnMoveSpeedMin = 1.0f;
	public float speedFactor = .1f;
	public float topSpeed = 1f;

	public bool inBossBattle = false;

	float NextSpawnInterval = 0.0f;
	float PersonWaitTime = 0.0f;
	float FlyWaitTime = 0.0f;
	float WizardWaitTime = 0.0f;
	float initialSpeedFactor = 1;

	List<GameObject> objList;

	// Use this for initialization
	void Start () {

		NextSpawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);
		
		GameObject[] objArr = {EnemyObject, FriendObject, PowerupObject};
		objList = new List<GameObject>(objArr);
		

	}

		// bool allSpawns = GameConfig.peopleInGame && GameConfig.fliesInGame && GameConfig.wizardInGame;
		// bool peopleAndFlies = GameConfig.peopleInGame && GameConfig.fliesInGame && !GameConfig.wizardInGame;
		// bool peopleAndWizard = GameConfig.peopleInGame && GameConfig.wizardInGame;
		// bool fliesAndWizard = GameConfig.fliesInGame && GameConfig.wizardInGame;

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
			
			if (randValue > .5f && GameConfig.fliesInGame) {
				objToSpawn = objList[1];
				
				speed = Random.Range(0, GameConfig.fliesSpeedStart);
				spawnTime = 60/GameConfig.fliesNumberPerMin;
				
				spawn = (FlyWaitTime >= spawnTime);
				if(spawn)
					FlyWaitTime = 0;
			}
			else {
				objToSpawn = objList[0];
				
				speed = Random.Range(0, GameConfig.peopleSpeedStart);
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

			Vector3 randomPos = new Vector3(Random.Range(50, Screen.width-50), Screen.height, Camera.main.nearClipPlane);
			Vector3 pos = Camera.main.ScreenToWorldPoint(randomPos);
			pos.z = 0;
	
			// if(GameConfig.peopleInGame) {
					
			// }
			// else {
		 
			// 	
			// 	else if (randValue < .45f)
			// 		objToSpawn = objList[0];
			// 	else
			// 		objToSpawn = objList[1];

			// }		
			
			// if(GameConfig.peopleSpeedIncreaseFactor > 1)
			// 	speed *= GameConfig.peopleSpeedIncreaseFactor;

			speed = Mathf.Clamp(speed, 0, topSpeed);

			GameObject spawnObj = Instantiate(objToSpawn, pos, Quaternion.identity);

			SpawnObject spawnScript = spawnObj.gameObject.GetComponent<SpawnObject>();
			spawnScript.MoveSpeed = speed * GameConfig.peopleSpeedStart;

			// WaitTime = 0;

			initialSpeedFactor += speedFactor;

		}
		else {
		
		 if(GameConfig.peopleAmountIncreaseFactor > 0)
			GameConfig.peopleNumberPerMin += GameConfig.peopleAmountIncreaseFactor;
		
		 if(GameConfig.fliesAmountIncreaseFactor > 0)
			GameConfig.fliesNumberPerMin += GameConfig.fliesAmountIncreaseFactor;
		
		 if(GameConfig.wizardAmountIncreaseFactor > 0)
			GameConfig.wizardsNumberPerMin += GameConfig.wizardAmountIncreaseFactor;

		}

	}

}
