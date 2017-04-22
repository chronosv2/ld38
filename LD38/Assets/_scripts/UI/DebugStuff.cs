using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DebugStuff : MonoBehaviour {
	public InputField loseTimeInput;

	void ToggleIsPlaying() {
		GameManager.isPlaying = !GameManager.isPlaying;
	}

	void AddLoseTime() {
		int Amt = Convert.ToInt32(loseTimeInput.text);
		GameManager.addLoseTime(Amt);
	}
}
