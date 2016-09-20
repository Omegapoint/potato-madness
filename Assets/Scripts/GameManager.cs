using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public Text scoreText;
	public int numberOfAvailabePotatoes = 10;
	public bool gameOver = false;

	private int totalNumberOfBricks;
	private int currentNumberOfBricks;
	private int numberOfPotatoesShot;

	// Use this for initialization
	void Start () {
		if (gm == null) {
			gm = this.gameObject.GetComponent<GameManager>();
		}

		totalNumberOfBricks = GameObject.FindGameObjectsWithTag ("block").Length;
		currentNumberOfBricks = totalNumberOfBricks;
	}

	public void shotFired() {
		numberOfPotatoesShot++;
		if (numberOfPotatoesShot == numberOfAvailabePotatoes) {
			gameOver = true;
		}
	}

	// Update is called once per frame
	void Update () {
		currentNumberOfBricks = GameObject.FindGameObjectsWithTag ("block").Length;
		scoreText.text = (totalNumberOfBricks - currentNumberOfBricks) + " av " + totalNumberOfBricks;
	}

}
