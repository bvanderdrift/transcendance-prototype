using UnityEngine;
using System.Collections;

public class TerrainFieldController : BeatListener {
	//Reference to the particle systems
	ParticleSystem[] systems;

	// Use this for initialization
	protected override void Start () {
		//Load the systems
		systems = GetComponentsInChildren<ParticleSystem>();
	}

	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//The two flashes of the sea
		if(b64 >= 1 && b64 <= 13) 
			if(b8 % 8 == 0)
				SetIntensity(1f - ((float)(bh % 16) / 15f));
			else
				SetIntensity(0.5f * (1f - ((float)(bh % 128 - 16) / (64f - 16f))));

		//Make sure the particle system only works when visible
		if(systems[0].startColor.a == 0)
			StopField ();
		else
			StartField ();
	}

	void SetIntensity(float i){
		foreach(ParticleSystem system in systems) system.startColor = new Color(1, 1, 1, i);
	}

	void StartField(){
		foreach(ParticleSystem system in systems) system.Play();
	}

	void StopField(){
			
			foreach(ParticleSystem system in systems) system.Stop();
	}
}
