using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public BaseEnemy enemy;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for progress bar
    private float curCooldown = 0.0f;
    private float maxCooldown = 5.0f;

    // Use this for initialization
    void Start()
    {

        currentState = TurnState.PROCESSING;

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

            case (TurnState.ADDTOLIST):

                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.SELECTING):

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
            currentState = TurnState.ADDTOLIST;
        }
    }

}
