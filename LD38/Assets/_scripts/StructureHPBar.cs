using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureHPBar : MonoBehaviour {

	Slider myHPBar;
	SupportStructure mySS;

	// Use this for initialization
	void Start () {
		myHPBar = gameObject.GetComponentInChildren<Slider>();
		mySS = gameObject.GetComponent<SupportStructure>();
		myHPBar.maxValue = mySS.Health;
	}
	
	// Update is called once per frame
	void Update () {
		myHPBar.value = mySS.Health;
	}
}
