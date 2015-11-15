using UnityEngine;
using System.Collections;

public class PublicVars : MonoBehaviour {
	//Public music variables
	static public float musicFadeSpeed = 0.3f;
	static public float musicLowVol = 0.1f;

	//GUI fade speed
	static public float GUIFadeSpeed = 0.4f;
	
	//Bool to check if started
	static public bool started = false;

	//Color array of my hip colors
	static public Color[] colors =
	{
		Color.red,
		Color.blue,
		Color.green,
		Color.cyan,
		Color.yellow,
		Color.magenta
	};
	
	//The Audio Sources
	public AudioSource loopSource;
	public AudioSource fullSource;
}
