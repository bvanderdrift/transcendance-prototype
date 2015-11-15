using UnityEngine;
using System.Collections;

public class CircleController : BeatListener {
	//Variable which states the amount of sphere
	public int sphereAmount;

	//Variable for the radius of the circle
	public float radius;

	//Sphere Prefab reference
	public Transform sphere;

	//Array for all the spheres;
	Transform[] spheres;

	//Controllers of the spheres
	SphereController[] controllers;

	//Rotationspeed variable
	public float rotationSpeed;

	//Actual Y position of circle
	float circleY;

	//Extra Y position because of bounce
	float bounceY;

	//Y extra because of bouncing
	public float bounceHeight;

	//Destination variable for smoothing Y transition
	float yGoal;

	//Variable to set speed to which the wheel transitions between y coordinates
	public float ySpeed;

	//Variable to represent Alpha destination
	float alphaGoal;

	//Variable to set fade speed
	public float fadeSpeed;

	// Use this for initialization
	override protected void Start () {
		//Create the spheres arrays
		spheres = new Transform[sphereAmount];
		controllers = new SphereController[sphereAmount];

		//Calculate the phase steps
		float step = 2 * Mathf.PI / sphereAmount;

		//Create the amount of spheres
		for(int i = 0; i < sphereAmount; i++){
			//Create sphere
			spheres[i] = (Transform)Instantiate(sphere, new Vector3(radius * Mathf.Cos(i * step), transform.position.y, radius * Mathf.Sin(i * step)), Quaternion.identity);

			//Make it a child of the circle
			spheres[i].parent = transform;

			//Get the controllers
			controllers[i] = spheres[i].GetComponent<SphereController>();

			//Set the sphere rotation adjunct to camera
			spheres[i].Rotate(new Vector3(0, -360f/(float)sphereAmount * i, 0));
		}

		//Save the circles Y pos and initialize the bounce Y pos
		yGoal = (circleY = transform.position.y);
		bounceY = 0;

		//Initialize alphaGoal
		alphaGoal = 0;

		//Call base start
		base.Start();
	}

	//Update function
	protected override void Update ()
	{
		//Rotate the circle
		transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

		//Phase in the beat
		float bouncePhase = (BeatListener.GetSongTime() % (2 * BeatListener.halfBeatTime)) / (2 * BeatListener.halfBeatTime);

		//Set the bounceheight
		bounceY = bounceHeight * Mathf.Sin(bouncePhase * Mathf.PI);

		//Approach the y goal
		circleY = Mathf.Lerp(circleY, yGoal, ySpeed * Time.deltaTime);

		//Set the new height
		transform.position = new Vector3(transform.position.x, circleY + bounceY, transform.position.z);

		//Approach alpha goal
		SetAlpha(Mathf.Lerp(GetAlpha(), alphaGoal, fadeSpeed * Time.deltaTime));

		//Call base function
		base.Update();
	}

	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//When dropped, make visible
		if(b64 >= 7){
			SetAlphaGoal(1);
		}
	}

	protected override void Handle1 (int b64, int b32, int b16, int b8, int b4, int b2, int b1)
	{
		//When second drop make bounce every kick
		if(b64 >= 9){
			GoToY(yGoal == 300 ? 500 : 300);
		}
	}

	//Function to approach an alpha value
	void SetAlphaGoal(float a){
		alphaGoal = a;
	}

	//Function to set alpha of all spheres
	void SetAlpha(float i){
		for(int j = 0; j < sphereAmount; j++){
			controllers[j].SetAlpha(i);
		}
	}

	//Function to get alpha
	float GetAlpha(){
		return controllers[0].GetAlpha();
	}

	//Function to set a Y coordinate as height goal
	void GoToY(float y){
		yGoal = y;
	}
}
