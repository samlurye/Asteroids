using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAbility : Ability {

	public bool isActive = false;
	private GameObject blackHoleClone;

	public override void Fire() {
		CheckCooldown();
		if (Input.GetKeyDown(keyBinding) && !isCoolingDown && !isActive) {
			isActive = true;
		} else if (Input.GetKeyDown(keyBinding) && !isCoolingDown) {
			blackHoleClone.GetComponent<BlackHole>().TriggerExplosion();
		}
	}

}
