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
    private Image progressBar;

    //find selector game object
    public GameObject selector;

    //ienumerator
    public GameObject targetEnemy;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 10f;
    //death
    private bool alive = true;

    //hero panel stuff
    private HeroPanelStuff stuff;
    public GameObject heroPanel;
    private Transform HeroPanelSpacer;

	// Use this for initialization
	void Start ()
    {

        curCooldown = Random.Range(0, 2.5f);
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        selector.SetActive(false);
        startPosition = transform.position;
        //find spacer
        HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.Find("HeroPanel").transform.Find("HeroPanelSpacer");
        //create panel & fill info
        createHeroPanel();
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
                StartCoroutine(actionTimer());
                break;

            case (TurnState.DEAD):
                if(!alive)
                {
                    return;
                }
                else
                {
                    //change tag
                    this.gameObject.tag = "DeadPlayer";
                    //make not attackable
                    BSM.PlayerCharacters.Remove(this.gameObject);
                    //stop management of character
                    BSM.PlayerManagement.Remove(this.gameObject);
                    //deactive selector if on
                    selector.SetActive(false);
                    //reset gui
                    BSM.attackPanel.SetActive(false);
                    BSM.targetPanel.SetActive(false);
                    //remove item from list
                    if (BSM.PlayerCharacters.Count > 0)
                    {
                        for (int i = 0; i < BSM.TurnList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (BSM.TurnList[i].attackerGO == this.gameObject)
                                {
                                    BSM.TurnList.Remove(BSM.TurnList[i]);
                                }
                                if (BSM.TurnList[i].attackTarget == this.gameObject)
                                {
                                    BSM.TurnList[i].attackTarget = BSM.PlayerCharacters[Random.Range(0, BSM.PlayerCharacters.Count)];
                                }
                            }
                        }
                    }
                    // change colour/ play death animation
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(155,155,155,255);
                    //reset player input
                    BSM.battlestate = BattleStateMachine.PerformAction.CHECKALIVE;

                    alive = false;
                }
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

    private IEnumerator actionTimer()
    {

        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        //animate player
        Vector3 targetPos = new Vector3(targetEnemy.transform.position.x + 1.5f, targetEnemy.transform.position.y, targetEnemy.transform.position.z);
        while (MoveToEnemy(targetPos))
        {
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(0.5f);
        //damage
        doDamage();
        //animate back to start position
        Vector3 originPOS = startPosition;
        while (MoveToOrigin(originPOS))
        {
            yield return null;
        }
        //remove from bsm list
        BSM.TurnList.RemoveAt(0);

        //reset bsm to wait
        if (BSM.battlestate != BattleStateMachine.PerformAction.WIN && BSM.battlestate != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battlestate = BattleStateMachine.PerformAction.WAIT;
            //end coroutine


            //reset enemy state
            curCooldown = 0f;
            currentState = TurnState.PROCESSING;
        }
        else
        {
            currentState = TurnState.WAITING;
        }
        actionStarted = false;
    }
       

    private bool MoveToEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveToOrigin(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void takeDamage(float damageAmount)
    {
        //reduce hp by damage amount
        hero.currentHP -= damageAmount;
        //check if dead
        if (hero.currentHP <=0)
        {
            hero.currentHP = 0;
            currentState = TurnState.DEAD;
        }

        updateHeroPanel();
    }

    //do damage
    void doDamage()
    {
        float damageDone = hero.curATK + BSM.TurnList[0].chosenAttack.attackDmg;
        targetEnemy.GetComponent<EnemyStateMachine>().takeDamage(damageDone);
    }

    void createHeroPanel()
    {
        heroPanel = Instantiate(heroPanel) as GameObject;
        stuff = heroPanel.GetComponent<HeroPanelStuff>();

        stuff.heroName.text = hero.theName;
        stuff.heroHP.text = "HP: " + hero.currentHP + "/" + hero.baseHP;
        stuff.heroAP.text = "AP: " + hero.currentAP + "/" + hero.baseAP;
        progressBar = stuff.progressbar;

        heroPanel.transform.SetParent(HeroPanelSpacer, false);
    }

    //update UI
    void updateHeroPanel()
    {
        stuff.heroHP.text = "HP: " + hero.currentHP + "/" + hero.baseHP;
        stuff.heroAP.text = "AP: " + hero.currentAP + "/" + hero.baseAP;
    }

}
