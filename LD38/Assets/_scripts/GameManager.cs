using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static int Score {get; private set;}
	public static int PlayerHealth {get; private set;}
	public static float GameTimer {get; private set;}
	public static int WorldState;
	public static float LoseTimeStep {get; private set;}
	public int loseTimeStep = 3;
	public static float LoseTimeRemaining { get; private set; }
	public static int HighScore { get; private set; }
	public static bool isPlaying = false;
	// Use this for initialization
	void Awake () {
		Score = 0;
		PlayerHealth = 100;
		GameTimer = 300;
		LoseTimeStep = loseTimeStep;
		LoseTimeRemaining = 0;
		WorldState = 0;
	}
	
	// Update is called once per frame
	// Manages the Timer (and accelerated timer with LoseTimer!)
	void Update () {
		if (isPlaying) {
			if (LoseTimeRemaining > 0) {
				GameTimer -= Time.deltaTime * LoseTimeStep;
				LoseTimeRemaining -= Time.deltaTime;
			} else {
				GameTimer -= Time.deltaTime;
			}

			if (GameTimer <= 0) {
				doGameOver();
			}
		}
	}

	public static void giveScore(int amount) {
		Score += amount;
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

	public static void damagePlayer(int amount) {
		PlayerHealth -= amount;
		if (PlayerHealth <= 0) {
			doGameOver();
		}
	}

	void LoadPrefs() {
		if (PlayerPrefs.HasKey ("HighScore")) {
			GameManager.HighScore = PlayerPrefs.GetInt ("HighScore");
		}		
	}

	static void doGameOver() {
		isPlaying = false;
		//TODO: Add Game Over stuff.
	}

	public static void addLoseTime(int seconds) {
		LoseTimeRemaining += seconds;
	}
}
