using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int damageValue = 5;
	public float moveSpeed = 18;
	float lifetime = 4f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = lifetime;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		transform.position += transform.up*moveSpeed*Time.deltaTime;
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Destroy(gameObject);
		}
	}
}
