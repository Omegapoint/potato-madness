using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class LookAtScript : MonoBehaviour {

	public GameObject target;//the target object
	public int rotationStep = 3;
	public float positiveVerticalViewLimit = 10.0f;
	public float negativeVerticalViewLimit = -10.0f;

	private Vector3 point;//the coord to the point where the camera looks at

	void Start () {//Set up things on the start method
		point = target.transform.position;//get target's coords
		transform.LookAt(point);//makes the camera look to it
	}

	void Update () {//makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier
		float changeHorizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
		float changeVerticalAxis = -CrossPlatformInputManager.GetAxis("Vertical");
		point.Set (point.x, point.y + getLimitedVerticalChange(changeVerticalAxis), point.z);
		transform.LookAt(point);
		transform.RotateAround(point, new Vector3(0.0f, changeHorizontalAxis, 0.0f), rotationStep);
	}

	float getLimitedVerticalChange(float rawChange) {
		if (point.y + rawChange > positiveVerticalViewLimit) {
			return positiveVerticalViewLimit - point.y;
		} else if (point.y + rawChange < negativeVerticalViewLimit) {
			return negativeVerticalViewLimit - point.y;
		} else {
			return rawChange;
		}
	}

}
