using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireScript : MonoBehaviour {

	public GameObject firetube;
	public GameObject spawnPoint;
	public GameObject potatoPrefab;
	public AudioClip[] fireSoundClips;
	public int shotForce = 3000;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			GameObject newPotato = (GameObject)Instantiate (potatoPrefab, spawnPoint.transform.position, 
				Quaternion.Euler (spawnPoint.transform.eulerAngles.x, spawnPoint.transform.eulerAngles.y, spawnPoint.transform.eulerAngles.z));
			newPotato.transform.eulerAngles = firetube.transform.eulerAngles;

			newPotato.name = "potatofire";
			Rigidbody potatoRigidBody = newPotato.GetComponent<Rigidbody> ();
			potatoRigidBody.AddForce (potatoRigidBody.transform.up * shotForce);
			PlayFireSound ();
			GameManager.gm.shotFired ();
		} 
	}

	private void PlayFireSound() {
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = fireSoundClips[Random.Range(0, fireSoundClips.Length)];
		audio.Play();
	}
}
