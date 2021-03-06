﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterMovement : MonoBehaviour {

	public float movementPhaseTime = 4.0f;
	public float randomVariance = 0.25f;
	public float moveSpeed = 4f;
	float realSpeed;
	float timer;
	int Direction = 0;
	public int BaseHealth = 8;
	public int Health = 8;
	public int scoreValue = 500;
	public float powerUpRate = 0.4f;
	GameObject playerGO;

	// Use this for initialization
	void Start () {
		realSpeed = moveSpeed;
		Direction = Mathf.RoundToInt(Random.Range(0f,1.01f));
		Debug.Log(Direction);
		timer = movementPhaseTime * Random.Range(1-randomVariance, 1+randomVariance);
		playerGO = GameObject.Find("Player_Unit");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (timer <= 0) {
			doResetTimer();
		} else {
			timer -= Time.deltaTime;
			doMovement();
		}		
			if (Health <= 0) {
			doDestroyShot();
		}
}

	void doMovement() {
		Vector3 keepinRange = new Vector3();
		float DistanceToPlayer = Vector3.Distance(transform.position,playerGO.transform.position);
		if (DistanceToPlayer >= 8.25) {
			keepinRange = transform.up;
		} else if (DistanceToPlayer <= 7.75) {
			keepinRange = -transform.up;
		} else {
			keepinRange = Vector3.zero;
		}
		switch(Direction) {
			case 1:
				transform.position += (transform.right + keepinRange) * Time.deltaTime * realSpeed;		
			break;
			case 0:
				transform.position += (-transform.right + keepinRange) * Time.deltaTime * realSpeed;		
			break;
		}
	}

	void doResetTimer() {
		Debug.Log("Generating new movement choice.");
		int timeElapsed = Mathf.FloorToInt(300 - GameManager.GameTimer);
		timer = movementPhaseTime * Random.Range(1-randomVariance, 1+randomVariance);
		Direction = Mathf.RoundToInt(Random.Range(0f,1.01f));
		Debug.Log(Direction);
		if (timeElapsed < 60) {
			return;
		} else if (timeElapsed < 120) {
			timer *= 0.875f;
			realSpeed = moveSpeed * 1.25f;
		} else if (timeElapsed < 180) {
			timer *= 0.75f;
			realSpeed = moveSpeed * 1.5f;
		} else if (timeElapsed < 240) {
			timer *= 0.625f;
			realSpeed = moveSpeed * 1.75f;
		}
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

	void doDestroyShot() {
		GameManager.giveScore(scoreValue);
		doPowerUpChance(powerUpRate);
		doDestroy();
	}

	void doDestroy() {
		Destroy(gameObject);
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
