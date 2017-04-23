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
	BoxCollider2D WarnCollider;

	// Use this for initialization
	void Start () {
		WarnRenderer = WarnZone.GetComponent<SpriteRenderer>();
		WarnCollider = WarnZone.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.isPlaying) return;
		Vector2 PlayableBounds = new Vector2(31,22);
		int TimeLeft = Mathf.FloorToInt(GameManager.GameTimer);
		if (TimeLeft <= 0) {
			WarnZone.SetActive(false);
		} else if (TimeLeft < 30) {
			Phase5Ground.SetActive(false);			
		} else if (TimeLeft < 60) {
			PlayableBounds = new Vector2(15,10);
			WarnRenderer.size = PlayableBounds;
			WarnCollider.size = PlayableBounds;			
		} else if (TimeLeft < 90) {
			Phase4Ground.SetActive(false);			
		} else if (TimeLeft < 120) {
			PlayableBounds = new Vector2(20,13);
			WarnRenderer.size = PlayableBounds;
			WarnCollider.size = PlayableBounds;			
		} else if (TimeLeft < 150) {
			Phase3Ground.SetActive(false);			
		} else if (TimeLeft < 180) {
			PlayableBounds = new Vector2(25,16);
			WarnRenderer.size = PlayableBounds;
			WarnCollider.size = PlayableBounds;			
		} else if (TimeLeft < 210) {
			Phase2Ground.SetActive(false);			
		} else if (TimeLeft < 240) {
			PlayableBounds = new Vector2(29,20);
			WarnRenderer.size = PlayableBounds;
			WarnCollider.size = PlayableBounds;
		} else if (TimeLeft < 270) {
			Phase1Ground.SetActive(false);
		}
	}
}
