using UnityEngine;
using System.Collections;

public enum Axis {x, y, z}

public class LinearPlatform : MonoBehaviour {

	private Vector3 screenpoint;
	private Vector3 offset;

	public bool rotation = false;
	public Axis rotationAxis = Axis.x;
	public float rotationSpeed = 50f;
	Vector3 initialMousePoint;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		screenpoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position
			- Camera.main.ScreenToWorldPoint(new Vector3(screenpoint.x, Input.mousePosition.y, screenpoint.z));
//		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (screenpoint);

//		Vector3 yay = new Vector3 (0, screenpoint.y, 0);
//		offset = screenpoint - new Vector3 (0, Input.mousePosition.y, 0);

		print ("On Mouse Down" + offset);
		initialMousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f));
	}

	void OnMouseDrag() {
		if (rotation) {
			Rotate();
		} else {
			Vector3 curScreenPoint = new Vector3 (screenpoint.x, Input.mousePosition.y , screenpoint.z);
			Vector3 curPos = Camera.main.ScreenToWorldPoint(curScreenPoint + offset);
	//		Vector3 curPos = transform.position;
	//		curPos += new Vector3(0, Input.mousePosition.y
			transform.position = curPos;
	//		print ("On Mouse Drag, Before: " + Camera.main.ScreenToWorldPoint(curScreenPoint) + "After: " + curPos);
			print (screenpoint);
		}
	}

	void Rotate() {
		Vector3 newMousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f));
		Vector3 rotation = (initialMousePoint - newMousePoint) * rotationSpeed;
		rotation.z = 0f;
		if (rotationAxis.Equals(Axis.x)) {
			rotation.y = 0f;
		} else if (rotationAxis.Equals(Axis.y)) {
			rotation.x = 0f;
		}
		transform.Rotate (rotation, Space.World);
		initialMousePoint = newMousePoint;
	}
	
	//test change
}
