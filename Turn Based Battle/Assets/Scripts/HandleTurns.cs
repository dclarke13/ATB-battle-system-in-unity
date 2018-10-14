using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurns
{

    //name of attacker
    public string attacker;
    public string type;
    //gameobject of attacker
    public GameObject attackerGO;
    //gameobject of target
    public GameObject attackTarget;

    //which attack is performed
    public BaseAttack chosenAttack;

}
