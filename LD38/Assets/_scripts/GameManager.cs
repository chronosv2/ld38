using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static int Score;
	public static int PlayerHealth;
	public static float GameTimer;
	public static int WorldState;
	public static float LoseTimeStep;
	public static float LoseTimeRemaining { get; private set; }
	public static int HighScore { get; private set; }
	public static bool isPlaying = false;
	// Use this for initialization
	void Awake () {
		Score = 0;
		PlayerHealth = 100;
		GameTimer = 300;
		LoseTimeStep = 3;
		LoseTimeRemaining = 0;
		WorldState = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying) {
			if (LoseTimeRemaining > 0) {
				GameTimer -= Time.deltaTime * LoseTimeStep;
				LoseTimeRemaining -= Time.deltaTime;
			} else {
				GameTimer -= Time.deltaTime;
			}

			if (GameTimer <= 0) {
				isPlaying = false;
				doGameOver();
			}
		}
	}

	public static void NewGame() {
		Score = 0;
		PlayerHealth = 100;
		GameTimer = 300;
		LoseTimeStep = 3;
		LoseTimeRemaining = 0;
		WorldState = 0;
	}

	void SavePrefs() {

	}

	void LoadPrefs() {
		if (PlayerPrefs.HasKey ("HighScore")) {
			GameManager.HighScore = PlayerPrefs.GetInt ("HighScore");
		}		
	}

	void doGameOver() {
		//TODO: Add Game Over stuff.
	}

	public static void addLoseTime(int seconds) {
		LoseTimeRemaining += seconds;
	}
}
