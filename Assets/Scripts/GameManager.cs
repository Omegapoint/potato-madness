using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public bool gameOver = false;

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

	public void EndGame() {
		gameOver = true;
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void StartGame() {
		SceneAsset scene = CurrentLevel ().sceneAsset;
		SceneManager.LoadScene (scene.name);
	}

}
