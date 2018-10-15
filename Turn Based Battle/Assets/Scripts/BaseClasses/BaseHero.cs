using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseHero: BaseClass
{

    //Class setup
    public enum Type
    {
        SCOUT,
        HEAVY,
        SUPPORT
    }

  

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

    public List<BaseAttack> skillList = new List<BaseAttack>();
	
}
