using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {

	public float moveSpeed = 4f;
	public int BaseHealth = 8;
	public int Health = 8;
	public int scoreValue = 250;
	public int damageValue = 25;
	//EnemyTargeting eTgt;
	// Use this for initialization
	void Start () {
		int TimeElapsed = Mathf.FloorToInt(300 - GameManager.GameTimer);
		if (TimeElapsed < 60) {
			Health = BaseHealth;
		} else if (TimeElapsed < 120) {
			Health = BaseHealth * 2;
		} else if (TimeElapsed < 180) {
			Health = BaseHealth * 4;
		} else if (TimeElapsed < 240) {
			Health = BaseHealth * 8;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Attempt to collide with target.
		transform.position += transform.up * Time.deltaTime * moveSpeed;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		GameObject collGO = collision.collider.gameObject;
		Debug.Log("Collided with " + collGO.name);
		if (collGO.CompareTag("Target")) {
			if (collGO.name == "Player_Unit") {
				GameManager.damagePlayer(damageValue);
			} else {
				SupportStructure sStruct = collGO.GetComponent<SupportStructure>();
				sStruct.Health -= damageValue;
			}
			DoCollisionDestroy();
		}
	}

	void DoCollisionDestroy() {
		//Collision Destruction effects (particles? Explosion sprite?)
		Destroy(gameObject);
	}

	void DoShotDestroy() {
		//Shot Destruction effects (particles? Explosion sprite?)
		GameManager.giveScore(scoreValue);
		Destroy(gameObject);
	}
}
