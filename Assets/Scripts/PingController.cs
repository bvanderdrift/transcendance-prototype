using UnityEngine;
using System.Collections;

public class PingController : MonoBehaviour {
	//Variables for fading
	float intensityGoal;
	public float fadeInSpeed;
	public float fadeOutSpeed;

	// Use this for initialization
	void Start () {
		intensityGoal = 0;
		GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().material.color = new Color(1, 1, 1, Mathf.Lerp(GetComponent<Renderer>().material.color.a, intensityGoal, (GetComponent<Renderer>().material.color.a < intensityGoal ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime));
	}

	//Function te set intensity
	public void SetIntensity(float i){
		GetComponent<Renderer>().material.color = new Color(1, 1, 1, i);
	}
}
