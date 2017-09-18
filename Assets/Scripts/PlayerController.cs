using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float thrustForce;
	public float angSpeed;
	public float xBound, yBound;
	public int score;
	public GameObject scoreText;
	public GameObject explosion;
	public Animator anim;
	public Rigidbody2D rb;
	private bool canWrap;
	private bool dead = false;
	public float scoreInterval = 1f;
	public float lastScoreTime = 0f;

	public Ability[] activeAbilities;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update() {
		foreach (Ability ability in activeAbilities) {
			ability.Fire();
		}
		if (scoreInterval < Time.time - lastScoreTime && !dead) {
			lastScoreTime = Time.time;
			AddScore(10);
		}
	}

	void FixedUpdate() {
		float rot = Input.GetAxis("Horizontal");
		float thrust = Input.GetAxis("Vertical");
		if (Input.GetAxisRaw("Vertical") > 0) {
			anim.SetBool("EngineOn", true);
		} else {
			anim.SetBool("EngineOn", false);
		}
		if (thrust < 0) {
			thrust = 0;
		}
		transform.Rotate(0, 0, angSpeed * rot);
		rb.AddForce(transform.up * thrust * thrustForce);
	} 

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Boundary" && canWrap) {
			WrapWorld();
			canWrap = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Boundary") {
			canWrap = true;
		} else if (other.tag == "Asteroid") {
			Instantiate(explosion, transform.position, Quaternion.identity);
			GetComponent<GravityGunAbility>().TurnGunOff();
			gameObject.SetActive(false);
			Invoke("Restart", 2);
		}
	}

	void Restart() {
		SceneManager.LoadScene("Main");
	}

	void WrapWorld() {
		int h = 1;
		int v = 1;
		if (Mathf.Abs(transform.position.x) >= xBound) {
			h = -1;
		}
		if (Mathf.Abs(transform.position.y) >= yBound) {
			v = -1;
		}
		transform.position = new Vector3(h * transform.position.x, v * transform.position.y, 0);
	}

	public void AddScore(int points) {
		score = score + points;
		scoreText.GetComponent<Text>().text = "Score: " + score;
	}

}
