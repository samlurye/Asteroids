using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFieldPositions : MonoBehaviour {

	public GameObject grid;
	public SpriteRenderer srend;
	public static float intensity = 0.03f;
	private Material gridMaterial;

	void Start() {
		gridMaterial = grid.GetComponent<SpriteRenderer>().sharedMaterial;
	}

	void Update () {
		Vector4[] fieldPositions = new Vector4[10];
		for (int i = 0; i < 10; i++) {
			if (i < GravityGun.gravPoints.Count) {
				fieldPositions[i] = new Vector3(1f / 20f * (GravityGun.gravPoints[i].x + 10), 1f / 20f * (GravityGun.gravPoints[i].y + 10), 0);
			} else {
				fieldPositions[i] = new Vector4(Mathf.Infinity, Mathf.Infinity, 0, 0);
			}
		}
		gridMaterial.SetVectorArray("_FieldPositions", fieldPositions);
		if (!srend.enabled) {
			if (intensity > 0) {
				intensity -= 0.0036f;
				gridMaterial.SetFloat("_Intensity", intensity);
			} else {
				for (int i = 0; i < 10; i++) {
					GravityGun.gravPoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, 0);
				}
			}
		} else {
			if (intensity < 0.03f) {
				intensity += 0.0036f;
			} else {
				intensity = 0.03f;
			}
			gridMaterial.SetFloat("_Intensity", intensity);
		}
	}
}
