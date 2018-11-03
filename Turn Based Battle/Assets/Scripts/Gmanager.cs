using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gmanager : MonoBehaviour {

    public static Gmanager instance;
	// Use this for initialization
	void Awake () {
		
        //check if the instance exists
        if(instance == null)
        {
            //if not set instance to this GameObject
            instance = this;
        }
        //if exists but is not this instance
        else if(instance != this)
        {
            //destroy it to prevent duplicate objects
            Destroy(gameObject);
        }
        //set current to not destroyable
        DontDestroyOnLoad(gameObject);
    }
	
}
