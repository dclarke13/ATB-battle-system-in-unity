using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start ()
    {
        battlestate = PerformAction.WAIT;
        //add players to list at start of battle
        PlayerCharacters.AddRange(GameObject.FindGameObjectsWithTag("Player"));
       //add enemies to list at start of battle
        EnemyCharacters.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
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

}
