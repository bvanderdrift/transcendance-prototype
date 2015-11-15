using UnityEngine;
using System.Collections;

public class ColumnPlacement : MonoBehaviour {
	public int columnNumber;
	public int totalSteps;
	public float radius;
	
	// Use this for initialization
	void Start () {
		float phase = (float)columnNumber / (float)totalSteps;
		transform.eulerAngles = new Vector3(0, phase * 360f, 0);
		transform.position = new Vector3(radius * Mathf.Sin(phase * 2 * Mathf.PI), 300, radius * Mathf.Cos(phase * 2 * Mathf.PI));
	}
}
