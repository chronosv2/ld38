using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBomb : MonoBehaviour {

	public float TimeLimit = 5;
	[HideInInspector]
	public float Timer = 5;
	int Health = 30;
	int Damage = 100;
	int scoreValue = 1000;
	float Range = 1.0f;
	bool exploded = false;
	// Use this for initialization
	void Start () {
		Timer = TimeLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeLimit > 0) {
			Timer -= Time.deltaTime;
		} else {
			doExplode();
		}
	}

	void doExplode() {
		CircleCollider2D coll = GetComponent<CircleCollider2D>();
		coll.radius = Range;
	}
}
