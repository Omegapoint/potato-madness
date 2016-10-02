using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class EndLevelSplash : MonoBehaviour {

	void SetUp(UnityAction action, string buttonText, string splashText) {
		Debug.Log ("SetUp EndLevelSplash");
		var button = gameObject.GetComponentInChildren<Button> ();
		button.onClick.AddListener (action);
		button.GetComponentInChildren<Text>().text = buttonText;

		var text = gameObject.GetComponentInChildren<Text> ();
		text.text = splashText;
	}

	public static void Create(UnityAction action, string buttonText, string splashText) {
		GameObject endLevelSplashPrefab = Resources.Load ("Prefab/EndLevelSplash/Canvas") as GameObject;
		GameObject endLevelSplash = Instantiate (endLevelSplashPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		endLevelSplash.GetComponent<EndLevelSplash> ().SetUp(action, buttonText, splashText);
	}

}
