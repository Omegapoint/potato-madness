using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private GameLevel level;
	private Canvas scoreCanvas;
	private int numberOfPotatoesShot;

	void OnEnable ()
	{
		EventManager.StartListening ("shotFired", ShotFired);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("shotFired", ShotFired);
	}

	public void ShotFired() {
		numberOfPotatoesShot++;
		if (numberOfPotatoesShot == level.numberOfBalls) {
			GameManager.gm.EndGame ();
		}
	}

	public int ShotsLeft() {
		return level.numberOfBalls - numberOfPotatoesShot;
	}
		
	// Use this for initialization
	void Start () {
		level = GameManager.gm.CurrentLevel ();

		RenderSettings.skybox = level.skybox;
		GameObject table = GameObject.Find ("Table");

		GameObject boxes = Instantiate (level.levelBlocks, Vector3.zero, Quaternion.identity) as GameObject;
		scoreCanvas = Instantiate(level.scoreCanvas, Vector3.zero, Quaternion.identity) as Canvas;
		boxes.transform.parent = table.transform;
		boxes.transform.position = table.transform.position;
		boxes.transform.localScale = level.levelBlocks.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		


}
