using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunAbility : Ability {

	public float maxTimeOn;
	private SpriteRenderer srend;
	private bool isOn;
	private float timeAtOn;
	private GameObject asteroid;

	public override void Fire() {
		srend = abilityPrefab.GetComponent<SpriteRenderer>();
		asteroid = abilityPrefab.GetComponent<GravityGun>().asteroid;
		CheckCooldown();
		if (Input.GetAxis(keyBinding) > 0 && !isOn && !isCoolingDown) {
			srend.enabled = true;
			isOn = true;
			timeAtOn = Time.time;
		} else if ((!(Input.GetAxis(keyBinding) > 0) || Time.time - timeAtOn >= maxTimeOn) && isOn) {
			TurnGunOff();
		}
	}

	public void TurnGunOff() {
		srend.enabled = false;
		if (asteroid) {
			Vector3 asteroidVel = asteroid.GetComponent<Rigidbody2D>().velocity;
			asteroid.GetComponent<Rigidbody2D>().velocity = asteroidVel.normalized * Mathf.Clamp(asteroidVel.magnitude, 0f, 10f);
		}
		abilityPrefab.GetComponent<GravityGun>().asteroid = null;
		isOn = false;
		lastUse = Time.time;
	}

}
