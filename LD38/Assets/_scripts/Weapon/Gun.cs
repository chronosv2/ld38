using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	bool isPlayerGun = false;
	int FireRate = 1;
	float BaseBPS = 3.5f;
	float timer;
	float _fireRate;
	float fFireRate;
	public bool isSpread = false;
	public GameObject BulletPrefab;
	public AudioClip shootClip;
	PlayerHealth myHealth;
	// Use this for initialization
	void Start () {
		if (transform.parent.gameObject.name == "Player_Unit") {
			isPlayerGun = true;
			myHealth = transform.parent.gameObject.GetComponent<PlayerHealth>();
			fFireRate = (GameManager.GunRate-1)*0.30f;
			_fireRate = 1 / ((1+fFireRate)*BaseBPS);
		} else {
			_fireRate = 1 / (FireRate*BaseBPS);
		}
		timer = _fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (isPlayerGun) {
			if (Input.GetKeyDown(KeyCode.Backslash)) {
				GameManager.GunSpread = !GameManager.GunSpread;
			}
			if (Input.GetMouseButton(0) && !myHealth.playerIsDestroyed) {
				timer -= Time.deltaTime;
				if (timer <= 0) {
					resetTimer();
					isSpread = GameManager.GunSpread;
					doShoot();
				}
			} else if (Input.GetMouseButtonUp(0)) {
				resetTimer();
			}
		} else {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				resetTimer();
				doShoot();
			}
		}
	}

	void resetTimer() {
		if (!isPlayerGun) {
			timer = _fireRate;
			timer *= Random.Range(1,2.5f);
		} else {
			fFireRate = (GameManager.GunRate-1)*0.3f;
			_fireRate = 1 / ((1+fFireRate)*BaseBPS);
			timer = _fireRate;
		}
	}

	void doShoot() {
		Vector3 target = transform.up+transform.position;
		Vector3 dir = target - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90;
		makeBullet(Quaternion.AngleAxis(angle, Vector3.forward));
		PlayClipAt(shootClip, transform.position);
		if ((GameManager.GunSpread && isPlayerGun) || isSpread) {
			foreach (Transform child in transform) {
				target = child.position;
				dir = target - transform.position;
				angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90;
				makeBullet(Quaternion.AngleAxis(angle, Vector3.forward));
			}
		}
	}

	void makeBullet(Quaternion direction) {
		GameObject myBullet = Instantiate(BulletPrefab,transform.position,direction) as GameObject;
		Projectile myProjectile = myBullet.GetComponent<Projectile>();
		if (isPlayerGun) {
			myProjectile.damageValue *= GameManager.GunDamage;
		}		
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
