using UnityEngine;
using System.Collections;

public enum MoveOrRotate {Move, Rotate, Once, All};

public class Button : MonoBehaviour {

	public LinearPlatform controlObject;
	public MoveOrRotate moveOrRotate = MoveOrRotate.Rotate;
	public Vector3 moveOnceLoc;
	
	Vector3 holdMov;
	Vector3 holdRot;
	bool pressed = false;
	bool curMoving = false;
	bool curRotating = false;

	// For Debugging purposes
	void OnGizmosDraw() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (transform.position, transform.position + .3f * Vector3.up);

		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, controlObject.transform.position);
	}

	// Use this for initialization
	void Start () {
		holdMov = controlObject.transform.position;
		holdRot = controlObject.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		//print (hold + " " + controlObject.transform.position.x);
		//the object is not in the same position as the last frame, so it's currently moving
		if (controlObject.transform.position != holdMov) {
			curMoving = true;
		}
		else {
			curMoving = false;
		}
		//the object is not in the same rotation as the last frame, so it's currently rotating
		if (controlObject.transform.eulerAngles != holdRot) {
			curRotating = true;
		}
		else {
			curRotating = false;
		}
		// the instant the button is pressed
		int layerMask = 1 << LayerMask.NameToLayer ("PlayerLayer");
		Vector3 rayCenter = transform.position;
		rayCenter.y -= 0.5f;
		if (Physics.Raycast (rayCenter, Vector3.up, 1f, layerMask) && !pressed && !curMoving && !curRotating) {
			pressed = true;
			if (moveOrRotate == MoveOrRotate.Move)
				controlObject.Move ();
			else if (moveOrRotate == MoveOrRotate.Rotate)
				controlObject.Rotate();
			else if (moveOrRotate == MoveOrRotate.Once) {
				controlObject.MoveOnce(moveOnceLoc);
			}
			else { //All
				controlObject.Move ();
				controlObject.Rotate ();
			}
		} else if(!Physics.Raycast (rayCenter, Vector3.up, 1f, layerMask)) {
			pressed = false;
		}
		
		holdMov = controlObject.transform.position;
		holdRot = controlObject.transform.eulerAngles;
	}
}
