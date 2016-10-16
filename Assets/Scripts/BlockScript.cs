using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	//Set this to the transform you want to check
	private Transform objectTransfom;

	private float noMovementThreshold = 0.0001f;
	private const int noMovementFrames = 3;
	Vector3[] previousLocations = new Vector3[noMovementFrames];
	private bool isMoving;

	private bool isDestroyed = false;

	//Let other scripts see if the object is moving
	public bool IsMoving
	{
		get{ return isMoving; }
	}

	void Start() {
		objectTransfom = gameObject.transform;
	}

	void Awake()
	{
		//For good measure, set the previous locations
		for(int i = 0; i < previousLocations.Length; i++)
		{
			previousLocations[i] = Vector3.zero;
		}
	}

	void Update()
	{
		//Store the newest vector at the end of the list of vectors
		for(int i = 0; i < previousLocations.Length - 1; i++)
		{
			previousLocations[i] = previousLocations[i+1];
		}
		previousLocations[previousLocations.Length - 1] = objectTransfom.position;
		//Check the distances between the points in your previous locations
		//If for the past several updates, there are no movements smaller than the threshold,
		//you can most likely assume that the object is not moving
		for(int i = 0; i < previousLocations.Length - 1; i++)
		{
			if(Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold)
			{
				isMoving = true;
				// Safe guard if a object misses the death plane.
				if (gameObject.transform.position.y < -200 && !isDestroyed) {
					Destroy (gameObject);
					isDestroyed = true;
					EventManager.TriggerEvent ("targetKnockedDown");
				}
			}
			else
			{
				isMoving = false;
				break;
			}
		}
	}
}
