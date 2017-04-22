using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float SpawnTimer = 3f;
	float timer = 0;
	float randomVariance = 0.3f;
	public GameObject EnemyType;

	// Use this for initialization
	void Start () {
		if (EnemyType == null) {
			Debug.LogError("The Spawner \""+gameObject.name+"\" does not have an Enemy Type assigned to it!");
			Destroy(gameObject);
		}
		timer = SpawnTimer * Random.Range(1-randomVariance, 1+randomVariance);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0) {
			doSpawnEnemy();
			doResetTimer();
		} else {
			timer -= Time.deltaTime;
		}

	}

	void doResetTimer() {
		int timeElapsed = Mathf.FloorToInt(300 - GameManager.GameTimer);
		timer = SpawnTimer * Random.Range(1-randomVariance, 1+randomVariance);
		if (timeElapsed < 60) {
			return;
		} else if (timeElapsed < 120) {
			timer *= 0.875f;
		} else if (timeElapsed < 180) {
			timer *= 0.75f;
		} else if (timeElapsed < 240) {
			timer *= 0.625f;
		}
	}

	void doSpawnEnemy() {
		Debug.Log("Spawned " + EnemyType.name);
		Instantiate(EnemyType,transform.position,transform.rotation);
	}
}
