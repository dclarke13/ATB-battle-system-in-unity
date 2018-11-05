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
        if(Gmanager.instance.nextspawnpoint != " ")
        {
            GameObject spawnpoint = GameObject.Find(Gmanager.instance.nextspawnpoint);
            transform.position = spawnpoint.transform.position;

            Gmanager.instance.nextspawnpoint = " ";
        }
        else if (Gmanager.instance.prevPlayerPos != Vector3.zero)
        {
            transform.position = Gmanager.instance.prevPlayerPos;
            Gmanager.instance.prevPlayerPos = Vector3.zero;
        }
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

        if(other.tag == "enterexit")
        {

            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            Gmanager.instance.nextspawnpoint = col.spawnpointName;
            Gmanager.instance.sceneToLoad = col.sceneToLoad;
            Gmanager.instance.LoadNewScene();
        }
      
        if (other.tag == "dangerzone")
        {
            RegionData region = other.gameObject.GetComponent<RegionData>();
            Gmanager.instance.curRegion = region;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "dangerzone")
        {
            Gmanager.instance.canEncounterEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "dangerzone")
        {
            Gmanager.instance.canEncounterEnemy = false;
        }
    }
}
