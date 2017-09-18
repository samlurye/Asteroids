using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public float health;
	public Slider healthSlider;
	public GameObject explosion;

	void Start() {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.angularVelocity = Random.Range(-360, 360);
	}

	public void TakeDamage(int damage) {
		health = health - damage;
		if (health <= 0) {
			Explode();
		} else {
			healthSlider.value = health;
		}
	}

	void Explode() {
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
		GameObject ship = GameObject.Find("ShipSprite");
		if (ship) {
			ship.GetComponent<PlayerController>().AddScore(100);
		}
	}
		
}
