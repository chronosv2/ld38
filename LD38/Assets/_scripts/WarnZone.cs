using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnZone : MonoBehaviour {

	bool playerInZone = true;
	public int HurtStep = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerInZone) {
			GameManager.damagePlayer(HurtStep);
		}
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
