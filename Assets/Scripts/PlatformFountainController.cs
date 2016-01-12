using UnityEngine;
using System.Collections;

public class PlatformFountainController : BeatListener {
	//Goal variable for smooth transition
	float goalSpeed;

	//Transition speed;
	public float transitionSpeed;

	// Use this for initialization
	protected override void Start () {
		goalSpeed = 0;

		base.Start();
	}

	protected override void Update ()
	{
		GetComponent<ParticleSystem>().startSpeed = Mathf.Lerp(GetComponent<ParticleSystem>().startSpeed, goalSpeed, transitionSpeed * Time.deltaTime);
		                                     

		base.Update ();
	}

	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		if(b64 == 8) GetComponent<ParticleSystem>().startColor = new Color(1, 1, 1, (float)(bh % 128) / 127f);
		if(b64 == 13) GetComponent<ParticleSystem>().startColor = new Color(1, 1, 1, 1f - (float)(bh % 128) / 127f);
	}

	protected override void Handle64 (int b64)
	{
		//Set the goal speeds at different measures
		if(b64 == 8) goalSpeed = 2;
		if(b64 == 11) goalSpeed = 0;
	}
}
