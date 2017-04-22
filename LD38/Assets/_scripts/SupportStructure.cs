using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportStructure : MonoBehaviour {

	public int Health = 100;
	public int loseTimeAmount = 4; // This value multiplied by LoseTimeStep for time lost when destroyed.
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0) {
			doAddLoseTime();
			doDestroy();
		}
	}

	void doAddLoseTime() {
		GameManager.addLoseTime(loseTimeAmount);
	}

	void doDestroy() {
		//TODO: Particle effect? Explosion sprite? Obviously play a sound!
		Destroy(gameObject);
	}
}
