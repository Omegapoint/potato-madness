using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public Object welcomeScene;
	public Object creditsScene;
	public GameLevel[] levels;

	private int currentLevelIndex = 0;

	protected GameManager() {}

	// Use this for initialization
	void Start () {
		if (gm == null) {
			gm = this.gameObject.GetComponent<GameManager>();
		}
	}

	public GameLevel CurrentLevel() {
		return levels [currentLevelIndex];
	}

	public LevelManager CurrentLevelManager() {
		return Camera.main.GetComponent<LevelManager>();
	}

	public void NextLevel() {
		if (currentLevelIndex + 1 < levels.Count()) {
			currentLevelIndex++;
			StartGame ();
		} else {
			RollCredits ();
		}
	}

	public bool IsFinalLevel() {
		return currentLevelIndex + 1 == levels.Count ();
	}

	public void GameOver() {
		currentLevelIndex = 0;
		SceneManager.LoadScene(welcomeScene.name);
	}

	public void RollCredits() {
		SceneManager.LoadScene (creditsScene.name);
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void StartGame() {
		string sceneName = CurrentLevel ().sceneAssetName;
		SceneManager.LoadScene (sceneName);
	}

}
