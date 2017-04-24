using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {

	Slider mySlider;
	// Use this for initialization
	void Start () {
		mySlider = GetComponent<Slider>();
		mySlider.value = AudioListener.volume;
	}
	
	public void onSliderChanged() {
		AudioListener.volume = mySlider.value;
	}
}
