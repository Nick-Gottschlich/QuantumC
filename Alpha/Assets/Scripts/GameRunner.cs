using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour {
	static public bool P1colGoal = false;
	static public bool P2colGoal = false;
	
	public Text winText;

	//GameObject P1 = GameObject.Find("Player1");
	//GameObject P2 = GameObject.Find("Player2");

	// Use this for initialization
	void Start () {
		GameObject winDisp = GameObject.Find("WinDisp");
		winText = winDisp.GetComponent<Text>();
		
		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (P1colGoal && P2colGoal) {
			winText.text = "You Win!";

			if(Input.GetKeyUp(KeyCode.Return))
				Application.LoadLevel("_Level_Selection");
		}
	}
}
