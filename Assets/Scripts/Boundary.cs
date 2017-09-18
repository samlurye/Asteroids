using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag != "Player" && other.tag != "BlackHole") {
			Destroy(other.gameObject);
		}
	}

}
