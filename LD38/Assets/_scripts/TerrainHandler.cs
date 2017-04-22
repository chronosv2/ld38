using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHandler : MonoBehaviour {

	public GameObject WarnZone;
	public GameObject Phase1Ground;
	public GameObject Phase2Ground;
	public GameObject Phase3Ground;
	public GameObject Phase4Ground;
	public GameObject Phase5Ground;
	SpriteRenderer WarnRenderer;
	bool[] WorldStates;

	// Use this for initialization
	void Start () {
		WarnRenderer = WarnZone.GetComponent<SpriteRenderer>();
		WorldStates = new bool[4] {false, false, false, false};
	}
	
	// Update is called once per frame
	void Update () {
		switch (GameManager.WorldState) {
			case 1:
				if (!WorldStates[0]) {
					//TODO: WorldState 1 stuff
				}
			break;
			case 2:
				if (!WorldStates[1]) {
					//TODO: WorldState 2 stuff
				}
			break;
			case 3:
				if (!WorldStates[2]) {
					//TODO: WorldState 3 stuff
				}
			break;
			case 4:
				if (!WorldStates[3]) {
					//TODO: WorldState 4 stuff
				}
			break;
		}
	}
}
