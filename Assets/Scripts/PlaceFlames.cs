using UnityEngine;
using System.Collections;

public class PlaceFlames : MonoBehaviour {
	//Array with all flame transforms
	Transform[] fTransforms;

	//Min and max radius for placement
	public float radius;

	// Function to place
	void Start () {
		//Get all transforms
		fTransforms = GetComponentsInChildren<Transform>();

		//Iterate through each flame and set their locations
		foreach(Transform flame in fTransforms){
			//If not for this the field own transform would chose to
			if(flame.parent == transform){
				//Get the position
				Vector2 position = radius * Vector2.one;

				//Set the flames position
				flame.localPosition = new Vector3(position.x, flame.localPosition.y, position.y);
			}
		}
	}
}
