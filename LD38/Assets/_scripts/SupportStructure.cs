using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportStructure : MonoBehaviour {

	public int Health = 100;
	public int loseTimeAmount = 4; // This value multiplied by LoseTimeStep for time lost when destroyed.
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0) {
			doAddLoseTime();
			doDestroy();
		}
	}

	void doAddLoseTime() {
		GameManager.addLoseTime(loseTimeAmount);
	}

	void doDestroy() {
		//TODO: Particle effect? Explosion sprite? Obviously play a sound!
		Destroy(gameObject);
	}
	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet") {
			int Damage = other.gameObject.GetComponent<Projectile>().damageValue;
			Health -= Damage;
			Destroy(other.gameObject);
		}
	}	
}
