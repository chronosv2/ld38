using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour {

	public GameObject myTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;	
		Vector3 target = myTarget.transform.position;
		Vector3 dir = target - transform.position;
    	float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90;
	    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);				
	}
}
