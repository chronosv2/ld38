using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	static int Score;
	static int PlayerHealth;
	static float GameTimer;
	static int WorldState;
	// Use this for initialization
	void Awake () {
		Score = 0;
		PlayerHealth = 100;
		GameTimer = 300;
		WorldState = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
