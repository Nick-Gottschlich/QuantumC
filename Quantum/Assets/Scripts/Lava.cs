using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Restart level
		if (transform.position.y > 7.2f)
			Application.LoadLevel(Application.loadedLevel);
	
	}

	// Edited in Project settings that it only 
	// collides with player!
	void OnCollisionEnter(Collision c) {

		// Restart level
		//Application.LoadLevel(Application.loadedLevel);
		
		if (c.gameObject.tag == "Player") {
			GameRunner.killedByLava();
		}
	}
}
