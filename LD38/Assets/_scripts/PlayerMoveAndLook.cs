//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAndLook : MonoBehaviour {

	public float moveSpeed = 3;
	private Camera gameCamera;

	// Use this for initialization
	void Start () {
		gameCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		HandleTurning();
	}

	void HandleMovement() {
		Vector2 newPosition;
		float NewY = transform.position.y;
		float NewX = transform.position.x;
		if (Input.GetKey(KeyCode.W)) {
			//Move Up (+Y)
			NewY = transform.position.y + (moveSpeed * Time.deltaTime);
		} else if (Input.GetKey(KeyCode.S)) {
			//Move Down (-Y)
			NewY = transform.position.y - (moveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A)) {
			//Move Left (-X)
			NewX = transform.position.x - (moveSpeed * Time.deltaTime);
		} else if (Input.GetKey(KeyCode.D)) {
			//Move Right (+X)
			NewX = transform.position.x + (moveSpeed * Time.deltaTime);
		}
		newPosition = new Vector2(NewX,NewY);
		transform.position = newPosition;
	}

	void HandleTurning() {
		//Yeah, not proud of it but code slightly modified from http://answers.unity3d.com/comments/1205447/view.html
		Vector3 target = gameCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = target - transform.position;
    	 float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90;
	     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		 //What used to be here was a jumbled mess of code. Would have saved it for posterity's sake but didn't do a git push yet.
	}
}
