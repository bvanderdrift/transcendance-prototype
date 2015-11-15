using UnityEngine;
using System.Collections;

public abstract class BeatListener : MonoBehaviour
{	
	//Integer to keep track of what event comes next
	private int currentEvent;
	
	//Requires times var
	virtual protected void Start(){
		//Set to start
		currentEvent = 0;
	}
	
	virtual protected void Update(){
		//Check if the next event has passed
		if(source.time > halfBeatTime * currentEvent){
			//Execute handle functions
			HandleHalf (currentEvent / 128, currentEvent / 64, currentEvent / 32, currentEvent / 16, currentEvent / 8, currentEvent / 4, currentEvent / 2, currentEvent); 
			
			if(currentEvent % 2 == 0) {
				Handle1(currentEvent / 128, currentEvent / 64, currentEvent / 32, currentEvent / 16, currentEvent / 8, currentEvent / 4, currentEvent / 2); 
			}
			if(currentEvent % 4 == 0) {
				Handle2(currentEvent / 128, currentEvent / 64, currentEvent / 32, currentEvent / 16, currentEvent / 8, currentEvent / 4); 
			}
			if(currentEvent % 8 == 0) {
				Handle4(currentEvent / 128, currentEvent / 64, currentEvent / 32, currentEvent / 16, currentEvent / 8); 
			}
			if(currentEvent % 16 == 0) {
				Handle8(currentEvent / 128, currentEvent / 64, currentEvent / 32, currentEvent / 16);
			}
			if(currentEvent % 32 == 0) {
				Handle16(currentEvent / 128, currentEvent / 64, currentEvent / 32);
			}
			if(currentEvent % 64 == 0) {
				Handle32(currentEvent / 128, currentEvent / 64);
			}
			if(currentEvent % 128 == 0) {
				Handle64(currentEvent / 128);
			}
			
			//Increment to detect next beat
			currentEvent++;
		}

		//Reset the beatlistener if the song isnt playing 
		if(!source.isPlaying) currentEvent = 0;
	}
	
	virtual protected void HandleHalf(int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh){}
	
	virtual protected void Handle1(int b64, int b32, int b16, int b8, int b4, int b2, int b1){}
	
	virtual protected void Handle2(int b64, int b32, int b16, int b8, int b4, int b2){}
	
	virtual protected void Handle4(int b64, int b32, int b16, int b8, int b4){}
	
	virtual protected void Handle8(int b64, int b32, int b16, int b8){}
	
	virtual protected void Handle16(int b64, int b32, int b16){}
	
	virtual protected void Handle32(int b64, int b32){}
	
	virtual protected void Handle64(int b64){}
	
	//STATIC PART -- END OF CLASS PART
	
	//AudioSource reference
	static protected AudioSource source;
	
	//Time half a beat takes
	static public float halfBeatTime = 0.46875f / 2;
	
	//Function to set audiosource ref
	static public void SetAudioSource(AudioSource audsource){
		source = audsource;
	}

	//Function to get song time
	static public float GetSongTime(){
		return source.time;
	}
}