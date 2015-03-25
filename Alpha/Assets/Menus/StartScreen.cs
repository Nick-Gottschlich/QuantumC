using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject light = GameObject.Find ("Directional Light");

		if (Application.loadedLevelName == "_Start_Screen") {
			light.transform.rotation = Quaternion.Euler(new Vector3(359.2033f, 322.2887f, 282.8511f));

			if (Input.GetKeyUp (KeyCode.Return))
				Application.LoadLevel ("_Level_Selection");
		}
		else if (Application.loadedLevelName == "_Level_Selection"){
			light.transform.rotation = Quaternion.Euler(new Vector3(26.75745f, 0.8023775f, 1.781754f));


			if(Input.GetKeyUp (KeyCode.I))
				Application.LoadLevel ("_Instructions");
			else if(Input.GetKeyUp (KeyCode.O))
				Application.LoadLevel ("_Objective");
		}
		else if (Application.loadedLevelName == "_Instructions"){
//			light.transform.rotation = Quaternion.Euler(new Vector3(51.84637f, 2.026001f, 2.575775f));
			light.transform.rotation = Quaternion.Euler(new Vector3(26.75745f, 0.8023775f, 1.781754f));

			if (Input.GetKeyUp (KeyCode.I) || Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.Escape))
				Application.LoadLevel ("_Level_Selection");
		}
		else if (Application.loadedLevelName == "_Objective"){
			light.transform.rotation = Quaternion.Euler(new Vector3(26.75745f, 0.8023775f, 1.781754f));

			if (Input.GetKeyUp (KeyCode.O) || Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.Escape))
				Application.LoadLevel ("_Level_Selection");
		}
	}
}
