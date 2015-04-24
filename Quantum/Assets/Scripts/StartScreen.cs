using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	GameObject selector;
	int selectorPos = 0;
	Vector3 [] optionsPos;
	string [] optionScenes;

	// Use this for initialization
	void Start () {
		LevelSelect.initLevels ();
		Time.timeScale = 1;
		if (Application.loadedLevelName == "_Start_Screen")
			InitStartScreenOptions ();
	}

	void InitStartScreenOptions() {
		selector = GameObject.Find("Selector");

		optionScenes = new string[3] {"_Transition_1", "_Level_Selection", "_Instructions"};
		optionsPos = new Vector3[3];

		optionsPos[0] = selector.transform.position;
		optionsPos[1] = new Vector3(optionsPos[0].x, optionsPos[0].y - 1.4f, optionsPos[0].z);
		optionsPos[2] = new Vector3(optionsPos[1].x, optionsPos[1].y - 1.4f, optionsPos[1].z);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GameRunner.loadingMessage();
			Application.LoadLevel("_Start_Screen");
		}

		if (Application.loadedLevelName == "_Start_Screen") {
			StartScreenRun ();
		}
		else if (Application.loadedLevelName == "_Level_Selection"){
			LevelSelection ();
		}
		else if (Application.loadedLevelName == "_Success"){
			Success ();
		}
		
		if (Application.loadedLevelName == "_Instructions") {
			if (Input.GetKey(KeyCode.Return)) {
				GameRunner.loadingMessage();
				Application.LoadLevel("_Start_Screen");
			}
		}
				
	}

	void StartScreenRun() {
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
			MoveSelector(-1);
		} else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
			MoveSelector(1);
		} else if (Input.GetKeyDown(KeyCode.Return)) {
			GameRunner.loadingMessage();
			Application.LoadLevel(optionScenes[selectorPos]);
		}
	}

	void MoveSelector(int dir) {
		selectorPos += dir;
		selectorPos = selectorPos % 3;
		selectorPos = selectorPos < 0 ? Mathf.Abs (selectorPos-1) : Mathf.Abs(selectorPos);
		selector.transform.position = optionsPos[selectorPos];
	}

	void LevelSelection() {
		GameObject light = GameObject.Find ("Directional Light");
		light.transform.rotation = Quaternion.Euler(new Vector3(21.91902f, 12.48193f, 2.010067f));
		
		if(Input.GetKeyUp (KeyCode.I)) {
			GameRunner.loadingMessage();
			Application.LoadLevel ("_Instructions");
		}
		else if(Input.GetKeyUp (KeyCode.O)) {
			GameRunner.loadingMessage();
			Application.LoadLevel ("_Objective");
		}
	}

	void Success() {
		GameObject light = GameObject.Find ("Directional Light");
		light.transform.rotation = Quaternion.Euler(new Vector3(21.91902f, 12.48193f, 2.010067f));
		//			light.transform.rotation = Quaternion.Euler(new Vector3(26.75745f, 0.8023775f, 1.781754f));
		
		if (Input.GetKeyUp (KeyCode.Return)) {
			GameRunner.loadingMessage();
			Application.LoadLevel ("_Start_Screen");
		}
	}

}
