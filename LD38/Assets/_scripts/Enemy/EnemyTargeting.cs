using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour {

	public Transform target;
	private Transform[] targetList;
	public float TargetLockTime = 6f;
	public bool OnlyTargetStructures = false;
	public bool RemoveOnRetarget = false;
	float LockTime = 0;
	List<Transform> SingleTargetList;
	public bool nothingToTarget = false;

	// Use this for initialization
	void Start () {
		if (RemoveOnRetarget) {
			GenerateTargetList();
			target = GetClosestEnemy(targetList);		
		}
	}
	
	void GenerateTargetList() {
		GameObject[] targets;
		List<Transform> targetTransforms = new List<Transform>();
		targets = GameObject.FindGameObjectsWithTag("Target") as GameObject[];
		foreach(GameObject target in targets) {
			if (OnlyTargetStructures) {
				if (target.layer == 9) { //Layer 9 should be Structures.
					targetTransforms.Add(target.transform);			
				}
			} else {
					targetTransforms.Add(target.transform);
			}
		}
		targetList = targetTransforms.ToArray();
		if (targetList.Length == 0) {
			nothingToTarget = true;
		}
		if (RemoveOnRetarget) {
			SingleTargetList = targetTransforms;
		}
	}

	void UpdateTargetList(Transform lastTarget) {
		if (nothingToTarget == true) return;
		List<Transform> targetTransforms = new List<Transform>(SingleTargetList);
		SingleTargetList.Clear();
		SingleTargetList.TrimExcess();
		foreach(Transform target in targetTransforms) {
			if (OnlyTargetStructures) {
				if (target != null) {
					if (target.gameObject.layer == 9) { //Layer 9 should be Structures.
						if (target.GetHashCode() != lastTarget.GetHashCode()) {
							if (target != null) {
								SingleTargetList.Add(target.transform);
							}
						}
					}
				}
			}
		}
		targetList = SingleTargetList.ToArray();
		if (targetList.Length == 0) {
			GenerateTargetList();
		}
	}

	Transform GetClosestEnemy (Transform[] enemies) {
        if (nothingToTarget) return null;
		Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }	
		LockTime = TargetLockTime;
		Debug.Log(gameObject.name + " chose target: " + bestTarget.gameObject.name);
        return bestTarget;
    }

	public void Retarget() {
		UpdateTargetList(target);
		target = GetClosestEnemy(targetList);
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (RemoveOnRetarget) return;
		if (LockTime <= 0 || target == null) {
			GenerateTargetList();
			target = GetClosestEnemy(targetList);
		} else {
			LockTime -= Time.deltaTime;
		}
	}
}
