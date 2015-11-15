using UnityEngine;
using System.Collections;

public class PlatformLightsController : BeatListener {
	//Radius distance between circles
	public float radiusStep;

	//Amount of circles
	public int circleCount;

	//Amount of lights in circle
	public int lightsPerCircle;

	//Reference to platform light
	public Transform platformLight;

	//Variable for controllers per circle
	PlatformLightController[,] controllers;

	// Use this for initialization
	protected override void Start () {
		//Initialize the controller array
		controllers = new PlatformLightController[circleCount,lightsPerCircle];

		//Code for circle placement
		for(int i = 0; i < circleCount; i++){
			//Iterate past every light per circle
			for(int j = 0 ; j < lightsPerCircle; j++){
				//Calculate the phase
				float phase = ((float)j / (float)lightsPerCircle) * 2 * Mathf.PI;

				//Calculate the radius
				float radius = radiusStep * (i + 1);

				//Create the new platform light
				Transform newPlatformLight = (Transform)Instantiate(platformLight, new Vector3(radius * Mathf.Sin(phase), -2.6f, radius * Mathf.Cos (phase)), Quaternion.identity);

				//Set it in the hiarchy
				newPlatformLight.parent = transform;

				//Load the controller
				controllers[i,j] = newPlatformLight.GetComponent<PlatformLightController>();
			}
		}
	}

	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//Variable for syncing
		int in8thMeasure = bh % 16;
		bool inFirstOf2 = b8 % 2 == 0;

		if(b64 == 1 || b64 == 2 || b64 == 11){
			//Set the color
			SetColor(Color.white);

			//Set blinking on
			SetBlinking(true);

			//Choreographed part
			if(in8thMeasure >= 0 && in8thMeasure < 3)
				SetTillRingIntensity(inFirstOf2 ? 1 : 4, 1, true);
			else if(in8thMeasure >= 3 && in8thMeasure < 6)
				SetTillRingIntensity(inFirstOf2 ? 3 : 5, 1, true);
			else
				SetTillRingIntensity(inFirstOf2 ? 2 : 4, 1, true);
		}else if(b64 >= 7 && b64 <= 10){
			//Set blinking to false
			SetBlinking(false);

			//Set intensity
			SetIntensity(1);

			//Choreographed part
			for(int i = 1; i <= circleCount; i += 2) SetRingColor(i, b1 % 2 == 0 ? Color.green : Color.red);
			for(int i = 2; i <= circleCount; i += 2) SetRingColor(i, b1 % 2 == 0 ? Color.red : Color.green);

			//Nested choreography
			if(b64 == 9 || b64 == 10){

				//Choreographed part
				if(in8thMeasure == 14)
					SetRingColor(inFirstOf2 ? 3 : 4, Color.white);
				else if(in8thMeasure == 15)
					SetRingColor(inFirstOf2 ? 4 : 3, Color.white);
				else
					SetRingColor(in8thMeasure % 3 + 1, Color.white);
			}
		}else{
			SetIntensity(0);
		}
	}

	//Function to set a intensity of all rings
	void SetIntensity(float i){
		for(int j = 1; j <= circleCount; j++) SetRingIntensity(j, i);
	}

	//Function only set intensity of one ring, dim the rest
	void SetOnlyRingIntensity(int r, float i){
		for(int j = 1; j <= circleCount; j++) SetRingIntensity(j, j == r ? i : 0);
	}

	//Set the intensity of a certain ring
	void SetRingIntensity(int r, float i){
		for(int j = 0; j < lightsPerCircle; j++) controllers[r - 1, j].SetIntensity(i);
	}

	//Function to set intensity till a certain ring
	void SetTillRingIntensity(int r, float i, bool restDimmed){
		for(int j = 1; j <= (restDimmed ? circleCount : r); j++) SetRingIntensity(j, j <= r ? i : 0);
	}

	//Function to set color till a certain ring
	void SetTillRingColor(int r, Color c){
		for(int j = 1; j <= r; j++) SetRingColor(j, c);
	}

	//Function to set a rings intensity
	void SetRingColor(int r, Color c){
		for(int j = 0; j < lightsPerCircle; j++) controllers[r - 1, j].SetColor(c);
	}

	//Function to set a rings intensity
	void SetColor(Color c){
		for(int j = 1; j <= circleCount; j++) SetRingColor(j, c);
	}

	//Set if the lights are blinking or not
	void SetBlinking(bool blinking){
		for(int i = 0; i < circleCount; i++)
			for(int j = 0; j < lightsPerCircle; j++)
				controllers[i, j].SetBlinking(blinking);
	}
}
