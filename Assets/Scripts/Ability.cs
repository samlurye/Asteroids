using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ability : MonoBehaviour {

	public GameObject abilityPrefab;
	public string abilityName;
	public float cooldown;
	public float lastUse;
	public bool isCoolingDown;
	public string keyBinding;
	public bool useGetAxis;
	public bool useGetKeyDown;
	public GameObject visualFX;
	public float initialPositionOffset;
	public Slider cooldownSlider;

	void Start() {
		lastUse = -cooldown;
		isCoolingDown = false;
		if (abilityName == "GravityGun") {
			for (int i = 0; i < 10; i++) {
				GravityGun.gravPoints.Add(new Vector3(Mathf.Infinity, Mathf.Infinity, 0));
			}
		}
	}

	public virtual void Fire() {
		if (visualFX) {
			visualFX.SetActive(false);
		}
		CheckCooldown();
		SetCooldownSlider();
		if (useGetKeyDown) {
			if (Input.GetKeyDown(keyBinding) && !isCoolingDown) {
				InstantiateAbility();
			}
		} else if (useGetAxis) {
			if (Input.GetAxis(keyBinding) > 0 && !isCoolingDown) {
				InstantiateAbility();
			}
		}
	}

	public virtual GameObject InstantiateAbility() {
		lastUse = Time.time;
		if (visualFX) {
			visualFX.SetActive(true);
		}
		return Instantiate(
			abilityPrefab, 
			InitialPosition(),
			transform.rotation
		);
	}

	public virtual Vector3 InitialPosition() {
		return transform.position + initialPositionOffset * transform.up;
	}

	public virtual void CheckCooldown() {
		if (Time.time - lastUse >= cooldown) {
			isCoolingDown = false;
		} else {
			isCoolingDown = true;
		}
	}

	public virtual void SetCooldownSlider() {
		if (cooldownSlider) {
			cooldownSlider.value = cooldown - (Time.time - lastUse);
		}
	}

}
