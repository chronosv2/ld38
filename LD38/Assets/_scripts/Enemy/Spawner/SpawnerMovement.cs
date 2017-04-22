using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovement : MonoBehaviour {
	float moveSpeed = 16;
	int Direction;
	// Use this for initialization
	void Start () {
		Direction = Mathf.RoundToInt(Random.Range(0,1));
		if (Direction == 1) {
			Debug.Log("Spawner "+gameObject.name+" Moving Right");
		} else {
			Debug.Log("Spawner "+gameObject.name+" Moving Left");
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(gameObject.transform.position,0.5f);
	}

	// Update is called once per frame
	void Update () {
		Vector3 target = new Vector3(0,0,transform.position.z);
		Vector3 dir = target - transform.position;
    	float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90;
	    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);				
		if (Direction == 1) {
			transform.position += transform.right * Time.deltaTime * moveSpeed;		
		} else {
			transform.position += -transform.right * Time.deltaTime * moveSpeed;		
		}
	}
}
