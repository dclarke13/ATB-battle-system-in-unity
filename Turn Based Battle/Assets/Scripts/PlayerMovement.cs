using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float moveSpeed = 10f;

    Vector3 curPos;
    Vector3 lastPos;




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
        curPos = transform.position;
        //check if player is moving
        if (curPos == lastPos)
        {
            Gmanager.instance.isWalking = false;
        }
        else
        {
            Gmanager.instance.isWalking = true;
        }
        lastPos = curPos;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "dangerzonefield"||other.tag == "dangerzoneforest")
        {
            Gmanager.instance.canEncounterEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "dangerzonefield" || other.tag == "dangerzoneforest")
        {
            Gmanager.instance.canEncounterEnemy = false;
        }
    }
}
