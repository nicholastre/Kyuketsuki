using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CombatUnitStats
{
    public Animator unitAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StartCombatUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == UnitState.Dead)
        {
            unitAnimator.SetBool("isDead", true);
        } else
        {
            unitAnimator.SetBool("isDead", false);
        }
    }

    public void SetChosenSkill(int skillIdentifier)
    {
        chosenSkillIdentifier = skillIdentifier;
    }
}
