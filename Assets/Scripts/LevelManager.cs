﻿using UnityEngine;
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


	Vector2 touchStartPos;
	float touchStartTime;

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
		
	private bool MovingBlocks() {
		GameObject[] movingBlocks = GameObject.FindGameObjectsWithTag ("block");
		bool hasMovement = false;
		foreach (GameObject block in movingBlocks) {
			hasMovement = hasMovement || block.GetComponent<BlockScript> ().IsMoving;	
		}
		return hasMovement;
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
		if (Input.touchCount > 0) {
			Touch touch = Input.touches[0];

			switch (touch.phase) {
			case TouchPhase.Began:
				touchStartPos = touch.position;
				touchStartTime = Time.time;
				break;
			case TouchPhase.Ended:
				Debug.Log ("time: " + (Time.time - touchStartTime));
				if ((Time.time - touchStartTime) < 0.5f && (touch.position - touchStartPos).magnitude < 1.0f) {
					// If the touch is short enough and the finger hasn't moved that moch we can fire the gun
					fire();
				}
				break;
			}
				
		} else {
			if (Input.GetButtonDown ("Fire1")) {
				fire ();
			}
		}
		if (numberOfPotatosDestroyed == level.numberOfBalls && !MovingBlocks () && activePlay) {
			InitiateFailedLevel ();
		}
	}

	void fire() {
		if (numberOfPotatosShot < level.numberOfBalls && activePlay) {
			numberOfPotatosShot++;
			EventManager.TriggerEvent ("shotFired");
		}
	}
}
