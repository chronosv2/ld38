using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public bool playerIsDestroyed = false;
	bool playerisInvulnerable = false;
	public float RespawnTimer = 5.0f;
	public float InvulnTimer = 1.0f;
	public float timer = 0;
	float invtimer = 0;
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (GameManager.PlayerHealth <= 0) {
			if (!playerIsDestroyed) {
				gameObject.transform.position = new Vector2(0,0);
				playerIsDestroyed = true;
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
				gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
			} else {
				if (timer >= RespawnTimer) {
					playerisInvulnerable = true;
					gameObject.GetComponent<SpriteRenderer>().enabled = true;
					gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
					GameManager.setHealth(100);
					playerIsDestroyed = false;
					timer = 0;
				} else {
					timer += Time.deltaTime;
					invtimer = 0;
					playerisInvulnerable = false;
				}				
			}
		}
		if (playerisInvulnerable) {
			Color clr = gameObject.GetComponent<SpriteRenderer>().material.color;
			clr.a = 0.5f;
			gameObject.GetComponent<SpriteRenderer>().material.color = clr;
			invtimer += Time.deltaTime;
			if (invtimer >= InvulnTimer) {
				invtimer = 0;
				playerisInvulnerable = false;
			}
		} else {
			Color clr = gameObject.GetComponent<SpriteRenderer>().material.color;
			clr.a = 1f;
			gameObject.GetComponent<SpriteRenderer>().material.color = clr;
		}
	}

	public void doColliderHit() {
		invtimer = 0;
		playerisInvulnerable = true;		
	}
	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (!playerisInvulnerable) {
			if (other.gameObject.tag == "Bullet") {
				GameManager.damagePlayer(other.gameObject.GetComponent<Projectile>().damageValue);
				Destroy(other.gameObject);
				invtimer = 0;
				playerisInvulnerable = true;
			}
		}
	}
}
