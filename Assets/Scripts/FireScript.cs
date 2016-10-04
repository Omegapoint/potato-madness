using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FireScript : MonoBehaviour {

	public GameObject firetube;
	public GameObject spawnPoint;
	public GameObject potatoPrefab;
	public AudioClip[] fireSoundClips;
	public int shotForce = 3000;

	void OnEnable ()
	{
		EventManager.StartListening ("shotFired", ShotFired);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("shotFired", ShotFired);
	}

	// Update is called once per frame
	void Update () {
	}

	private void ShotFired() {		
		GameObject newPotato = (GameObject)Instantiate (potatoPrefab, spawnPoint.transform.position, 
			Quaternion.Euler (spawnPoint.transform.eulerAngles.x, spawnPoint.transform.eulerAngles.y, spawnPoint.transform.eulerAngles.z));
		newPotato.transform.eulerAngles = firetube.transform.eulerAngles;

		newPotato.name = "potatofire";
		Rigidbody potatoRigidBody = newPotato.GetComponent<Rigidbody> ();
		potatoRigidBody.AddForce (potatoRigidBody.transform.up * shotForce);
		PlayFireSound ();
	}

	private void PlayFireSound() {
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = fireSoundClips[Random.Range(0, fireSoundClips.Length)];
		audio.Play();
	}
}
