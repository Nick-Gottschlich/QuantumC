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
	void OnMouseDown() {
		screenpoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position
			- Camera.main.ScreenToWorldPoint(new Vector3(screenpoint.x, Input.mousePosition.y, screenpoint.z));
//		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (screenpoint);

//		Vector3 yay = new Vector3 (0, screenpoint.y, 0);
//		offset = screenpoint - new Vector3 (0, Input.mousePosition.y, 0);

		print ("On Mouse Down" + offset);
	}
	void OnMouseDrag() {
		Vector3 curScreenPoint = new Vector3 (screenpoint.x, Input.mousePosition.y , screenpoint.z);
		Vector3 curPos = Camera.main.ScreenToWorldPoint(curScreenPoint + offset);
//		Vector3 curPos = transform.position;
//		curPos += new Vector3(0, Input.mousePosition.y
		transform.position = curPos;
//		print ("On Mouse Drag, Before: " + Camera.main.ScreenToWorldPoint(curScreenPoint) + "After: " + curPos);
		print (screenpoint);
	}


}
