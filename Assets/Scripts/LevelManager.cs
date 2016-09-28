using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private GameLevel level;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load(GameLevel level) {
		this.level = level;

		StartCoroutine(LoadNewScene());
	}


	IEnumerator LoadNewScene() {
		Scene scene = SceneManager.GetSceneByName("LevelScene");
		AsyncOperation async = SceneManager.LoadSceneAsync("LevelScene");

		while (!async.isDone) {
			yield return null;
		}

		RenderSettings.skybox = level.skybox;
		GameObject table = GameObject.Find ("Table");

		GameObject boxes = Instantiate (level.levelBlocks, Vector3.zero, Quaternion.identity) as GameObject;
		boxes.transform.parent = table.transform;
		boxes.transform.position = table.transform.position;
		boxes.transform.localScale = level.levelBlocks.transform.localScale;
	}
}
