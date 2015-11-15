using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour {
	//To detect press events and not states
	bool justPressedT;
	bool justPressedP;
	
	//String for printing output
	string output;
	
	//Public Variables reference
	PublicVars pVars;
	
	// Use this for initialization
	void Start () {
		//Get pVar class
		pVars = GetComponent<PublicVars>();
		
		//Initialize justpressed
		justPressedT = false;
		justPressedP = false;	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Code for printing times to time for events when the player presses T
		if(Input.GetKeyDown(KeyCode.T) && !justPressedT){
			justPressedT = true;

			print(pVars.fullSource.time);
		//When the player releases T
		}else if(Input.GetKeyUp(KeyCode.T) && justPressedT){
			justPressedT = false;	
		}
		
		//When the player presses P
		if(Input.GetKeyDown(KeyCode.P) && !justPressedP){
			justPressedP = true;
			
		//When the player releases T
		}else if(Input.GetKeyUp(KeyCode.P) && justPressedP){
			justPressedP = false;	
		}

		//Exit the game when the user presses escape
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}
}
