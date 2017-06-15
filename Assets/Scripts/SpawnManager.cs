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

	float NextSpawnInterval = 0.0f;
	float WaitTime = 0.0f;

	List<GameObject> objList;

	// Use this for initialization
	void Start () {

		NextSpawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);
		
		GameObject[] objArr = {EnemyObject, FriendObject, PowerupObject};
		objList = new List<GameObject>(objArr);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		
		GameObject objToSpawn;

		// Counts up
		WaitTime += Time.deltaTime;

		// Check if its the right time to spawn the object
		if(WaitTime >= NextSpawnInterval) {

			float randValue = Random.value;
			float speed = Random.Range(SpawnMoveSpeedMin, SpawnMoveSpeedMax);

			Vector3 randomPos = new Vector3(Random.Range(0, Screen.width), Screen.height, Camera.main.nearClipPlane);
			Vector3 pos = Camera.main.ScreenToWorldPoint(randomPos);
			pos.z = 0;

			if (randValue < .45f)
				objToSpawn = objList[0];
			else if (randValue < .9f)
				objToSpawn = objList[1];
			else {
				objToSpawn = objList[2];
				speed *= .5f;
			}

			GameObject spawn = Instantiate(objToSpawn, pos, Quaternion.identity);
			SpawnObject spawnScript = spawn.gameObject.GetComponent<SpawnObject>();

			spawnScript.MoveSpeed = speed;

			WaitTime = 0;
			NextSpawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);

		}

	}
}
