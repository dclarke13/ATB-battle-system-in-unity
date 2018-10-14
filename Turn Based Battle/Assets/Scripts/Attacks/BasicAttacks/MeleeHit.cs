using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : BaseAttack
{

    public MeleeHit()
    {
        attackName = "Tackle";
        attackDmg = 10f;
        APcost = 0f;
        attackDescription = "Launch a thundering tackle at the enemy";
    }



}
