using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass {


    //charactername
    public string theName = " ";

    //hitpoint setup
    public float baseHP;
    public float currentHP;
    //action point setup
    public float baseAP;
    public float currentAP;

    public float baseATK;
    public float curATK;

    public float baseDef;
    public float curDef;

    //attacks that can be done
    public List<BaseAttack> attacks = new List<BaseAttack>();
}
