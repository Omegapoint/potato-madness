using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public int numberOfAvailabePotatoes = 10;
	public bool gameOver = false;

	public GameObject restartGameButton;
	public GameObject startGameButton;

	private Text scoreText;
	private Text shotsLeftText;

	private int totalNumberOfBricks;
	private int currentNumberOfBricks;
	private int numberOfPotatoesShot;

	// TODO The SceneManager has a scene build index that could be used instead of currentLevel
	// TODO The SceneManager could also most likely replace the levels array
	private string[] levels = { "Level 1", "Level 2" };
	private int currentLevel = -1;

	protected GameManager() {}

	// Use this for initialization
	void Start () {
		if (gm == null) {
			gm = this.gameObject.GetComponent<GameManager>();
		}
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
		restartGameButton.SetActive (true);
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		SceneManager.activeSceneChanged += OnSceneLoaded;

		// TODO No need to recreate the score texts?
		startGameButton = GameObject.FindGameObjectWithTag("startGameButton");
		restartGameButton = GameObject.FindGameObjectWithTag ("restartGameButton");
	}
		
	public void StartGame() {
		currentLevel = 0;
		SceneManager.LoadScene (levels [currentLevel]);
	}

	void OnSceneLoaded (Scene scene, Scene scene2) {
		UpdateGameState ();
	}

	// TODO Cleanup, this is getting messy
	private void UpdateGameState() {
		startGameButton.SetActive (currentLevel == -1);
		restartGameButton.SetActive (gameOver);

		totalNumberOfBricks = GameObject.FindGameObjectsWithTag ("block").Length;
		currentNumberOfBricks = totalNumberOfBricks;

		GameObject score = GameObject.FindGameObjectWithTag ("scoreText");
		GameObject shotsLeft = GameObject.FindGameObjectWithTag("shotsLeftText");

		if (currentLevel == -1) {
			score.SetActive (false);
			shotsLeft.SetActive (false);
		} else {
			score.SetActive (true);
			shotsLeft.SetActive (true);
		}

		scoreText = score.GetComponent<Text>();
		shotsLeftText = shotsLeft.GetComponent<Text>();

		scoreText.enabled = !gameOver && currentLevel >= 0;
		shotsLeftText.enabled = !gameOver && currentLevel >= 0;
	}
		
}
