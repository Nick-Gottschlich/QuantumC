using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public LinearPlatform rotateObject;
	public bool pressed, rotation, move, posOrNeg;
	public Axis rotateDir, moveDir;
	public Vector3 threshold;

	// For Debugging purposes
	void OnGizmosDraw() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (transform.position, transform.position + .3f * Vector3.up);

		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, rotateObject.transform.position);
	}

	// Use this for initialization
	void Start () {
		pressed = false;
	}
	
	// Update is called once per frame
	void Update () {

		// the instant the button is pressed
		if (Physics.Raycast (transform.position, Vector3.up, 1f) && pressed == false) {
			pressed = true;

			// Rotation
			if (rotation) {
				rotateObject.Rotate(rotateDir);
			}


		} 
		// as long as the player stands on top of the button
		else if (Physics.Raycast (transform.position, Vector3.up, 1f)) {
			pressed = true;
		}
		else {
			pressed = false;
		}

		if (move && pressed) {
			// Translation
			if (move) {
				rotateObject.transform.Translate(Vector3.up * Time.deltaTime);
				if (Vector3.Distance(rotateObject.transform.position,threshold) < .1f) {
					move = false;
				}
			}		
		}
	}
}
