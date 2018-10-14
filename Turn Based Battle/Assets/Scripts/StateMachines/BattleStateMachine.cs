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

    //panels for players to select options
    public GameObject attackPanel;
    public GameObject targetPanel;

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
        playerInput = PlayerGUI.ACTIVATE;

        //set panels to inactive
        attackPanel.SetActive(false);
        targetPanel.SetActive(false);

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
                    for (int i = 0; i < PlayerCharacters.Count; i++)
                    {
                        if (TurnList[0].attackTarget == PlayerCharacters[i])
                        {
                            ESM.targetPlayer = TurnList[0].attackTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else
                        { 
                            TurnList[0].attackTarget = PlayerCharacters[Random.Range(0, PlayerCharacters.Count)];
                            ESM.targetPlayer = TurnList[0].attackTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                        }
                    }
                }
                //handles heroes
                if (TurnList[0].type == "Player")
                {
                    PlayerStateMachine PSM = performer.GetComponent<PlayerStateMachine>();
                    PSM.targetEnemy = TurnList[0].attackTarget;
                    PSM.currentState = PlayerStateMachine.TurnState.ACTION;
                }

                battlestate = PerformAction.PERFORMACTION;
                break;

            case (PerformAction.PERFORMACTION):

                break;
        }


        switch(playerInput)
        {
            case (PlayerGUI.ACTIVATE):
                if(PlayerManagement.Count>0)
                {
                    PlayerManagement[0].transform.Find("selector").gameObject.SetActive(true);
                    playerChoice = new HandleTurns();
                    attackPanel.SetActive(true);
                    playerInput = PlayerGUI.WAITING;
                }
                break;
            case (PlayerGUI.WAITING):

                break;
            case (PlayerGUI.DONE):
                playerInputDone();
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

            buttonText.text = cur_enemy.enemy.theName;
            //Debug.Log(cur_enemy.enemy.enemyName);
            button.enemyGO = enemy;

            newButton.transform.SetParent(Spacer,false);
        }

    }

    //attack buttons
    public void input1()
    {

        playerChoice.attacker = PlayerManagement[0].name;
        playerChoice.attackerGO = PlayerManagement[0];
        playerChoice.type = "Player";

        attackPanel.SetActive(false);
        targetPanel.SetActive(true);

    }
    //enemy selection
    public void input2(GameObject targetEnemy)
    {
        playerChoice.attackTarget = targetEnemy;
        playerInput = PlayerGUI.DONE;

    }

    void playerInputDone()
    {
        TurnList.Add(playerChoice);
        targetPanel.SetActive(false);
        PlayerManagement[0].transform.Find("selector").gameObject.SetActive(false);
        PlayerManagement.RemoveAt(0);
        playerInput = PlayerGUI.ACTIVATE;

    }

}
