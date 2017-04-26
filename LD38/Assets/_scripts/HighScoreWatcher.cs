using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreWatcher : MonoBehaviour {

	Text myText;
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Master.isLoaded) {
			myText.text = Master.HighScore.ToString();
		}
	}
}
