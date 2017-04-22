using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoseTimer : MonoBehaviour {
	Text myText;

	// Use this for initialization
	void Awake () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		float timeLeft = GameManager.LoseTimeRemaining * GameManager.LoseTimeStep;
		int MinutesLeft = Mathf.FloorToInt(timeLeft / 60);
		float SecondsLeft = timeLeft % 60; 
		string secondsLeftString = SecondsLeft.ToString("00.00");

		if (timeLeft <= 0) {
			myText.text = "";
		} else {
			myText.text = "-" + MinutesLeft.ToString() + ":" + secondsLeftString;
		}
	}
}
