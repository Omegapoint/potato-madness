using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ScoreText : MonoBehaviour {

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
		Debug.Log ("TargetKnockedDown Function was called!");
	}

}
