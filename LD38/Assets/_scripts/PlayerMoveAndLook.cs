//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAndLook : MonoBehaviour {

	public float moveSpeed = 3;
	private Camera gameCamera;
	float xmin, xmax, ymin, ymax;
	public float padding = 0.3f;
	// Use this for initialization
	void Start () {
		gameCamera = FindObjectOfType<Camera>();
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));
		Vector3 btmMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
		ymin = btmMost.y + padding;
		ymax = topMost.y - padding;		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.isPaused || GameManager.isPreGame || !GameManager.isPlaying) return;
		if (GameManager.PlayerHealth > 0) {
			HandleMovement();
			HandleTurning();
		}
	}

	void HandleMovement() {
		Vector2 newPosition;
		float NewY = transform.position.y;
		float NewX = transform.position.x;
		if (Input.GetAxis("Vertical") > 0) {
			NewY = transform.position.y + (moveSpeed * Time.deltaTime);
		} else if (Input.GetAxis("Vertical") < 0) {
			NewY = transform.position.y - (moveSpeed * Time.deltaTime);		
		}
		if (Input.GetAxis("Horizontal") > 0) {
			NewX = transform.position.x + (moveSpeed * Time.deltaTime);
		} else if (Input.GetAxis("Horizontal") < 0) {
			NewX = transform.position.x - (moveSpeed * Time.deltaTime);			
		}

		// if (Input.GetKey(KeyCode.W)) {
		// 	//Move Up (+Y)
		// 	NewY = transform.position.y + (moveSpeed * Time.deltaTime);
		// } else if (Input.GetKey(KeyCode.S)) {
		// 	//Move Down (-Y)
		// 	NewY = transform.position.y - (moveSpeed * Time.deltaTime);
		// }
		// if (Input.GetKey(KeyCode.A)) {
		// 	//Move Left (-X)
		// 	NewX = transform.position.x - (moveSpeed * Time.deltaTime);
		// } else if (Input.GetKey(KeyCode.D)) {
		// 	//Move Right (+X)
		// 	NewX = transform.position.x + (moveSpeed * Time.deltaTime);
		// }
		NewX = Mathf.Clamp(NewX, xmin, xmax);
		NewY = Mathf.Clamp(NewY, ymin, ymax);
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
