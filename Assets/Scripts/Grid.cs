using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public float xbound;
	public float ybound;
	public float numSquares;
	public GameObject line;

	void Start() {
		float lineSpacing = 2 * xbound / numSquares;
		for (int i = 0; i < numSquares - 1; i++) {
			GameObject lineCloneHor = Instantiate(line, Vector3.zero, Quaternion.identity);
			GameObject lineCloneVert = Instantiate(line, Vector3.zero, Quaternion.identity);
			LineRenderer lrendHor = lineCloneHor.GetComponent<LineRenderer>();
			LineRenderer lrendVert = lineCloneVert.GetComponent<LineRenderer>();
			Vector3 startPosHor = new Vector3(xbound, (i + 1) * lineSpacing - ybound, 0);
			Vector3 startPosVert = new Vector3((i + 1) * lineSpacing - xbound, ybound, 0);
			Vector3 endPosHor = new Vector3(-xbound, (i + 1) * lineSpacing - ybound, 0);
			Vector3 endPosVert = new Vector3((i + 1) * lineSpacing - xbound, -ybound, 0);
			lrendHor.SetPositions(new Vector3[2] {startPosHor, endPosHor});
			lrendVert.SetPositions(new Vector3[2] {startPosVert, endPosVert});
		}
	}

}
