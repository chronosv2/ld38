using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : MonoBehaviour {

	public float moveSpeed = 4f;
	public int BaseHealth = 14;
	public int Health = 14;
	public int scoreValue = 750;
	EnemyTargeting eTgt;
	bool timerStarted = false;
	public float BombPlaceTime = 1.0f;
	float BombTimer = 0;
	// Use this for initialization
	void Start () {
		eTgt = GetComponent<EnemyTargeting>();		
	}
	
	// Update is called once per frame
	void Update () {
		if (eTgt.nothingToTarget) doNoTargetsLeft();
		if (eTgt.target == null) eTgt.Retarget();
		if (!timerStarted && BombTimer <= 0) {
			if (Vector3.Distance(transform.position,eTgt.target.position) < 1) {
				doPlaceBomb();
				eTgt.Retarget();
				if (eTgt.target == null) doNoTargetsLeft();
			} else {
				transform.position += transform.up * Time.deltaTime * moveSpeed;		
			}
		} else {
			if (timerStarted && BombTimer > 0) {
				BombTimer -= Time.deltaTime;
			} else {
				timerStarted = false;
				BombTimer = 0;
			}
		}
	}

	void doPlaceBomb() {
		//Instanciate a bomb.
		BombTimer = BombPlaceTime;
		timerStarted = true;
		Debug.Log("Placed a bomb.");
	}

	void doNoTargetsLeft() {
		Debug.Log(gameObject.name + " reports no targets left. Destroying.");
		doDestroy();
	}

	void doDestroyShot() {

	}

	void doDestroy() {
		Destroy(gameObject);
	}
}
