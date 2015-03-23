using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "_Start_Screen") {
			if (Input.GetKeyUp (KeyCode.Return))
				Application.LoadLevel ("_Level_Selection");

		}
		else if (Application.loadedLevelName == "_Level_Selection"){
			if(Input.GetKeyUp (KeyCode.I))
				Application.LoadLevel ("_Instructions");
			else if (Input.GetKeyUp (KeyCode.Return))
				Application.LoadLevel ("_Instructions");
			else if(Input.GetKeyUp (KeyCode.O))
				Application.LoadLevel ("_Objective");
		}
		else if (Application.loadedLevelName == "_Instructions"){
			if (Input.GetKeyUp (KeyCode.Return))
				Application.LoadLevel ("_Level_Selection");
		}
		else if (Application.loadedLevelName == "_Objective"){
			if (Input.GetKeyUp (KeyCode.Return))
				Application.LoadLevel ("_Level_Selection");
		}
	}
}
