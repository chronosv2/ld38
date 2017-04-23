using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static int Score {get; private set;}
	public static int PlayerHealth {get; private set;}
	public static float GameTimer {get; private set;}
	public static int WorldState;
	public static float LoseTimeStep {get; private set;}
	public int loseTimeStep = 3;
	public static float LoseTimeRemaining { get; private set; }
	public static bool isPlaying = false;
	public static bool isPreGame = true;
	static float PreGameTimer = 8;
	static float timer;
	static bool gameStarted = false;
	public static bool isPaused = false;
	public static string GameUpdateDisplay = "GET!READY";
	static bool gameOver = false;
	public static int GunRate = 1;
	public static int GunDamage = 1;
	public static bool GunSpread = false;
	public GameObject[] PowerUps;
	static bool foundMaster = false;
	public GameObject toTsUI;
	public AudioClip GameTheme;
	public AudioClip LowBeep;
	public AudioClip HighBeep;
	public AudioClip GameOverTheme;
	bool[] playedTone;
	AudioSource mySource;
	bool playedGameOver = false;

	// Use this for initialization
	void Awake () {
		if (Master.isLoaded) foundMaster = true;
		Score = 0;
		PlayerHealth = 100;
		GameTimer = 300;
		LoseTimeStep = loseTimeStep;
		LoseTimeRemaining = 0;
		WorldState = 0;
		//isPlaying = true;
		timer = PreGameTimer;
		playedTone = new bool[] {false, false, false, false};
		mySource = GetComponent<AudioSource>();
		mySource.loop = false;
	}
	
	// Update is called once per frame
	// Manages the Timer (and accelerated timer with LoseTimer!)
	void Update () {
		if (gameOver) return;
		if (Input.GetMouseButtonDown(0)) {
			Cursor.lockState = CursorLockMode.Confined;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
		}
		if (isPreGame) {
			if (timer > 0) {
				if (timer < 1) {
					if (!playedTone[1]) {
						mySource.PlayOneShot(LowBeep,0.7f);
						playedTone[1] = true;
					}
					GameUpdateDisplay = "1";
				} else if (timer < 2) {
					if (!playedTone[2]) {
						mySource.PlayOneShot(LowBeep,0.7f);
						playedTone[2] = true;
					}
					GameUpdateDisplay = "2";
				} else if (timer < 3) {
					if (!playedTone[3]) {
						mySource.PlayOneShot(LowBeep,0.7f);
						playedTone[3] = true;
					}
					GameUpdateDisplay = "3";
				} else {
					GameUpdateDisplay = "GET!READY";
				} 

				timer -= Time.deltaTime;
			} else {
				if (!playedTone[0]) {
					mySource.PlayOneShot(HighBeep,0.7f);
					playedTone[0] = true;
					mySource.clip = GameTheme;
					mySource.loop = true;
					mySource.Play();
				}
				isPreGame = false;
				if (gameStarted == false) {
					gameStarted = true;
					isPlaying = true;
				}
				GameUpdateDisplay = "";
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				isPaused = !isPaused;
			}
			if (isPaused) {
				GameUpdateDisplay = "PAUSED";
				isPlaying = false;
				return;
			} else {
				GameUpdateDisplay = "";
				if (GameTimer > 0) {
					isPlaying = true;
				}
			}
		}
		if (isPlaying) {
			if (LoseTimeRemaining > 0) {
				GameTimer -= Time.deltaTime * LoseTimeStep;
				LoseTimeRemaining -= Time.deltaTime;
			} else {
				GameTimer -= Time.deltaTime;
			}
		}
		if (GameTimer <= 0) {
			doGameOver();
		}
	}

	public static void giveScore(int amount) {
		Score += amount;
		if (foundMaster) {
			if (Score > Master.HighScore) {
				Master.HighScore = Score;
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
		isPreGame = true;
		isPlaying = false;
		gameStarted = false;
	}

	public static void damagePlayer(int amount) {
		PlayerHealth -= amount;
		// if (PlayerHealth <= 0) {
		// 	doGameOver();
		// }
	}

	public static void setHealth(int amount) {
		PlayerHealth = amount;
	}

	void doGameOver() {
		isPlaying = false;
		toTsUI.SetActive(true);
		GameUpdateDisplay = "Game!Over";
		gameOver = true;
		if (!playedGameOver) {
			mySource.Stop();
			mySource.clip = GameOverTheme;
			mySource.loop = false;
			mySource.Play();
		}
		//TODO: Add Game Over stuff.
	}

	public static void addLoseTime(int seconds) {
		LoseTimeRemaining += seconds;
	}

	public void toTitleScreen() {
		SceneManager.LoadScene("title");
	}
}
