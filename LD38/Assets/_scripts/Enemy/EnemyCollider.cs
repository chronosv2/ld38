using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {

	public float moveSpeed = 4f;
	public int BaseHealth = 8;
	public int Health = 8;
	public int scoreValue = 250;
	public int damageValue = 25;
	float BasePowerUpRate = 0.2f;
	float powerUpRate;
	public AudioClip collidedClip;
	EnemyTargeting eTgt;
	//EnemyTargeting eTgt;
	// Use this for initialization
	void Start () {
		int TimeElapsed = Mathf.FloorToInt(300 - GameManager.GameTimer);
		eTgt = GetComponent<EnemyTargeting>();
		if (TimeElapsed < 60) {
			Health = BaseHealth;
			powerUpRate = BasePowerUpRate * 1;
		} else if (TimeElapsed < 120) {
			Health = BaseHealth * 2;
			powerUpRate = BasePowerUpRate * 2;
		} else if (TimeElapsed < 180) {
			Health = BaseHealth * 4;
			powerUpRate = BasePowerUpRate * 3;
		} else if (TimeElapsed < 240) {
			Health = BaseHealth * 8;
			powerUpRate = BasePowerUpRate * 4;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Attempt to collide with target.
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (eTgt.target.gameObject.name.Contains("Player")) {
			if (eTgt.target.gameObject.GetComponent<PlayerHealth>().playerIsDestroyed) {
				transform.position += -transform.up * Time.deltaTime * (moveSpeed/2);
				return;
			}
		}
		transform.position += transform.up * Time.deltaTime * moveSpeed;
		if (Health <= 0) {
			DoShotDestroy();
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		GameObject collGO = collision.collider.gameObject;
		Debug.Log("Collided with " + collGO.name);
		if (collGO.CompareTag("Target")) {
			if (collGO.name == "Player_Unit") {
				collGO.GetComponent<PlayerHealth>().doColliderHit();
				GameManager.damagePlayer(damageValue);
			} else {
				SupportStructure sStruct = collGO.GetComponent<SupportStructure>();
				sStruct.Health -= damageValue;
			}
			DoCollisionDestroy();
		} else if (collGO.CompareTag("Bullet")) {
			int Damage = collGO.GetComponent<Projectile>().damageValue;
			Health -= Damage;			
			Destroy(collGO);
		}
	}

	void DoCollisionDestroy() {
		//Collision Destruction effects (particles? Explosion sprite?)
		PlayClipAt(collidedClip, transform.position);
		Destroy(gameObject);
	}

	void DoShotDestroy() {
		//Shot Destruction effects (particles? Explosion sprite?)
		GameManager.giveScore(scoreValue);
		doPowerUpChance(powerUpRate);
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

	AudioSource PlayClipAt(AudioClip clip, Vector3 pos){
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.rolloffMode = AudioRolloffMode.Linear;
		// set other aSource properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}
}
