using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour {

	Text myText;
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		int FireRate = GameManager.GunRate;
		int fireDamage = GameManager.GunDamage;
		bool fireSpread = GameManager.GunSpread;
		string spreadText = "Normal";
		if (fireSpread) {
			spreadText = "<color=cyan>Spread</color>";
		}

		myText.text = spreadText+"!D:"+fireDamage.ToString()+"!R:"+FireRate.ToString();
	}
}
