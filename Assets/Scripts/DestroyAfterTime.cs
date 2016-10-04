using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float timeBeforeDestruction;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject, timeBeforeDestruction);
	}

	void OnDestroy() {
		EventManager.TriggerEvent ("potatoDestroyed");
	}
}
