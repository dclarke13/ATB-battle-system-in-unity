using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelectButton : MonoBehaviour
{

    public GameObject enemyGO;
   

    public void SelectTarget()
    {
        //take info of target objects
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().input2(enemyGO);

    }

    public void toggleOn()
    {
            enemyGO.transform.Find("eSelector").gameObject.SetActive(true);
    }

    public void toggleOff()
    {
        enemyGO.transform.Find("eSelector").gameObject.SetActive(false);
    }




}