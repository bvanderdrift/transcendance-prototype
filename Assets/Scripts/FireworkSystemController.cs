using UnityEngine;
using System.Collections;

public class FireworkSystemController : BeatListener {
	//Firework references
	public Transform[] fireworks;

	//Maximum angle of the fireworks
	public float maximumRotation;

	// Use this for initialization
	protected override void Start () {

		base.Start();
	}

	protected override void Handle8 (int b64, int b32, int b16, int b8)
	{
		if(b64 > 9 && b64 <= 11) Fire ((int)(Random.value * 4));
	}

	//Function to fire a piece of firework, where i is the index
	void Fire(int i){
		//Create an instance of the firework
		Transform firework = (Transform)Instantiate(fireworks[Mathf.Min(Mathf.Max(0, i), fireworks.Length)], transform.position, Quaternion.identity);

		//Make it part of this system
		firework.parent = transform;

		//Get the controller of the instantiated piec
		FireworkController controller = firework.GetComponent<FireworkController>();

		//Set the explosion time to 2 beats later
		controller.secondsToExplosion = BeatListener.halfBeatTime * 4;

		//Fire the piece in a random angle
		controller.Fire(800, new Vector2((maximumRotation / 45f) * 2 * (Random.value - 0.5f), maximumRotation / 45f * 2 * (Random.value - 0.5f)));
	}
}
