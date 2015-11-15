using UnityEngine;
using System.Collections;

public class FireworkController : MonoBehaviour {
	//Variable for fire speed
	public float fireSpeed;
	public float secondsToExplosion;

	//Particle Systems
	public ParticleSystem tail;
	public ParticleSystem explosion;

	//Boolean to check if fired and exploded
	bool fired;
	bool exploded;

	// Use this for initialization
	void Awake () {
		//Initialize the fired & exploded variable
		fired = false;
		exploded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(fired && !exploded){
			//Decrease time till explosion
			secondsToExplosion -= Time.deltaTime;

			//Explode if time is up
			if(secondsToExplosion <= 0) Explode();
		}else if(exploded && !explosion.isPlaying) Destroy(gameObject);
	}

	public void Fire(){
		//Do nothing if fired
		if(fired) return;
		
		//Set fired to true
		fired = true;
		
		//Fire upwards
		rigidbody.AddForce(fireSpeed * Vector3.up, ForceMode.Impulse);
		
		//Start the tail particle emitter
		tail.Play();
	}

	public void Fire(float fireSpeed){
		//Do nothing if fired
		if(fired) return;
		
		//Set fired to true
		fired = true;
		
		//Fire upwards
		rigidbody.AddForce(fireSpeed * Vector3.up, ForceMode.Impulse);
		
		//Start the tail particle emitter
		tail.Play();
	}

	public void Fire(float fireSpeed, Vector2 Angle){
		//Do nothing if fired
		if(fired) return;
		
		//Set fired to true
		fired = true;
		
		//Fire upwards
		rigidbody.AddForce(fireSpeed * new Vector3(Angle.x, 1, Angle.y), ForceMode.Impulse);
		
		//Start the tail particle emitter
		tail.Play();
	}

	//Function to make the rocket explode
	void Explode(){
		//Do nothing if exploded
		if(exploded) return;

		//Set exploded to true
		exploded = true;

		//Change the particle system
		tail.Stop();
		explosion.Play();

		//Stop the rigidbody from moving and being affected by gravity
		rigidbody.velocity = Vector3.zero;
		rigidbody.useGravity = false;
	}
}
