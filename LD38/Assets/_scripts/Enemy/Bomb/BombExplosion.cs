using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour {

	public float Range = 1f;
	public int Damage = 100;
	public float Lifetime = 0.7f;
	public AudioClip explodeClip;
	float timer = 0;
	// Use this for initialization
	void Start () {
		CircleCollider2D coll = GetComponent<CircleCollider2D>();
		coll.radius = Range;
		PlayClipAt(explodeClip, transform.position, 0.4f);
	}
	
	// Update is called once per frame
	void Update () {
		//This is the explision, not the bomb. If we pause this the animation will get mucked.
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
			other.collider.gameObject.GetComponent<PlayerHealth>().doColliderHit();
		} else if (sS != null) {
			sS.Health -= Damage;
		} 
	}

	AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volume = 1f){
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.rolloffMode = AudioRolloffMode.Linear;
		// set other aSource properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}	
}
