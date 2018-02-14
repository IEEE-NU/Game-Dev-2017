using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour {

	public float CameraSpeed;
	public float CameraRadius;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		float t = Time.realtimeSinceStartup / CameraSpeed;
		float x = Mathf.Cos (t) * CameraRadius;
		float y = Mathf.Sin (t) * CameraRadius;
		transform.position = new Vector3 (x, y, 0f);
	}
}
