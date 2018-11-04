using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gmanager : MonoBehaviour {

    public static Gmanager instance;

    public GameObject playerCharacter;
	
    //positions for spawning
    public Vector3 nextPlayerPos;
    //last position for returning from battle scene
    public Vector3 prevPlayerPos;

    //battle scene stuff
    public string sceneToLoad;
    public string lastScene;

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
        if (!GameObject.Find("playerCharacter"))
        {
            GameObject Player = Instantiate(playerCharacter,Vector3.zero,Quaternion.identity) as GameObject;
            Player.name = "playerCharacter";
        }
    }

    public void LoadNewScene()
    {

        SceneManager.LoadScene(sceneToLoad);

    }
	
}
