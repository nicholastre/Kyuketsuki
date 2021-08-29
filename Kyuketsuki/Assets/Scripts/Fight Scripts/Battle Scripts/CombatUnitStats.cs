using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnitState
{
    Normal,
    Dead
}

public class CombatUnitStats : MonoBehaviour
{
    public string unitName;

    public Text hitPointsText;
    public int currentHitPoints;
    public int maxHitPoints;

    public Text powerPointsText;
    public int currentPowerPoints;
    public int maxPowerPoints;

    public int attack;
    public int defense;
    public int agility;

    public SkillBase[] unitSkills;
    public int chosenSkillIdentifier;
    public UnitState currentState;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCombatUnit()
    {
        UpdateUnitState();
        hitPointsText.text = "HP: " + currentHitPoints + " / " + maxHitPoints;
        powerPointsText.text = "MP: " + currentPowerPoints + " / " + maxPowerPoints;
    }

    public void ChangeHitPoints(int modifier)
    {
        currentHitPoints += modifier;

        if (currentHitPoints <= 0)
        {
            currentHitPoints = 0;
        }

        hitPointsText.text = "HP: " + currentHitPoints + " / " + maxHitPoints;
    }

    public void UpdateUnitState()
    {
        if (currentHitPoints <= 0)
        {
            currentState = UnitState.Dead;
        } else
        {
            currentState = UnitState.Normal;
        }
    }

    public void ChangePowerPoints(int modifier)
    {
        currentPowerPoints += modifier;

        if (currentPowerPoints <= 0)
        {
            currentPowerPoints = 0;
        }

        powerPointsText.text = "MP: " + currentPowerPoints + " / " + maxPowerPoints;
    }

    public void AttackTarget(int targetIdentifier, int targetDefense, int targetAgility)
    {
        unitSkills[chosenSkillIdentifier].UseSkill(attack, targetDefense, targetAgility);
        ChangePowerPoints(-1 * unitSkills[chosenSkillIdentifier].powerCost);

        BattleManager.instance.GetAttackResult(unitSkills[chosenSkillIdentifier].skillName,
            unitSkills[chosenSkillIdentifier].GetSkillResult(), unitSkills[chosenSkillIdentifier].GetSkillEffect(),
            targetIdentifier);
    }

    public UnitState CheckUnitState()
    {
        return currentState;
    }
}
