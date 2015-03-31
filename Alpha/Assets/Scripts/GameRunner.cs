using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour {
	static public bool P1colGoal = false;
	static public bool P2colGoal = false;
	
	static bool dead = false;
	
	public Text winText;
	public Text pauseText;
	public static Text deathText;

	//GameObject P1 = GameObject.Find("Player1");
	//GameObject P2 = GameObject.Find("Player2");

	// Use this for initialization
	void Start () {
		GameObject winDisp = GameObject.Find("WinDisp");
		winText = winDisp.GetComponent<Text>();
		
		GameObject PauseDisp = GameObject.Find("Pause");
		pauseText = PauseDisp.GetComponent<Text>();
		
		GameObject DeathDisp = GameObject.Find("Death");
		deathText = DeathDisp.GetComponent<Text>();
		
		winText.text = "";
		pauseText.text = "";
		deathText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		//get rid of or change this in the future
		if (P1colGoal && P2colGoal) {
			winText.text = "You Win!";
			
		deathText.text = "";

			if(Input.GetKeyUp(KeyCode.Return))
				Application.LoadLevel("_Level_Selection");
		}
		
		//reset level on press of R key
		if (Input.GetKeyUp (KeyCode.R)) {
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
			dead = false;
		}
		
		//press esc to pause and unpause level
		if (Input.GetKeyUp (KeyCode.Escape) && dead == false) {
			if (Time.timeScale == 0) {
				pauseText.text = "";
				Time.timeScale = 1;
			}
			else if (Time.timeScale == 1) {
				Time.timeScale = 0;
				pauseText.text = "Pause";
			}	
		}	
	}
	
	public static void killedByEnemy() {
		Time.timeScale = 0;
		dead = true;
		deathText.text = "ALL \n SYSTEMS \n CORRUPTED";
	}
}
