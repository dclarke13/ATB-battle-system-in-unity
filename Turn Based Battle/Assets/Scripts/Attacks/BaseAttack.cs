using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack: MonoBehaviour
{

    public string attackName;
    //base attack damage + (1/2 level * str)
    public float attackDmg;
    public float APcost;
    public string attackDescription;


}
