using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float moveSpeed = 10f;






	// Use this for initialization
	void Start ()
    {
        transform.position = Gmanager.instance.nextPlayerPos;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX,0.0f,moveZ);
        GetComponent<Rigidbody>().velocity = movement * moveSpeed;//* Time.deltaTime;

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnterTown")
        {
            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            Gmanager.instance.nextPlayerPos = col.spawnPoint.transform.position;
            Gmanager.instance.sceneToLoad = col.sceneToLoad;
            Gmanager.instance.LoadNewScene();
        }
        if(other.tag == "LeaveTown")
        {
            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            Gmanager.instance.nextPlayerPos = col.spawnPoint.transform.position;
            Gmanager.instance.sceneToLoad = col.sceneToLoad;
            Gmanager.instance.LoadNewScene();
        }
    }
}
