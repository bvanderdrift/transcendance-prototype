using UnityEngine;
using System.Collections;

public class OverheadColumnController : BeatListener {
	//Array for the 4 lights
	Light[] lights;
	MeshRenderer[] renderers;

	// Use this for initialization
	override protected void Start () {
		//Get the lights
		lights = GetComponentsInChildren<Light>();
		
		//Get the renderes
		renderers = GetComponentsInChildren<MeshRenderer>();
		
		//Set everything to dark
		SetIntensity(0);
		
		base.Start();
	}
	
	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//Var to check what beat nr is in the bar
		int currentBarBeat = bh % 16;
		int current16Beat = bh % 32;
		
		//Check if should be active
		if(b64 < 7 || b64 == 12 || b64 == 13){
			//Intensity
			float intensity = 0;

			//Set correct intensity
			if(b64 < 3)
				intensity = Mathf.Lerp(0, 8, ((bh - 64f) / 128f));
			else if(b64 == 3 || b64 == 12)
				intensity = 2 - currentBarBeat % 3 == 1 ? 0 : 0.3f;
			else if(b64 == 4 || b64 == 6 || b64 == 13){
				if(b16 % 4 == 0){
					intensity = 0.4f;
				}else if (b16 % 4 == 1){
					intensity = Mathf.Lerp (0.4f, 1, current16Beat / 31f);
				}else if (b16 % 4 == 2){
					intensity = 1;
				}else{
					intensity = Mathf.Lerp (1, 0, current16Beat / 31f);
				}
			}

			//You dont hear the 2nd ping in the second b64 bar
			if(b64 < 2 && 2 - currentBarBeat % 3 == 1) intensity = 0;

			//Don t hear 2nd ping in last measure but you do have the intensity pattern of 4 & 6
			if(b64 == 12 && 2 - currentBarBeat % 3 == 1) intensity = 0.3f;
			if(b64 == 13 && 2 - currentBarBeat % 3 == 1) intensity = 0;

			//Set color to white
			SetColor(Color.white);

			//Choreographed part
			if(currentBarBeat == 14){
				SetOnlyIntensity(2, intensity);
			}else if(currentBarBeat == 15){
				SetOnlyIntensity(b8 % 2 == 0 ? 3 : 0, intensity);
			}else{
				SetOnlyIntensity(2 - currentBarBeat % 3, intensity);
			}
		}else if(b64 >= 7 && b64 < 11){
			if(currentBarBeat == 14){
				SetOnlyIntensity(b8 % 2 == 0 ? 2 : 3, 8);
				SetColor(PublicVars.colors[b8 % 2 == 0 ? 2 : 3]);
 			}else if(currentBarBeat == 15){
				SetOnlyIntensity(b8 % 2 == 0 ? 3 : 2, 8);
				SetColor(PublicVars.colors[b8 % 2 == 0 ? 3 : 2]);
			}else{
				SetOnlyIntensity(currentBarBeat % 3, 8);
				SetColor(PublicVars.colors[currentBarBeat % 3]);
			}
		}else{
			SetIntensity(0);
		}
	}
	
	//Set intensity of all lights
	void SetIntensity(float i){
		for(int j = 0; j < 4; j++){
			lights[j].intensity = i;
			renderers[j].material.color = new Color(1, 1, 1, i);
		}
	}
	
	//Set intensity per light
	void SetIntensity(int l, float i){
		lights[l].intensity = i;
		renderers[l].material.color = new Color(1, 1, 1, i);
	}
	
	//Set intensity of one, rest is dark
	void SetOnlyIntensity(int l, float i){
		for(int j = 0; j < 4; j++){
			lights[j].intensity = j == l ? i : 0;
			renderers[j].material.color = new Color(1, 1, 1, j == l ? i : 0);
		}
	}

	//No alpha can be passed here since that is relevent to the intensity
	void SetColor(Color color){
		for(int j = 0; j < 4; j++){
			lights[j].color = color;
		}
	}
}
