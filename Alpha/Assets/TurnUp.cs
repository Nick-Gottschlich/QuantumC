using UnityEngine;
using System.Collections;

public class TurnUp : MonoBehaviour {

	public Transform startMarker;
	public Transform endMarker;

	private float startTime;
	private float journeyLength;

	public float speed = 5f;

	// Use this for initialization
	void Start () {
		//endMarker = this.transform;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
		transform.rotation = startMarker.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, endMarker.rotation, fracJourney/2);
	}
}
