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

	// Use this for initialization
	void Start ()
    {
        battlestate = PerformAction.WAIT;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
        switch(battlestate)
        {
            case (PerformAction.WAIT):

                break;

            case (PerformAction.TAKEACTION):

                break;

            case (PerformAction.PERFORMACTION):

                break;
        }

	}

}
