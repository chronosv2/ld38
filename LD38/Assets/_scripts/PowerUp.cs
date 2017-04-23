using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PowerUp : MonoBehaviour {

	public enum PowerUpType
	{
		DAMAGE,
		RATE,
		SPREAD
	}
	public PowerUpType myType = PowerUpType.DAMAGE;
	public AudioClip getClip;

	// Use this for initialization
	void Awake () {
		if (myType == PowerUpType.DAMAGE && GameManager.GunDamage > 4) { Destroy(gameObject); Debug.Log("Generated unneeded Damage Powerup"); }
		if (myType == PowerUpType.RATE   && GameManager.GunRate   > 4) { Destroy(gameObject); Debug.Log("Generated unneeded Rate Powerup"); }
		if (myType == PowerUpType.SPREAD && GameManager.GunSpread)     { Destroy(gameObject); Debug.Log("Generated unneeded Spread Powerup"); }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		switch (myType) {
			case PowerUpType.DAMAGE:
				if (GameManager.GunDamage < 5) GameManager.GunDamage ++;
			break;
			case PowerUpType.RATE:
				if (GameManager.GunRate < 5) GameManager.GunRate ++;
			break;
			case PowerUpType.SPREAD:
				if (!GameManager.GunSpread) {
					GameManager.GunSpread = true;
				}
			break;
		}
		PlayClipAt(getClip, transform.position);
		Destroy(gameObject);
	}

	AudioSource PlayClipAt(AudioClip clip, Vector3 pos){
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
