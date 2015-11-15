using UnityEngine;
using System.Collections;

public class RingController : BeatListener {

	protected override void HandleHalf (int b64, int b32, int b16, int b8, int b4, int b2, int b1, int bh)
	{
		//Every half beat in the 10th & 11th 64 measure
		if((b64 == 9 || b64 == 10) && bh % 2 == 1) Fire ();
	}

	//Function to fire the ring
	void Fire(){
		particleSystem.Play();
	}
}
