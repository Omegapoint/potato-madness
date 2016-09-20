using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Text scoreText;

	private int totalNumberOfBricks;
	private int currentNumberOfBricks;

	// Use this for initialization
	void Start () {
		totalNumberOfBricks = GameObject.FindGameObjectsWithTag ("block").Length;
		currentNumberOfBricks = totalNumberOfBricks;
	}
	
	// Update is called once per frame
	void Update () {
		currentNumberOfBricks = GameObject.FindGameObjectsWithTag ("block").Length;
		scoreText.text = (totalNumberOfBricks - currentNumberOfBricks) + " av " + totalNumberOfBricks;
	}
}
