using UnityEngine;
using System.Collections;

public class StartListener : BeatListener {	
	//PublicVars script reference
	PublicVars pVars;

	//Gui Text Reference
	GUIText[] texts;
	public Transform guiTextHolder;

	//Camera reference for platform heightning
	public Transform platformContainer;

	//Platform height
	public float platformHeight;

	//Platform speed
	public float platformSpeed;

	//Volume destination variable for smooth transitioning
	float volumeDestination;
	
	// Use this for initialization
	protected override void Start () {		
		//Get the public vars
		pVars = GetComponent<PublicVars>();

		texts = guiTextHolder.GetComponentsInChildren<GUIText>();

		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		//Check if the player wants to start
		if(
			(	
		 		//On PC
				Input.GetKeyDown(KeyCode.Space)  ||
				//On Android
		 		(Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended) ||
		 		//On GamePad
		 		(Input.GetButtonDown("Home") || Input.GetButtonDown("Jump"))
		 	)
		   && !PublicVars.started && !pVars.fullSource.isPlaying){
			//Set started to true
			PublicVars.started = true;
			
			//Make sure the song starts after the next cue-loop ends
			pVars.fullSource.PlayDelayed(pVars.loopSource.clip.length - pVars.loopSource.time);
			
			//Make sure the cue-loop stops
			pVars.loopSource.loop = false;
		}
		
		//Fade the sources to full volume when started and make the GUI text disappear
		if(PublicVars.started){
			pVars.loopSource.volume = Mathf.Lerp(pVars.loopSource.volume, 1, PublicVars.musicFadeSpeed * Time.deltaTime);
			pVars.fullSource.volume = Mathf.Lerp(pVars.fullSource.volume, 1, PublicVars.musicFadeSpeed * Time.deltaTime);

			//Up the camera
			platformContainer.position = new Vector3(0, Mathf.Lerp(platformContainer.position.y, platformHeight, platformSpeed * Time.deltaTime));

			foreach(GUIText text in texts){
				//Get aValue for simplicity
				float aValue = Mathf.Lerp(text.color.a, -1f, PublicVars.GUIFadeSpeed * Time.deltaTime);

				//Set the text color
				text.color = new Color(1, 1, 1, aValue);
			}
		}else{
			pVars.loopSource.volume = Mathf.Lerp(pVars.loopSource.volume, PublicVars.musicLowVol, PublicVars.musicFadeSpeed * Time.deltaTime);
			pVars.fullSource.volume = Mathf.Lerp(pVars.fullSource.volume, PublicVars.musicLowVol, PublicVars.musicFadeSpeed * Time.deltaTime);
			
			//Back down the camera
			platformContainer.position = new Vector3(0, Mathf.Lerp(platformContainer.position.y, 0, platformSpeed * Time.deltaTime));
			
			foreach(GUIText text in texts){
				//Get aValue for simplicity
				float aValue = Mathf.Lerp(text.color.a, 2, PublicVars.GUIFadeSpeed * Time.deltaTime);
				
				//Set the text color
				text.color = new Color(1, 1, 1, aValue);
			}
		}

		//Call base update
		base.Update();
	}

	//Make sure the beat returns to start when the song ends
	protected override void Handle64 (int b64)
	{
		//Make sure the loopsource starts playing exactly on time, it won t if at b64 = 14
		if(b64 == 13) pVars.loopSource.PlayDelayed(128 * BeatListener.halfBeatTime);
		if(b64 == 14){
			//Set started to false
			PublicVars.started = false;

			//Start the loop again
			pVars.loopSource.loop = true;
		}
	}
}
