using UnityEngine;
using System.Collections;

public class PillarController : BeatListener {
	//Particle system to start/stop flame
	ParticleSystem pSystem;

	//Reference to the pillar s pings
	public PingController[] controllers;

	//Variable to time-out the flame
	int beatsLeft;

	//Variable to save what ping the beat is in
	int atPing;

	//Time between pings
	float timeBetweenPings = BeatListener.halfBeatTime * 1.5f;

	//Variable to keep track of time
	float lastStart;

	//If in its measure
	bool inMeasure;

	// Use this for initialization
	protected override void Start () {
		//Get the particle system
		pSystem = GetComponent<ParticleSystem>();

		//Initialize Beatsleft
		beatsLeft = 0;

		//Set ping to a start value
		atPing = 0;

		lastStart = 0;
	}

	protected override void Update ()
	{
		//Custom beat recognision since the pings are off-beat
		if(atPing * timeBetweenPings + 0.15f < (GetSongTime() - lastStart)
		   && inMeasure){
			if(atPing == 0) LightPing(6, 1);
			else if(atPing == 1) LightPing(6, 1);
			else if(atPing == 2) LightPing(5, 1);
			else if(atPing == 3) LightPing(5, 1);
			else if(atPing == 4) LightPing(4, 1);
			else if(atPing == 5) LightPing(4, 1);
			else if(atPing == 6) LightPing(3, 1);
			else if(atPing == 7) LightPing(3, 1);
			else if(atPing == 8) LightPing(2, 1);
			else if(atPing == 9) LightPing(2, 1);
			else if(atPing == 10) LightPing(4, 1);
			else if(atPing == 11) LightPing(4, 1);
			else if(atPing == 12) LightPing(5, 1);
			else if(atPing == 13) LightPing(5, 1);
			else if(atPing == 14) LightPing(3, 1);
			else if(atPing == 15) LightPing(3, 1);
			else if(atPing == 16) LightPing(2, 1);
			else if(atPing == 17) LightPing(2, 1);
			else if(atPing == 18) LightPing(1, 1);
			else if(atPing == 19) LightPing(1, 1);
			else LightPing (0, 0);

			//Increment atPing
			atPing++;
		}

		//Call base update function
		base.Update ();
	}

	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//Count down beatsLeft
		if(beatsLeft > 0){
			beatsLeft--;

			//Switch flame on if reached 0
			if(beatsLeft == 0){
				PillarOff();
			}
		}
	}

	protected override void Handle16 (int b64, int b32, int b16)
	{
		//In the 6th 64-bar
		if(b64 == 5){
			//In its measure
			inMeasure = true;

			//Reset which ping we re at
			atPing = 0;

			//Set the time of last start to current time again
			lastStart = GetSongTime();
		}else inMeasure = false;
	}

	//Function to switch flame on
	public void PillarOn(){
		PillarOn(0);
	}

	/// <summary>
	/// Switch flame on for i half beats.
	/// </summary>
	/// <param name="b">Amount of half beats in which the flame will turn off. 0 == infinite</param>
	public void PillarOn(int b){
		//Activate Flame
		pSystem.Play();

		//Set timer
		beatsLeft = b;
	}

	//Function to put out flame
	public void PillarOff(){
		//Stop emitting particles
		pSystem.Stop();
	}

	//Function to set start speed
	public void SetStartSpeed(float s){
		pSystem.startSpeed = s;
	}

	//Function to light a certain ping
	void LightPing(int p, float i){
		for(int j = 0; j <= controllers.Length; j++) 
			if (j == p && j != 0) 
				controllers[j - 1].SetIntensity(i);
			else if(j != 0)
				controllers[j - 1].SetIntensity(0);
	}
}
