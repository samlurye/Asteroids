using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

	public float spawnRate;
	public GameObject asteroid;
	public GameObject player;
	public GameObject warning;
	public float speed;
	public float startSpeed;
	public float startSpawnRate;
	public float xBound, yBound, warningXBound, warningYBound;
	public float lastSpawn;
	public float maxSpeed;
	public float speedInc;
	public float minSpawnRate;
	public float spawnRateDec;

	private IEnumerator coroutine;

	void Start() {
		lastSpawn = 0;
		speed = startSpeed;
		spawnRate = startSpawnRate;
	}

	void Update() {
		if (Time.time > lastSpawn + spawnRate) {
			lastSpawn = Time.time;
			SpawnBySide(Random.Range(1, 5));
			if (speed < maxSpeed) {
				speed += speedInc;
			}
			if (spawnRate > minSpawnRate) {
				spawnRate -= spawnRateDec;
			}
		}
	}

	Vector3 getPointOnSide(int side) {
		if (side == 1) {
			return (new Vector3(Random.Range(-xBound, xBound), -yBound, 0));
		} else if (side == 2) {
			return (new Vector3(xBound, Random.Range(-yBound, yBound), 0));
		} else if (side == 3) {
			return (new Vector3(Random.Range(-xBound, xBound), yBound, 0));
		} else {
			return (new Vector3(-xBound, Random.Range(-yBound, yBound), 0));
		}
	}

	void SpawnBySide(int side) {
		Vector3 startPt = getPointOnSide(side);
		Vector3 warningPt = new Vector3 (
	        Mathf.Clamp(startPt.x, -warningXBound, warningXBound), 
	        Mathf.Clamp(startPt.y, -warningYBound, warningYBound),
	        0
	    );
		Vector3 endPt = getPointOnSide((side + 2) % 4);
		Vector3 velocity = (endPt - startPt).normalized * speed;
		GameObject warningClone = Instantiate(warning, warningPt, Quaternion.identity);
		coroutine = SpawnAsteroid(startPt, velocity, warningClone);
		StartCoroutine(coroutine);
	}

	private IEnumerator SpawnAsteroid(Vector3 startPt, Vector3 velocity, GameObject warningClone) {
		yield return new WaitForSeconds(2);
		GameObject astr = Instantiate(asteroid, startPt, Quaternion.identity);
		astr.GetComponent<Rigidbody2D>().velocity = velocity;
		Destroy(warningClone);
	}
		
}
