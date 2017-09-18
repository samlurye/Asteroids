using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

	public float pullForce;
	public float speed;
	public Rigidbody2D rb;
	public Animator anim;
	Collider2D[] expRadius;

	void Start() {
		anim = GetComponent<Animator>();
		expRadius = new Collider2D[0];
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = speed * transform.up;
		int angSpeed = Random.Range(0, 2);
		if (angSpeed == 0) {
			rb.angularVelocity = -360;
		} else {
			rb.angularVelocity = 360;
		}
	}

	void FixedUpdate() {
		for (int i = 0; i < expRadius.Length; i++) {
			if (expRadius[i]) {
				GameObject obj = expRadius [i].gameObject;
				if (obj.tag == "Asteroid" && obj != null) {
					float dist = (obj.transform.position - transform.position).magnitude;
					if (dist <= 0.5f) {
						Vector3 vel = obj.GetComponent<Rigidbody2D>().velocity;
						obj.GetComponent<Rigidbody2D>().velocity = vel.normalized * 0.1f;
					}
				}
			}
		}
	}

	public void TriggerExplosion() {
		anim.SetTrigger("Explode");
	}

	void OnExplode() {
		Vector3 pos = transform.position;
		expRadius = Physics2D.OverlapCircleAll(pos, 5);
		for (int i = 0; i < expRadius.Length; i++) {
			if (expRadius[i]) {
				GameObject obj = expRadius[i].gameObject;
				string tag = obj.tag;
				if (tag == "Asteroid") {
					Vector3 otherPos = obj.transform.position;
					Vector3 force = (pos - otherPos).normalized * pullForce;
					obj.GetComponent<Rigidbody2D>().AddForce(force);
				}
			}
		}
	}

	void OnExplodeEnd() {
		for (int i = 0; i < expRadius.Length; i++) {
			if (expRadius[i]) {
				GameObject obj = expRadius [i].gameObject;
				if (obj.tag == "Asteroid" && obj != null) {
					Vector3 vel = obj.GetComponent<Rigidbody2D>().velocity;
					obj.GetComponent<Rigidbody2D>().velocity = vel.normalized * 0.1f;
				}
			}
		}
		GameObject ship = GameObject.Find("ShipSprite");
		if (ship) {
			ship.GetComponent<BlackHoleAbility>().isActive = false;
		}
		Destroy(gameObject);
	}
		
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Boundary") {
			TriggerExplosion();
		}
	}
}
