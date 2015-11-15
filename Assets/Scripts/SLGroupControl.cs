using UnityEngine;
using System.Collections;

public class SLGroupControl : BeatListener {
	//All the controls of the spotlights
	SpotLightControl[] slc;
	
	//Boolean to see if group starts
	public bool start;

	//Time every 0.75 beat takes
	public float offBeatTime = BeatListener.halfBeatTime * 1.5f;

	//Variable to save the time of the start of 8 measure
	public float start8Measure;

	//Variable to keep track of the offbeat
	public int atOffBeat;

	//Variable to keep track if listening to offbeat
	public bool listeningToOffBeat;

	// Use this for initialization
	override protected void Start () {
		//Get the controls
		slc = GetComponentsInChildren<SpotLightControl>();
		
		//Disable all lights
		foreach(SpotLightControl sl in slc){
			sl.SetIntensity(0);
		}

		//Initialize the offbeat sync
		start8Measure = 0;
		atOffBeat = 0;
		listeningToOffBeat = false;
		
		base.Start();
	}

	protected override void Update ()
	{
		if(listeningToOffBeat && BeatListener.GetSongTime() >= start8Measure + atOffBeat * offBeatTime){
			//Choreographed part
			if(atOffBeat < 5){
				float intensity = 0.25f;
				if(atOffBeat % 2 == 0)
					SetIntensity(start ? intensity : 0);
				else
					SetIntensity(start ? 0 : intensity);
			}else{
				SetIntensity(0);
			}

			//Incrment atOffBeat
			atOffBeat++;
		}

		base.Update();
	}

	protected override void Handle64 (int b64)
	{
		//Set listening to off beat on or off at good measure
		if(b64 == 4 || b64 == 6)
			listeningToOffBeat = true;
		else
			listeningToOffBeat = false;

		base.Handle64 (b64);
	}

	protected override void Handle8 (int b64, int b32, int b16, int b8)
	{
		//Reset start8Measure every 8 measure in 4rd 64 measure
		start8Measure = BeatListener.GetSongTime();
		atOffBeat = 0;
	}

	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//Switch the spotlights on if they have to start every 16measure
		if(bh % 16 == 0 && b64 <= 2 && start) SetIntensity(1);
		else
		//Switch the spotlights every beat
		if(bh % 16 != 0 && b64 <= 2){
			if(bh % 2 == 0) SetIntensity(GetIntensity() == 1 ? 0 : 1);
			//On the last beat all should be on
			else if(bh % 16 == 15)	SetIntensity(1);
		}else if(!listeningToOffBeat) SetIntensity(0);
	}

	//Set intensity function of all spotlights
	void SetIntensity(float i){
		foreach(SpotLightControl sl in slc){
			sl.SetIntensity(i);
		}
	}

	//Get instensity function of all spotlights
	float GetIntensity(){
		return slc[0].GetIntensity();
	}
}
