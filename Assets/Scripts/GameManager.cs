using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public Text scoreText;
	public Text shotsLeftText;
	public int numberOfAvailabePotatoes = 10;
	public bool gameOver = false;

	public GameObject restartButtons;
	public string restartLevelToLoad;

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

		if (restartButtons) {
			restartButtons.SetActive (false);
		}

	}

	public void shotFired() {
		numberOfPotatoesShot++;
		if (numberOfPotatoesShot == numberOfAvailabePotatoes) {
			EndGame ();
		}
	}

	// Update is called once per frame
	void Update () {
		shotsLeftText.text = (numberOfAvailabePotatoes - numberOfPotatoesShot)  + " shots left";
		currentNumberOfBricks = GameObject.FindGameObjectsWithTag ("block").Length;
		scoreText.text = (totalNumberOfBricks - currentNumberOfBricks) + " of " + totalNumberOfBricks;
	}

	void EndGame() {
		gameOver = true;
		restartButtons.SetActive (true);
	}

}
