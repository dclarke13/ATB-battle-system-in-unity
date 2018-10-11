using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour {

    public BaseHero hero;

    private BattleStateMachine BSM;
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
    public Image progressBar;

    //find selector game object
    public GameObject Selector;

	// Use this for initialization
	void Start ()
    {
        curCooldown = Random.Range(0, 2.5f);
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        Selector.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                updateProgressBar();
                break;

            case (TurnState.ADDTOLIST):
                BSM.PlayerManagement.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):
                //idle
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
        float calcualtion = curCooldown / maxCooldown;
        progressBar.transform.localScale = new Vector3(Mathf.Clamp(calcualtion,0,1),progressBar.transform.localScale.y,progressBar.transform.localScale.z);

        if (curCooldown >= maxCooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

}
