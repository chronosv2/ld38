using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnZone : MonoBehaviour {

	Color Color1 = new Color(0.6f,0.6f,0,1);
	Color Color2 = new Color(0.6f,0,0,1);
	float colorLerp = 0;
	bool colorUp = true;
	public float colorStep = 1;
	SpriteRenderer myMaterial;
	bool playerInZone = true;
	public int HurtStep = 1;
	// Use this for initialization
	void Start () {
		myMaterial = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerInZone) {
			GameManager.damagePlayer(HurtStep);
		}
		if (colorUp) {
			colorLerp += colorStep*Time.deltaTime;
			if (colorLerp >= 1) {
				colorUp = false;
			}
		} else {
			colorLerp -= colorStep*Time.deltaTime;
			if (colorLerp <= 0) {
				colorUp = true;
			}
		}
		myMaterial.color = Color.Lerp(Color1, Color2, colorLerp);
	}

	/// <summary>
	/// Sent when another object leaves a trigger collider attached to
	/// this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == 8) {
			playerInZone = false;
		}
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 8) {
			playerInZone = true;
		}		
	}
}
