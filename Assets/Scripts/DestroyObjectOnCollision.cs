using UnityEngine;
using System.Collections;

public class DestroyObjectOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "block") {
			Debug.Log ("One block have been knocked down.");
		}
		Destroy(col.gameObject);
	}
}
