using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShotsLeft : MonoBehaviour {

	private Text scoreText;

	void Start() {
		scoreText = gameObject.GetComponent<Text>();
		UpdateText ();
	}

	void OnEnable ()
	{
		EventManager.StartListening ("shotFired", ShotFired);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("shotFired", ShotFired);
	}

	void UpdateText() {
		scoreText.text = GameManager.gm.currentLevel.ShotsLeft() + " shots left";
	}

	void ShotFired ()
	{
		Text scoreText = gameObject.GetComponent<Text>();
		Debug.Log ("ShotFired Function was called!");
		UpdateText ();
	}
}
