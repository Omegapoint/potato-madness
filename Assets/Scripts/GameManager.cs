using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public int numberOfAvailabePotatoes = 10;
	public bool gameOver = false;

	public GameObject restartButtons;
	public GameObject startGameButton;

	private Text scoreText;
	private Text shotsLeftText;

	private int totalNumberOfBricks;
	private int currentNumberOfBricks;
	private int numberOfPotatoesShot;

	private string[] levels = { "Level 1", "Level 2" };
	private int currentLevel = -1;

	protected GameManager() {}

	// Use this for initialization
	void Start () {
		totalNumberOfBricks = GameObject.FindGameObjectsWithTag ("block").Length;
		currentNumberOfBricks = totalNumberOfBricks;

		UpdateGameState ();

	}

	public void ShotFired() {
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

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void StartGame() {
		currentLevel = 0;
		UpdateGameState ();
		Application.LoadLevel (levels[currentLevel]);
	}

	private void UpdateGameState() {
		scoreText = GameObject.FindGameObjectWithTag ("scoreText").GetComponent<Text>();
		shotsLeftText = GameObject.FindGameObjectWithTag ("shotsLeftText").GetComponent<Text>();

		startGameButton.SetActive (currentLevel == -1);
		restartButtons.SetActive (gameOver);
		scoreText.enabled = !gameOver && currentLevel >= 0;
		shotsLeftText.enabled = !gameOver && currentLevel >= 0;
	}
		
}
