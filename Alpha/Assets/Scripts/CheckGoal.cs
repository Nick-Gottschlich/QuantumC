using UnityEngine;
using System.Collections;

public class CheckGoal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 pos = transform.position;
		pos.y -= .5f;
		int layerMask = 1 << LayerMask.NameToLayer ("PlayerLayer");
		if (Physics.Raycast (pos, Vector3.up, out hit, 2f, layerMask)) {
			if (hit.collider.name == "Player1" && transform.name == "P1Goal") {
				GameRunner.P1colGoal = true;
			}
			if (transform.name == "P2Goal" && hit.collider.name == "Player2") {
				GameRunner.P2colGoal = true;
			}
		}
		else {
			if (transform.name == "P1Goal") {
				GameRunner.P1colGoal = false;
			}
			if (transform.name == "P2Goal") {
				GameRunner.P2colGoal = false;
			}
		}
		
	}
}
