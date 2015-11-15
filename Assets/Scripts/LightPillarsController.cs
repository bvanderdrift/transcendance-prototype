using UnityEngine;
using System.Collections;

public class LightPillarsController : BeatListener {
	//All the Flame controllers
	PillarController[] controllers;

	// Use this for initialization
	protected override void Start () {
		//Get all the flame controllers
		controllers = GetComponentsInChildren<PillarController>();

		//Call base start
		base.Start();
	}
	
	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{		
		//Get what halfbeatnr it is in 8bar
		int hBeatIn8 = bh % 16;
		
		//In the 6th 64bar
		if(b64 == 5){
			//In the 2nd and 6th 8 bar
			if(b8 % 8 == 1 || b8 % 8 == 5){
				if(hBeatIn8 == 0  || hBeatIn8 == 3){
					Fire(2);
				}
				if(hBeatIn8 == 6) {
					Fire(9);
				}
			}else if(b8 % 8 == 3){
				//Fire for full bar
				if(hBeatIn8 == 0) Fire (15);

				//Slowly increase firespeed
				SetStartSpeed(((float)(bh % 16) / 16f) * 3000);
			}else if(b8 % 8 == 7){
				//Fire for full bar
				if(hBeatIn8 == 0) Fire (15);

				if(bh % 2 == 0){
					//Slowly increase firespeed
					SetStartSpeed(((float)(bh % 16) / 16f) * 3000);
				}else{
					SetStartSpeed(0);
				}
			}else{
				//Any other case reset the start speed to normal
				SetStartSpeed(1000);
			}
		}
	}

	//Function to fire 100% of flames
	void Fire(){
		Fire(0);
	}

	//Function to fire 100% of flames for set amount of beats
	void Fire(int b){
		//Iterate past all controllers
		foreach(PillarController controller in controllers){
			controller.PillarOn(b);
		}
	}

	//Function to set start speed
	void SetStartSpeed(float s){
		//Iterate past all controllers
		foreach(PillarController controller in controllers){
			controller.SetStartSpeed(s);
		}
	}
}
