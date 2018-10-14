using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMovement : MonoBehaviour {

    public float speed = 3.0f;

	
	// Update is called once per frame
	void Update ()
    {
        //rotate selector
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
