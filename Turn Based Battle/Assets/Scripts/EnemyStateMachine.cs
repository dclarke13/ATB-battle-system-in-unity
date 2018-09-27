using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public BaseEnemy enemy;
    private BattleStateMachine bsm;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for progress bar
    private float curCooldown = 0.0f;
    private float maxCooldown = 5.0f;
    //
    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {

        currentState = TurnState.PROCESSING;
        bsm = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                updateProgressBar();
                break;

            case (TurnState.CHOOSEACTION):
                chooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.ACTION):

                break;

            case (TurnState.DEAD):

                break;

        }



    }

    void updateProgressBar()
    {

        curCooldown = curCooldown + Time.deltaTime;


        if (curCooldown >= maxCooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void chooseAction()
    {

        HandleTurns thisAttack = new HandleTurns();
        thisAttack.attacker = enemy.enemyName;
        thisAttack.attackerGO = this.gameObject;
        thisAttack.attackTarget = bsm.PlayerCharacters[Random.Range(0, bsm.PlayerCharacters.Count)];
        bsm.CollectActions(thisAttack);
    }

}
