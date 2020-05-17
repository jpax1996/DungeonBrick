using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform CameraStartPosition;
	// Use this for initialization
	void Start () {
        this.transform.position = CameraStartPosition.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
