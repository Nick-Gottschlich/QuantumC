using UnityEngine;
using System.Collections;


public class Pad : MonoBehaviour {

	public bool teleport;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (teleport) {
					
		}
	}
	void OnGizmosDraw() {
		if (teleport) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere (transform.position, 1f);
		}
	}
}
