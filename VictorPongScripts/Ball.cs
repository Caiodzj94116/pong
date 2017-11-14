using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	private float velBX, velBY;
	public float speed;
	private float randomAngle;
	private Rigidbody2D ballRb;
	private Transform ballTr;
	public GameObject ballPrefab;
	public Vector3 pos;

	private Rigidbody2D compBarRb;
	private int directBar;

	private Text score1;
	private Text score2;

	private PlayerBar playerSc;
	private Rigidbody2D playerBarRb;

	void Start () {
		ballRb = GetComponent<Rigidbody2D> ();
		ballTr = GetComponent<Transform> ();

		compBarRb = GameObject.Find ("CompBar").GetComponent<Rigidbody2D> ();

		playerSc = GameObject.Find ("playerBar").GetComponent<PlayerBar> ();
		playerBarRb = GameObject.Find ("playerBar").GetComponent<Rigidbody2D> ();

		score1 = GameObject.Find ("Score1").GetComponent<Text> ();
		score2 = GameObject.Find ("Score2").GetComponent<Text> ();

		pos = ballTr.position;

		speed = 8f;

		randomAngle = getRandonAngle (true);
		velBX = Mathf.Cos (randomAngle) * speed;
		velBY = Mathf.Sin (randomAngle) * speed;
	}
	
	// Update is called once per frame
	void Update () {
		ballRb.velocity = new Vector2 (velBX, velBY);

		if (ballRb.position.y < compBarRb.position.y)
			directBar = -1;
		else
			directBar = 1;
		
		if (ballRb.position.y > compBarRb.position.y + 0.64 || ballRb.position.y < compBarRb.position.y - 0.64)
			compBarRb.velocity = new Vector2 (compBarRb.velocity.x, directBar * 7);
		else if (ballRb.position.y < compBarRb.position.y + 0.64 && ballRb.position.y > compBarRb.position.y - 0.64)
			compBarRb.velocity = new Vector2 (compBarRb.velocity.x, 0);

		if (ballRb.position.x > compBarRb.position.x || ballRb.position.x < playerBarRb.position.x)
			over ();
	}

	void bounceBall() {
		int direct = (ballRb.position.x > 0)?( -1):(1);

		randomAngle = getRandonAngle (false);

		float dir = Random.value;
		if (dir > 0.5)
			dir = 1;
		else
			dir = -1;
		
		velBX = direct * Mathf.Cos(randomAngle) * speed * 1.3f;
		velBY = dir * Mathf.Sin (randomAngle) * speed * 1.3f;
	}

	void OnCollisionEnter2D (Collision2D colisao) {
		if (colisao.gameObject.tag == "parede") {
			velBY *= -1;
		}
		if (colisao.gameObject.tag == "bar") {
			bounceBall ();
		}
	}

	float getRandonAngle(bool init) {
		float angle;
		if (init) {
			angle = Random.value * 2 * Mathf.PI;
			while ((angle > Mathf.PI / 3 && angle < 2 * Mathf.PI / 3) || (angle > 4 * Mathf.PI / 3 && angle < 5 * Mathf.PI / 3))
				angle = Random.value * 2 * Mathf.PI;
		} else {
			angle = (Random.value * Mathf.PI / 3);
		}

		return angle;
	}

	void over() {
		if (ballRb.position.x > 0) {
			playerSc.scr1++;
			score1.text = "" + playerSc.scr1;
		} else {
			playerSc.scr2++;
			score2.text = "" + playerSc.scr2;
		}

		Instantiate (ballPrefab, pos, transform.localRotation);
		Destroy (this.gameObject);
	}

}