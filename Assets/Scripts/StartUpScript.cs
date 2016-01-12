using UnityEngine;
using System.Collections;

public class StartUpScript : MonoBehaviour {	
	//PublicVars script reference
	PublicVars pVars;
	
	// Use this for initialization
	void Start () {
		//Get pVar class
		pVars = GetComponent<PublicVars>();
		
		//Silent the sources
		pVars.loopSource.volume = 0;
		pVars.fullSource.volume = 0;
		
		//Set audiocource for all controllers
		BeatListener.SetAudioSource(pVars.fullSource);

		//Mouse setting
		Cursor.visible = false;
		//Screen.lockCursor = true;
	}
	
	void Update () {
		//Fade the sound in
		if(!PublicVars.started){
			pVars.loopSource.volume = Mathf.Lerp(pVars.loopSource.volume, PublicVars.musicLowVol, PublicVars.musicFadeSpeed * Time.deltaTime);
			pVars.fullSource.volume = Mathf.Lerp(pVars.fullSource.volume, PublicVars.musicLowVol, PublicVars.musicFadeSpeed * Time.deltaTime);
		}
	}
}
