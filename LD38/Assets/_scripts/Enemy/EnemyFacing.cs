using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacing : MonoBehaviour {

	EnemyTargeting eTgt;
	// Use this for initialization
	void Start () {
		eTgt = GetComponent<EnemyTargeting>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame) return;
		if (eTgt.target == null) return;
		Vector3 target = eTgt.target.position;
		Vector3 dir = target - transform.position;
    	float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90;
	    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);		
	}
}
