using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	public GameObject firetube;
	public GameObject potatoPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("Tänt var det här!");
			Debug.Log (Input.mousePosition);
			//GameObject potato = GameObject.Find("Potato");

			Debug.Log (firetube.transform.position);
			GameObject newPotato = (GameObject)Instantiate(potatoPrefab, firetube.transform.position, firetube.transform.rotation);
			newPotato.name = "fire";
			Rigidbody potatoRigidBody = newPotato.GetComponent<Rigidbody> ();
			potatoRigidBody.velocity = Vector3.zero;
			//potatoRigidBody.isKinematic = true;
			potatoRigidBody.AddForce(transform.forward * 30000);

			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		} 
	}
}
