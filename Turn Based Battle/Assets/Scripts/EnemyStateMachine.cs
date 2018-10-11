﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public BaseEnemy enemy;
    private BattleStateMachine BSM;

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
    private float maxCooldown = 10.0f;
    // start position for animations
    private Vector3 startPosition;
    //actiontimer setup
    private bool actionStarted = false;
    //GO for target used for animation
    public GameObject targetPlayer;
    private float animSpeed = 10f;
    // Use this for initialization
    void Start()
    {

        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
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
                StartCoroutine(actionTimer());
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
        thisAttack.type = "Enemy";
        thisAttack.attackerGO = this.gameObject;
        thisAttack.attackTarget = BSM.PlayerCharacters[Random.Range(0, BSM.PlayerCharacters.Count)];
        BSM.CollectActions(thisAttack);
    }

    private IEnumerator actionTimer()
    {

        if(actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        //animate enemy 
        Vector3 targetPos = new Vector3(targetPlayer.transform.position.x -1.5f, targetPlayer.transform.position.y, targetPlayer.transform.position.z);
        while(MoveToEnemy(targetPos))
        {
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(0.5f);
        //damage

        //animate back to start position
        Vector3 originPOS = startPosition;
        while (MoveToOrigin(originPOS))
        {
            yield return null;
        }
        //remove from bsm list
        BSM.TurnList.RemoveAt(0);

        //reset bsm to wait
        BSM.battlestate = BattleStateMachine.PerformAction.WAIT;
        //end coroutine
        actionStarted = false;

        //reset enemy state
        curCooldown = 0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveToEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position,target,animSpeed *Time.deltaTime));
    }

    private bool MoveToOrigin(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
}
