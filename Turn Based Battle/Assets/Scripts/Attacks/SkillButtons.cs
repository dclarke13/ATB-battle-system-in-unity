using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtons : MonoBehaviour
{
    public BaseAttack skillAttackToPerform;

    public void useSkillAttack()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().input4(skillAttackToPerform);
    }
	
}
