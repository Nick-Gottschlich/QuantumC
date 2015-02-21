using UnityEngine;
using System.Collections;

public class LinearPlatform : MonoBehaviour {

	private Vector3 screenpoint;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDrag() {
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, gameObject.transform.position.y, gameObject.transform.position.z);
		Vector3 curPos = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPos;

	}

	void OnMouseDown() {
		offset = Camera.main.WorldToScreenPoint(gameObject.transform.position)
			- Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, gameObject.transform.position.y, gameObject.transform.position.z));
	}

}
