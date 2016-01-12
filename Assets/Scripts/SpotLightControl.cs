using UnityEngine;
using System.Collections;

public class SpotLightControl : MonoBehaviour {
	//Place for the cone renderer
	MeshRenderer cone;

	//Variables for fading (these are destination variables)
	float intensity;
	float transparancy;

	//Reference to lights
	Light[] lights;

	//Fadespeed variable
	public float fadeSpeedIn;
	public float fadeSpeedOut;
	
	// Use this for initialization
	void Awake () {
		//Get the cone
		cone = GetComponentInChildren<MeshRenderer>();

		//Get the lights
		lights = GetComponentsInChildren<Light>();
	}

	void Update() {
		//Code for fading instead of just switching
		foreach(Light l in lights){
			if(l.transform != transform) l.intensity = Mathf.Lerp(l.intensity, 8 * intensity, (l.intensity < intensity ? fadeSpeedIn : fadeSpeedOut) * Time.deltaTime);
		}

		GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, intensity, (GetComponent<Light>().intensity < intensity ? fadeSpeedIn : fadeSpeedOut) * Time.deltaTime); 

		cone.material.color = new Color(1, 1, 1, Mathf.Lerp(cone.material.color.a, transparancy, (GetComponent<Light>().intensity < intensity ? fadeSpeedIn : fadeSpeedOut) * Time.deltaTime));
	}

	//Intensity functions
	public void SetIntensity(float i){
		intensity = i;
		transparancy = i * 130f/255f;
	}
	
	public float GetIntensity(){
		return intensity;
	}
}
