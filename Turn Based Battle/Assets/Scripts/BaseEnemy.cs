using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy
{

    public string enemyName = " ";
    
    //type advantage setup
    public enum Type
    {
        SCOUT,
        HEAVY,
        SUPPORT
    }

	//rarity setup
    public enum DropRate
    {
        COMMON,
        UNCOMMON,
        RARE
    }

    public Type enemyType;
    public DropRate droprate;

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
}
