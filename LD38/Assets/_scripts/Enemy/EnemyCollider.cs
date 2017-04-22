using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {

	public float TargetLockTimer = 2.5f;
	public static Support_Structure[] supportStructures;

	// Use this for initialization
	void Start () {
		//Get a list of Player object and Support Structures
		if (supportStructures.Length == 0) {
			supportStructures = FindObjectsOfType<Support_Structure>() as Support_Structure[];
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Attempts to ram the player or closest object. Has a TargetCooldown value that keeps it from instantly switching between targets.
	}
}
