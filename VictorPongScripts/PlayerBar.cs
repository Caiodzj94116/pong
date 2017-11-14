using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour {

	private Rigidbody2D barRb;
	public int velY = 0;
	private int direct = 0;
	private bool upPressed = false, downPressed = false;

	private Rigidbody2D ballRb;

	public int scr1 = 0, scr2 = 0;

	void Start () {
		barRb = GetComponent<Rigidbody2D> ();	
		barRb.velocity = new Vector2(barRb.velocity.x, velY);

		ballRb = GameObject.FindWithTag ("ball").GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchPosition = Input.GetTouch (0).position;

			if (touchPosition.y > 0) {
				upPressed = true;
			} else
				upPressed = false;
			if (touchPosition.y <= 0) {
				downPressed = true;
			} else
				downPressed = false;
		}

		ballRb = GameObject.FindWithTag ("ball").GetComponent<Rigidbody2D> ();

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			upPressed = true;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			downPressed = true;
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			upPressed = false;
		} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
			downPressed = false;
		}

		if (upPressed)
			velY = 7;
		else if (downPressed)
			velY = -7;
		else
			velY = 0;

		barRb.velocity = new Vector2(barRb.velocity.x, velY);
	}
}
