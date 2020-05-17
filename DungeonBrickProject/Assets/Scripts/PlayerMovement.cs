using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed = 0.5f;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            Debug.Log("Pressing Left Arrow");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            Debug.Log("Pressing Right Arrow");
        }
    }
}
