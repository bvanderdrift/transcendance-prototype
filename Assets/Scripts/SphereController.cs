using UnityEngine;
using System.Collections;

public class SphereController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Make sure the particles are transparant
		GetComponent<ParticleSystem>().startColor = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Function to set transparancy
	public void SetAlpha(float a){
		//Set the particle transparance
		GetComponent<ParticleSystem>().startColor = new Color(1, 1, 1, a);
	}

	//Function to get transparancy
	public float GetAlpha(){
		return GetComponent<ParticleSystem>().startColor.a;
	}
}
