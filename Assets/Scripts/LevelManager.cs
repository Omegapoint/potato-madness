using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private GameLevel level;
	private int numberOfPotatosShot;
	private int numberOfPotatosDestroyed;
	private int numberOfTargetsKnockedDown;
	private int startingNumberOfTargets;
	private bool activePlay = true;

	void OnEnable ()
	{
		EventManager.StartListening ("targetKnockedDown", TargetKnockedDown);
		EventManager.StartListening ("potatoDestroyed", PotatoDestroyed);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("targetKnockedDown", TargetKnockedDown);
		EventManager.StopListening ("potatoDestroyed", PotatoDestroyed);
	}

	void TargetKnockedDown() {
		numberOfTargetsKnockedDown++;
		if (numberOfTargetsKnockedDown == startingNumberOfTargets) {
			InitiateNextLevel ();
		}
	}

	void PotatoDestroyed() {
		numberOfPotatosDestroyed++;
		if (numberOfPotatosDestroyed == level.numberOfBalls) {
			InitiateFailedLevel ();
		}
	}

	void InitiateNextLevel() {
		activePlay = false;
		if (GameManager.gm.IsFinalLevel()) {
			EndLevelSplash.Create (new UnityAction (NextLevel), "Roll credits", "Congratulations, you beat the game!");
		} else {
			EndLevelSplash.Create (new UnityAction (NextLevel), "Next Level", "You've made it to the next level!");
		}
	}

	void InitiateFailedLevel() {
		activePlay = false;
		EndLevelSplash.Create (new UnityAction (FailedLevel), "Restart", "Game Over");
	}
				
	public void NextLevel() {
		GameManager.gm.NextLevel ();
	}

	public void FailedLevel() {
		GameManager.gm.GameOver ();
	}

	public int ShotsLeft() {
		return level.numberOfBalls - numberOfPotatosShot;
	}

	public int TargetsLeft() {
		return startingNumberOfTargets - numberOfTargetsKnockedDown;
	}

	public int StartingNumberOfTargets() {
		return startingNumberOfTargets;
	}

	// Use this for initialization
	void Start () {
		level = GameManager.gm.CurrentLevel ();

		RenderSettings.skybox = level.skybox;
		GameObject table = GameObject.Find ("Table");

		GameObject boxes = Instantiate (level.levelBlocks, Vector3.zero, Quaternion.identity) as GameObject;
		Instantiate(level.scoreCanvas, Vector3.zero, Quaternion.identity);
		boxes.transform.parent = table.transform;
		boxes.transform.position = table.transform.position;
		boxes.transform.localScale = level.levelBlocks.transform.localScale;

		startingNumberOfTargets = level.levelBlocks.GetComponent<Transform> ().childCount;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Fire2")) {
			if (numberOfPotatosShot < level.numberOfBalls) {
				numberOfPotatosShot++;
				EventManager.TriggerEvent ("shotFired");
			}
		}
	}

}
