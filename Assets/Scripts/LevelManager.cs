using UnityEngine;
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
			GameManager.gm.EndGame ();
		}
	}

	void TargetKnockedDown() {
		numberOfTargetsKnockedDown++;
		if (numberOfTargetsKnockedDown == startingNumberOfTargets) {
			GameManager.gm.NextLevel ();
		}
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
