using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DebugStuff : MonoBehaviour {
	public InputField loseTimeInput;
	public Text loseTimeMinSec;

	public void ToggleIsPlaying() {
		GameManager.isPlaying = !GameManager.isPlaying;
	}

	public void AddLoseTime() {
		int Amt = Convert.ToInt32(loseTimeInput.text);
		GameManager.addLoseTime(Amt);
	}

	public void onLoseTimeInputChange() {
		try {
			float timeLeft = Convert.ToInt32(loseTimeInput.text) * GameManager.LoseTimeStep;
			int MinutesLeft = Mathf.FloorToInt(timeLeft / 60);
			float SecondsLeft = timeLeft % 60;
			string secondsLeftString = SecondsLeft.ToString("00.00");

			loseTimeMinSec.text = MinutesLeft.ToString() + ":" + secondsLeftString;
		} catch {
			loseTimeMinSec.text = "0:00.00";
			return;
		}
	}
}
