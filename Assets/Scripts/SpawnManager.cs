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
	float WaitTime = 0.0f;
	float initialSpeedFactor = 1;

	List<GameObject> objList;

	// Use this for initialization
	void Start () {

		NextSpawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);
		
		GameObject[] objArr = {EnemyObject, FriendObject, PowerupObject};
		objList = new List<GameObject>(objArr);
		

	}

	void FixedUpdate() {
		
		GameObject objToSpawn;

		// Counts up
		WaitTime += Time.deltaTime;

		// Check if its the right time to spawn the object
		if(WaitTime >= 60/GameConfig.numObjects) {

			float randValue = Random.value;
			float speed = Random.Range(SpawnMoveSpeedMin, SpawnMoveSpeedMax);

			Vector3 randomPos = new Vector3(Random.Range(50, Screen.width-50), Screen.height, Camera.main.nearClipPlane);
			Vector3 pos = Camera.main.ScreenToWorldPoint(randomPos);
			pos.z = 0;

			if(!inBossBattle) {
		 
				if(randValue < GameConfig.powerUpChance) {
					objToSpawn = objList[2];
					speed *= .5f;
				}
				else if (randValue < .45f)
					objToSpawn = objList[0];
				else
					objToSpawn = objList[1];
			}			
			else {

				if (randValue < .7f)
					objToSpawn = objList[0];
				else
					objToSpawn = objList[1];
					
			}

			if(GameConfig.speedUpToggle)
				speed *= initialSpeedFactor;

			speed = Mathf.Clamp(speed, 0, topSpeed);

			GameObject spawn = Instantiate(objToSpawn, pos, Quaternion.identity);
			SpawnObject spawnScript = spawn.gameObject.GetComponent<SpawnObject>();

			spawnScript.MoveSpeed = speed * GameConfig.speedFactor;

			WaitTime = 0;
			NextSpawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);

			initialSpeedFactor += speedFactor;

			}
			else if(GameConfig.increaseToggle)
				GameConfig.numObjects += .03f;

		}

}
