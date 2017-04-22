using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DebugStuff : MonoBehaviour {
	public InputField loseTimeInput;

	public void ToggleIsPlaying() {
		GameManager.isPlaying = !GameManager.isPlaying;
	}

	public void AddLoseTime() {
		int Amt = Convert.ToInt32(loseTimeInput.text);
		GameManager.addLoseTime(Amt);
	}
}
