using UnityEngine;
using System.Collections;

public class CubeStackController : BeatListener {
	//Cube renderers
	MeshRenderer[] cubes;

	//The light of the cubestack
	new Light light;

	// Use this for initialization
	protected override void Start () {
		//Get the mesh renderers of the cubes
		cubes = GetComponentsInChildren<MeshRenderer>();

		//Get the light
		light = GetComponentInChildren<Light>();

		//Start as invisible
		LightLevel(0);

		base.Start();
	}
	
	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//Var to check what beat nr is in the bar
		int currentBarBeat = bh % 16;
		
		//Check if should be active
		if(b64 >= 8 && b64 < 11){
			if(currentBarBeat == 14){
				LightLevel(b8 % 2 == 0 ? 3 : 4);
			}else if(currentBarBeat == 15){
				LightLevel(b8 % 2 == 0 ? 4 : 3);
			}else{
				LightLevel(currentBarBeat % 3 + 1);
			}
		}else{
			LightLevel(0);
		}
	}

	//Function to light the stack to a certain level
	void LightLevel(int l){
		for(int i = 0; i < 5; i++){
			cubes[i].material.color = new Color(PublicVars.colors[l].r, PublicVars.colors[l].g, PublicVars.colors[l].b, i < l ? 1 : 0);
		}

		//Set the lights intensity
		light.intensity = l;
		light.color = PublicVars.colors[l];
	}

}