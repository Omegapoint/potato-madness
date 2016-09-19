using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireScript : MonoBehaviour {

	public GameObject firetube;
	public GameObject potatoPrefab;
	public AudioClip[] fireSoundClips;
	public int shotForce = 3000;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("rotation: " + firetube.transform.rotation);
			Debug.Log ("eulerAngles: " + firetube.transform.eulerAngles);
			GameObject newPotato = (GameObject)Instantiate (potatoPrefab, firetube.transform.position, 
				Quaternion.Euler (firetube.transform.eulerAngles.x, firetube.transform.eulerAngles.y, firetube.transform.eulerAngles.z));
			newPotato.transform.eulerAngles = firetube.transform.eulerAngles;

			newPotato.name = "potatofire";
			Rigidbody potatoRigidBody = newPotato.GetComponent<Rigidbody> ();
			potatoRigidBody.AddForce (potatoRigidBody.transform.up * shotForce);

			PlayFireSound ();
		} 
	}

	private void PlayFireSound() {
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = fireSoundClips[Random.Range(0, fireSoundClips.Length)];
		audio.Play();
	}
}
