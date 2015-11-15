using UnityEngine;
using System.Collections;

public class PingFieldController : BeatListener {
	//Reference for directionallight
	Light pinglight;
	
	//Reference to music player for time
	AudioSource connected;
	
	//Var to keep track of how long a light should last
	float timeLeft;
	
	//Start function
	override protected void Start(){
		//Start with timeleft = 0
		timeLeft = 0;
		
		//Get light
		pinglight = GetComponentInChildren<Light>();
		
		//Call base function
		base.Start();
	}
	
	//Update function
	override protected void Update(){
		//Dim light when time runs out
		if(timeLeft > 0){
			timeLeft -= Time.deltaTime;
		}else{
			if(pinglight.intensity != 0){
				SetIntensity(0);
			}
		}
		
		//Update base
		base.Update();
	}
	
	//Event handle function
	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		timeLeft = 0.1f;
		SetIntensity(1);
	}
	
	//Function to set field intensity
	public void SetIntensity(float i){
		//Set intensity
		pinglight.intensity = i;
	}
}
