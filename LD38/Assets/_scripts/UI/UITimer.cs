using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour {
	Text myText;

	// Use this for initialization
	void Awake () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		float timeLeft = GameManager.GameTimer;
		int MinutesLeft = Mathf.FloorToInt(timeLeft / 60);
		float SecondsLeft = timeLeft % 60; 
		if (MinutesLeft <= 0) MinutesLeft = 0;
		if (SecondsLeft <= 0) SecondsLeft = 0;
		string secondsLeftString = SecondsLeft.ToString("00.00");

		myText.text = MinutesLeft.ToString() + ":" + secondsLeftString;
	}
}
