using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Restart : MonoBehaviour {

	private Button button;

	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(GameManager.gm.GameOver);
	}

}
