using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gmanager : MonoBehaviour {

    public static Gmanager instance;

    //class for random monsters
    [System.Serializable]
    public class RegionData
    {
        public string regionname;
        public int maxEnemies = 4;
        public List<GameObject> possibleEnemies = new List<GameObject>();
    }

    public List<RegionData> Regions = new List<RegionData>();

    public GameObject playerCharacter;
	
    //positions for spawning
    public Vector3 nextPlayerPos;
    //last position for returning from battle scene
    public Vector3 prevPlayerPos;

    //battle scene stuff
    public string sceneToLoad;
    public string lastScene;

    //booleans for checking if player is in danger zone
    public bool isWalking = false;
    public bool canEncounterEnemy = false;
    public bool gotEncountered = false;

    //state machine enum
    public enum gameStates
    {
        WORLD_STATE,
        TOWN_STATE,
        BATTLE_STATE,
        IDLE
    }
    public gameStates gamestate;

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

    void Update()
    {
        switch(gamestate)
        {
            case (gameStates.WORLD_STATE):
                if(isWalking)
                {
                    RandomEncounter();
                }
                if(gotEncountered)
                {
                    gamestate = gameStates.BATTLE_STATE;
                }

                break;
            case (gameStates.TOWN_STATE):

                break;
            case (gameStates.BATTLE_STATE):
                //load battle scene
                
                //go to idle
                break;
            case (gameStates.IDLE):

                break;
        }
    }

    public void LoadNewScene()
    {

        SceneManager.LoadScene(sceneToLoad);

    }
	
    void RandomEncounter()
    {
        if(isWalking && canEncounterEnemy)
        {
            
            if(Random.Range(0,1000) < 10)
            {
                Debug.Log("Enocunter Occured");
                gotEncountered = true;
            }

        }
    }

    void startBattle()
    {



    }
}
