using UnityEngine;
using System.Collections;

public class PlatformController : BeatListener {
	float cRotationSpeed;

	public float rotationSpeed;

	public float rotationAcceleration;

	bool rotating;

	// Use this for initialization
	void Awake () {
		cRotationSpeed = 0;

		rotating = false;

		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		//Add rotation
		cRotationSpeed = Mathf.Lerp(cRotationSpeed, rotating ? rotationSpeed : 0, rotationAcceleration * Time.deltaTime);
		transform.Rotate(new Vector2(0, cRotationSpeed * Time.deltaTime));

		base.Update();
	}

	protected override void Handle64 (int b64)
	{
		if(b64 == 9 || b64 == 10) rotating = true;
		else rotating = false;
	}
}
