using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBomb : MonoBehaviour {

	public float TimeLimit = 5;
	[HideInInspector]
	public float Timer = 5;
	public int Health = 30;
	public int Damage = 100;
	public int scoreValue = 1000;
	public GameObject explosionPrefab;
	float Range = 1.0f;
	//bool exploded = false;
	// Use this for initialization
	void Awake () {
		Timer = TimeLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (Timer > 0) {
			Timer -= Time.deltaTime;
		} else {
			doExplode();
		}
		if (Health <= 0) {
			doDestroy();
		}
	}

	void doExplode() {
		//CircleCollider2D coll = GetComponent<CircleCollider2D>();
		GameObject Explode = Instantiate(explosionPrefab,transform.position,Quaternion.identity) as GameObject;
		Explode.GetComponent<BombExplosion>().Damage = Damage;
		Explode.GetComponent<BombExplosion>().Range = Range;
		Destroy(gameObject);
	}

	void doDestroy() {
		Destroy(gameObject);
		GameManager.giveScore(scoreValue);
	}
}
