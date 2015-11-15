using UnityEngine;
using System.Collections;

public class PlatformLightController : MonoBehaviour {
	//Color of the light
	Color color;

	//Intensity of the light
	float intensity;

	//Reference to the light
	new Light light;

	//Variable to keep track if blinking
	bool blinking;

	//Fade speed variable
	public float fadeSpeed;

	//The dark color
	const float darkIntensity = 0.1f;
	Color dark = new Color(darkIntensity, darkIntensity, darkIntensity);

	// Use this for initialization
	void Start () {
		//Get the light
		light = GetComponentInChildren<Light>();

		//Set start intensity
		intensity = 0;

		//Set start color
		color = Color.white;

		//Set start blinking value
		blinking = false;
	}

	//Update function
	void Update(){
		//Color intensity, this is for better visual results
		float colorIntensity = Mathf.Sqrt(intensity) * (1 - darkIntensity);

		//Set the materials color
		renderer.material.color = new Color(dark.r + colorIntensity * color.r, dark.g + colorIntensity * color.g, dark.b + colorIntensity * color.b, 1);

		//Set intensity back to 0 if blinking
		if(blinking) intensity = Mathf.Lerp(intensity, 0, fadeSpeed * Time.deltaTime);

		//Set the lights intensity
		light.intensity = 8 * intensity;

		//Set the lights color
		light.color = color;
	}

	//Function te set blinking on or off
	public void SetBlinking(bool blinking){
		this.blinking = blinking;
	}

	//Set the color of the light
	public void SetColor(Color color){
		this.color = color;
	}

	//Set the intensity of the light
	public void SetIntensity(float intensity){
		this.intensity = intensity;
	}
}
