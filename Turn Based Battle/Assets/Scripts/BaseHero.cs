using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseHero
{
    //charactername
    public string name = " ";

    //Class setup
    public enum Type
    {
        SCOUT,
        HEAVY,
        SUPPORT
    }

    //hitpoint setup
    public float baseHP;
    public float currentHP;

    //action point setup
    public float baseAP;
    public float currentAP;

    //set up level
    public int level;

    //set up xp
    public float startingXP;
    public float currentXP;

    //set up stats
    public int str;
    public int spd;
    public int intel;
    public int constitution;
	
}
