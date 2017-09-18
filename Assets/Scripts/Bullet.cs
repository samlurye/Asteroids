using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;
	public int damage;

	void Start() {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = speed * transform.up;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Asteroid") {
			other.gameObject.GetComponent<Asteroid>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}

	void OnDestroy() {
		Vector3 target = transform.position;
	}

}
