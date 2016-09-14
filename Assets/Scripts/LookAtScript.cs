using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class LookAtScript : MonoBehaviour {

	public GameObject target;//the target object
	public int rotationStep = 3;

	private Vector3 point;//the coord to the point where the camera looks at

	void Start () {//Set up things on the start method
		point = target.transform.position;//get target's coords
		transform.LookAt(point);//makes the camera look to it
	}

	void Update () {//makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier
		float changeHorizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
		transform.RotateAround(point, new Vector3(0.0f, changeHorizontalAxis, 0.0f), rotationStep);
	}
}
