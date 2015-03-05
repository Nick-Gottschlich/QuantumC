using UnityEngine;
using System.Collections;

public enum Axis {x, y, z}

public class LinearPlatform : MonoBehaviour {

	public GameObject trigger;

	private Vector3 screenpoint;
	private Vector3 offset;

	Vector3 dragStartPosition;
	float dragStartDistance;

	public bool rotation = false;
	public Axis rotationAxis = Axis.x;
	public float rotationSpeed = 50f;
	public float correctAngle = 0f;
	public static bool correctPosition = false;
	Quaternion toRot = Quaternion.identity;


	// Gizmos
	void OnDrawGizmos() {
//		Gizmos.color = Color.green;
//		Gizmos.DrawLine (transform.position, trigger.transform.position);
	}


	// Use this for initialization
	void Start () {
		toRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {


		if (rotation) {
//			if (trigger.GetComponent<Pad>().rotate_activate == true) {
//				Rotate();	
//			}

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

	public void Rotate() {
		float angle = 90f;
		if (rotationAxis.Equals(Axis.x)) {
			toRot *= Quaternion.Euler (angle, 0f, 0f);
		} else if (rotationAxis.Equals(Axis.y)) {
			toRot *= Quaternion.Euler (0f, angle, 0f);
		} else if (rotationAxis.Equals(Axis.z)) {
			toRot *= Quaternion.Euler (0f, 0f, angle);
		}

		//transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, Time.deltaTime * rotationSpeed);
	}


}
