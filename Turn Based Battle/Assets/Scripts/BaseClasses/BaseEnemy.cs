using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy: BaseClass
{
  

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

}
