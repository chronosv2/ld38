using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour {

	public static bool isLoaded;
	static Master instance;
	public static int HighScore = 25000;

	// Use this for initialization
	void Start () {
		Random.InitState((int)System.DateTime.Now.Ticks);
		if (instance != null) {
			DestroyObject (gameObject);
			//			Debug.Log ("Destroyed duplicate GameStatus Object.");
		} else {
			instance = this;
			Master.isLoaded = true;
			GameObject.DontDestroyOnLoad(gameObject);		
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Cursor.lockState = CursorLockMode.Confined;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void StartGame() {
		SceneManager.LoadScene("main");
	}
}
