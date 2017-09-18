using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour {

	public SpriteRenderer srend;
	public GameObject asteroid;
	private Vector3 asteroidEnd;
	private Rigidbody2D asteroidRB;
	public float offset;
	public float pullForce;
	public GameObject ship;
	public BoxCollider2D col;
	public static List<Vector3> gravPoints = new List<Vector3>();

	void FixedUpdate() {
		if (ship) {
			transform.position = ship.transform.position + ship.transform.up * offset;
			transform.rotation = ship.transform.rotation;
		}
		if (asteroid && srend.enabled) {
			asteroidEnd = transform.position + transform.up * offset;
			Vector3 perpVel = pullForce * Vector3.Project(asteroidEnd - asteroid.transform.position, transform.right);
			Vector3 parVel = 0.2f * pullForce * Vector3.Project(asteroidEnd - asteroid.transform.position, transform.up);
			asteroidRB.velocity = perpVel + parVel;
			asteroid.transform.localScale -= new Vector3 (0.01f, 0.01f, 0);
			asteroidRB.mass -= 0.01f;
		}
		if (srend.enabled) {
			for (int i = 0; i < 10; i++) {
				gravPoints[i] = transform.position - transform.up * col.size.y * (0.3f - 0.1f * 0.8f * i);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Asteroid" && !asteroid) {
			asteroid = other.gameObject;
			asteroidRB = asteroid.GetComponent<Rigidbody2D>();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Asteroid" && asteroid && !srend.enabled) {
			asteroid = null;
		}
	}

}
