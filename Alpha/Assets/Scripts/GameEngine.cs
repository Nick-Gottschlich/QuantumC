using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	public static bool isHeavyMode = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H)) {
			isHeavyMode = !isHeavyMode;
		}
	}
}
