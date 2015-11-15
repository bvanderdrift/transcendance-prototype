using UnityEngine;
using System.Collections;

public class StarsController : BeatListener {
	public Transform starSystemPrefab;

	ParticleSystem[] starSystems;

	// Use this for initialization
	void Start () {
		NewSystem();
		SetStarLifeTime(1);

		base.Start();
	}
	
	// Update is called once per frame
	void Update () {

		base.Update();
	}

	protected override void Handle1 (int b64, int b32, int b16, int b8, int b4, int b2, int b1)
	{
		if(b64 == 9 || b64 == 10 || b64 == 11){
			SetStarLifeTime(2);
			Fire (1000);
		}

		base.Handle1 (b64, b32, b16, b8, b4, b2, b1);
	}

	protected override void Handle8 (int b64, int b32, int b16, int b8)
	{
		if(b64 == 3 || b64 == 4){
			SetStarLifeTime(16);

			if(b8 % 8 == 0)
				Fire (500);

			if(b8 % 8 == 1)
				Fire (150);
		}
	}

	void Fire(int amountOfParticles){
		foreach(ParticleSystem stars in starSystems){
			if(!(stars.particleCount >= stars.maxParticles)){
				stars.Emit(Mathf.Min(amountOfParticles, stars.maxParticles - stars.particleCount));
				amountOfParticles -= Mathf.Min(amountOfParticles, stars.maxParticles - stars.particleCount);

				//Stop 
				if(amountOfParticles <= 0){
					return;
				}
			}
		}

		NewSystem();
		Fire (amountOfParticles);
	}

	void NewSystem(){
		Transform nStars = ((Transform)Instantiate(starSystemPrefab));
		nStars.parent = transform;

		if(starSystems != null && starSystems.Length >= 0){
			nStars.particleSystem.startLifetime = starSystems[0].startLifetime;
		}

		starSystems = GetComponentsInChildren<ParticleSystem>();
	}

	void SetStarLifeTime(int beats){
		foreach(ParticleSystem stars in starSystems){
			stars.startLifetime = ((float)beats / 0.5f) * halfBeatTime;
		}
	}
}
