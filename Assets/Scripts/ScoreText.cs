using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class ScoreText : MonoBehaviour {

	private Text scoreText;

	void Start() {
		scoreText = gameObject.GetComponent<Text>();
		UpdateText ();
	}

	void OnEnable ()
	{
		EventManager.StartListening ("targetKnockedDown", TargetKnockedDown);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("targetKnockedDown", TargetKnockedDown);
	}

	void TargetKnockedDown ()
	{
		UpdateText ();
	}

	void UpdateText() {
		scoreText.text = GameManager.gm.CurrentLevelManager().TargetsLeft() + " of " + 
			GameManager.gm.CurrentLevelManager().StartingNumberOfTargets() + " left";
	}


}
