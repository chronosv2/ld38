﻿using System.Collections;
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
	public GameObject BombPrefab;
	public float powerUpRate = 0.75f;
	// Use this for initialization
	void Start () {
		eTgt = GetComponent<EnemyTargeting>();		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (eTgt.nothingToTarget) doNoTargetsLeft();
		if (eTgt.target == null) {
			eTgt.Retarget();
			return;
		}
		if (!timerStarted && BombTimer <= 0) {
		if (eTgt.target == null) return;			
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
		if (Health <= 0) {
			doDestroyShot();
		}
	}

	void doPlaceBomb() {
		//Instanciate a bomb.
		Instantiate(BombPrefab,transform.position,Quaternion.identity);
		BombTimer = BombPlaceTime;
		timerStarted = true;
		//Debug.Log("Placed a bomb.");
	}

	void doNoTargetsLeft() {
		Debug.Log(gameObject.name + " reports no targets left. Destroying.");
		doDestroy();
	}

	void doDestroyShot() {
		GameManager.giveScore(scoreValue);
		doPowerUpChance(powerUpRate);
		doDestroy();
	}

	void doDestroy() {
		Destroy(gameObject);
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet") {
			int Damage = other.gameObject.GetComponent<Projectile>().damageValue;
			Health -= Damage;
			Destroy(other.gameObject);
		}
	}

		void doPowerUpChance(float rate) {
		if (Random.value <= rate) {
			GameManager myGM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
			float pct = Random.value;
			if (pct < 0.37f) {
				Instantiate(myGM.PowerUps[0], transform.position, Quaternion.identity);
			} else if (pct < 0.74) {
				Instantiate(myGM.PowerUps[1], transform.position, Quaternion.identity);
			} else {
				Instantiate(myGM.PowerUps[2], transform.position, Quaternion.identity);
			}
		}
	}
}
