using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { set; get;}

	private bool gameIsStarted = false;

	public Text scoreTxt, coinTxt, modifierTxt;

	private PlayerMovement movment;
	private float score, coinScore, modifierScore;


	private void Awake(){
		Instance = this;
		UpdateScores ();
		movment = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
	}

	public void UpdateScores(){
		scoreTxt.text = score.ToString();
		coinTxt.text = coinScore.ToString ();
		modifierTxt.text = modifierScore.ToString ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (MobileInputs.Instance.Tap && !gameIsStarted) {
			gameIsStarted = true;
			movment.StartRunning ();
		}
	}
}
