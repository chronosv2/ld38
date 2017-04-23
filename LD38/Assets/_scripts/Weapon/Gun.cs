using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	bool isPlayerGun = false;
	int FireRate = 1;
	float BaseBPS = 3.5f;
	float timer;
	float _fireRate;
	bool isSpread = false;
	public GameObject BulletPrefab;
	// Use this for initialization
	void Start () {
		if (transform.parent.gameObject.name == "Player_Unit") {
			isPlayerGun = true;
		}
		_fireRate = 1 / (FireRate*BaseBPS);
		timer = _fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (isPlayerGun) {
			if (Input.GetKeyDown(KeyCode.Backslash)) {
				GameManager.GunSpread = !GameManager.GunSpread;
			}
			if (Input.GetMouseButton(0)) {
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
				isSpread = GameManager.GunSpread;
				doShoot();
			}
		}
	}

	void resetTimer() {
		if (!isPlayerGun) {
			timer = _fireRate;
			timer *= Random.Range(1,2.5f);
		} else {
			timer = _fireRate;
			_fireRate = 1 / (FireRate*BaseBPS);
		}
	}

	void doShoot() {
		Vector3 target = transform.up+transform.position;
		Vector3 dir = target - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90;
		makeBullet(Quaternion.AngleAxis(angle, Vector3.forward));
		if (GameManager.GunSpread || isSpread) {
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
}
