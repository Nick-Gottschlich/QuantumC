using UnityEngine;
using System.Collections;

public enum Axis {x, y, z}

public class LinearPlatform : MonoBehaviour {

	private Vector3 screenpoint;
	private Vector3 offset;
	
	bool dragEnabled = false;
	Vector3 dragStartPosition;
	float dragStartDistance;

	public bool rotation = false;
	public Axis rotationAxis = Axis.x;
	public float rotationSpeed = 50f;
	public float correctAngle = 0f;
	public static bool correctPosition = false;
	Quaternion toRot = Quaternion.identity;

	// Use this for initialization
	void Start () {
		toRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			dragEnabled = false;
		}

		if (rotation) {
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) {
				Rotate (false);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
				Rotate (true);
			}

			if (rotationAxis.Equals(Axis.x)) {
				correctPosition = transform.eulerAngles.x <= correctAngle+0.1f && transform.eulerAngles.x >= correctAngle-0.1f;
			} else if (rotationAxis.Equals(Axis.y)) {
				correctPosition = transform.eulerAngles.y <= correctAngle+0.1f && transform.eulerAngles.y >= correctAngle-0.1f;
			} else if (rotationAxis.Equals(Axis.z)) {
				correctPosition = transform.eulerAngles.z <= correctAngle+0.1f && transform.eulerAngles.z >= correctAngle-0.1f;
			}
		}
		transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, Time.deltaTime * rotationSpeed);
	}

	void OnMouseDown()
	{
		dragEnabled = true;
		dragStartPosition = transform.position;
		dragStartDistance = (Camera.main.transform.position - transform.position).magnitude;
	}

	void Rotate(bool positive) {
		float angle = positive ? 90f : -90f;
		if (rotationAxis.Equals(Axis.x)) {
			toRot *= Quaternion.Euler (angle, 0f, 0f);
		} else if (rotationAxis.Equals(Axis.y)) {
			toRot *= Quaternion.Euler (0f, angle, 0f);
		} else if (rotationAxis.Equals(Axis.z)) {
			toRot *= Quaternion.Euler (0f, 0f, angle);
		}
	}

	void OnMouseDrag()
	{
		if (dragEnabled)
		{
			Vector3 worldDragTo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragStartDistance));
			transform.position = new Vector3(dragStartPosition.x, worldDragTo.y, dragStartPosition.z);
		}
	}
}
