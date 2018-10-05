using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class BattleStateMachine : MonoBehaviour {

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public PerformAction battlestate;
    //list of turn order
    public List<HandleTurns> TurnList = new List<HandleTurns>();
    // list of player characters
    public List<GameObject> PlayerCharacters = new List<GameObject>();
    //list of enemies
    public List<GameObject> EnemyCharacters = new List<GameObject>();
    public GameObject targetButton;
    //transform for spacer
    public Transform Spacer;

    public enum PlayerGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }

    public PlayerGUI playerInput;

    public List<GameObject> PlayerManagement = new List<GameObject>();
    private HandleTurns playerChoice;

	// Use this for initialization
	void Start ()
    {
        battlestate = PerformAction.WAIT;
        //add players to list at start of battle
        PlayerCharacters.AddRange(GameObject.FindGameObjectsWithTag("Player"));
       //add enemies to list at start of battle
        EnemyCharacters.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        TargetButtons();
        Debug.Log("buttonscreated");
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        switch(battlestate)
        {
            case (PerformAction.WAIT):
                if(TurnList.Count>0)
                {
                    battlestate = PerformAction.TAKEACTION;
                }
                break;

            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(TurnList[0].attacker);
                //handles enemies
                if (TurnList[0].type =="Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.targetPlayer = TurnList[0].attackTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }
                //handles heroes
                if (TurnList[0].type == "Enemy")
                {

                }

                battlestate = PerformAction.PERFORMACTION;
                break;

            case (PerformAction.PERFORMACTION):

                break;
        }

	}

    public void CollectActions(HandleTurns turns)
    {

        TurnList.Add(turns);

    }

    void TargetButtons()
    {
        
        foreach(GameObject enemy in EnemyCharacters)
        {
            GameObject newButton = Instantiate(targetButton) as GameObject;
            TargetSelectButton button = newButton.GetComponent<TargetSelectButton>();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();

            TextMeshProUGUI buttonText = newButton.transform.Find("TMP Text").gameObject.GetComponent<TextMeshProUGUI>();

            buttonText.text = cur_enemy.enemy.enemyName;
            //Debug.Log(cur_enemy.enemy.enemyName);
            button.enemyGO = enemy;

            newButton.transform.SetParent(Spacer,false);
        }

    }

}
