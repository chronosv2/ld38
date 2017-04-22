using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour {

	public float Range = 1f;
	public int Damage = 100;
	public float Lifetime = 0.7f;
	float timer = 0;
	// Use this for initialization
	void Start () {
		CircleCollider2D coll = GetComponent<CircleCollider2D>();
		coll.radius = Range;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= Lifetime) {
			Destroy(gameObject);
		} else {
			timer += Time.deltaTime;
		}
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, Range);
	}

	/// <summary>
	/// Sent each frame where a collider on another object is touching
	/// this object's collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionStay2D(Collision2D other)
	{
		SupportStructure sS = other.collider.GetComponent<SupportStructure>();
		if (other.collider.name == "Player_Unit") {
			GameManager.damagePlayer(Damage);
		} else if (sS != null) {
			sS.Health -= Damage;
		}
	}

}
