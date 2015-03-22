using UnityEngine;
using System.Collections;

public enum MoveOrRotate {Move, Rotate, Both};

public class Button : MonoBehaviour {

	public LinearPlatform controlObject;
	public MoveOrRotate moveOrRotate = MoveOrRotate.Rotate;
	public bool pressed = false;

	// For Debugging purposes
	void OnGizmosDraw() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (transform.position, transform.position + .3f * Vector3.up);

		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, controlObject.transform.position);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// the instant the button is pressed
		int layerMask = 1 << LayerMask.NameToLayer ("PlayerLayer");
		Vector3 rayCenter = transform.position;
		rayCenter.y -= 0.5f;
		if (Physics.Raycast (rayCenter, Vector3.up, 1f, layerMask) && !pressed) {
			pressed = true;
			if (moveOrRotate == MoveOrRotate.Move)
				controlObject.Move ();
			else if (moveOrRotate == MoveOrRotate.Rotate)
				controlObject.Rotate();
			else {
				controlObject.Move ();
				controlObject.Rotate ();
			}
		} else if(!Physics.Raycast (rayCenter, Vector3.up, 1f, layerMask)) {
			pressed = false;
		}
	}
}
