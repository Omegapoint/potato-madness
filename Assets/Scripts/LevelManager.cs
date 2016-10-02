using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private GameLevel level;
	private int numberOfPotatoesShot;
	private int numberOfTargetsKnockedDown;
	private int startingNumberOfTargets;

	void OnEnable ()
	{
		EventManager.StartListening ("shotFired", ShotFired);
		EventManager.StartListening ("targetKnockedDown", TargetKnockedDown);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("shotFired", ShotFired);
		EventManager.StopListening ("targetKnockedDown", TargetKnockedDown);
	}

	void ShotFired() {
		numberOfPotatoesShot++;
		if (numberOfPotatoesShot == level.numberOfBalls) {
			InitiateFailedLevel ();
		}
	}

	void TargetKnockedDown() {
		numberOfTargetsKnockedDown++;
		if (numberOfTargetsKnockedDown == startingNumberOfTargets) {
			InitiateNextLevel ();
		}
	}

	void InitiateNextLevel() {
		UnityAction action = new UnityAction (NextLevel);
		CreateEndLevelSplash (action, "Next Level", "You've made it to the next level!");
	}

	void InitiateFailedLevel() {
		UnityAction action = new UnityAction (FailedLevel);
		CreateEndLevelSplash (action, "Restart", "Game Over");
	}
		
	void CreateEndLevelSplash(UnityAction action, string buttonText, string splashText) {
		GameObject canvasPrefab = Resources.Load ("Prefab/EndLevel/Canvas") as GameObject;
		GameObject buttonPrefab = Resources.Load ("Prefab/EndLevel/Button") as GameObject;
		GameObject textPrefab = Resources.Load ("Prefab/EndLevel/Text") as GameObject;

		GameObject canvas = Instantiate (canvasPrefab, Vector3.zero, Quaternion.identity) as GameObject;

		var buttonIcon = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		buttonIcon.GetComponentInChildren<Text>().text = buttonText;
		var button = buttonIcon.GetComponent<Button>();
		button.onClick.AddListener(action);
		buttonIcon.transform.SetParent (canvas.transform, false);

		GameObject textContainer = Instantiate (textPrefab, new Vector3 (0, 60, 0), Quaternion.identity) as GameObject;
		Text text = textContainer.GetComponent<Text>();
		text.text = splashText;
		textContainer.transform.SetParent (canvas.transform, false);
	}
		
	public void NextLevel() {
		GameManager.gm.NextLevel ();
	}

	public void FailedLevel() {
		GameManager.gm.EndGame ();
	}

	public int ShotsLeft() {
		return level.numberOfBalls - numberOfPotatoesShot;
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
	
	}

}
