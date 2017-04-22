using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour {

	Image myImage;
	int HealthMax;
	GameObject playerGO;
	PlayerHealth pHealth;

	// Use this for initialization
	void Start () {
		myImage = gameObject.GetComponent<Image>();
		HealthMax = GameManager.PlayerHealth;
		playerGO = GameObject.Find("Player_Unit");
		pHealth = playerGO.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pHealth.playerIsDestroyed) {
			if (GameManager.PlayerHealth > 0) {
				myImage.fillAmount = Mathf.Clamp((GameManager.PlayerHealth * 1.0f) / HealthMax,0f,1.0f);
			} else {
				myImage.fillAmount = 0;
			}
		} else {
			myImage.fillAmount = Mathf.Clamp(pHealth.timer / pHealth.RespawnTimer,0f,1.0f);
		}
	}
}
