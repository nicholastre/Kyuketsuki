using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CombatUnitStats
{

    // Start is called before the first frame update
    void Start()
    {
        StartCombatUnit();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (currentState == UnitState.Dead)
        {
            Color currentColor = this.gameObject.GetComponent<SpriteRenderer>().color;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g,
                currentColor.b, Mathf.MoveTowards(currentColor.a, 0f, 2f * Time.deltaTime));
            if (this.gameObject.GetComponent<SpriteRenderer>().color.a == 0f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ChooseSkill()
    {
        int skill = Random.Range(0, unitSkills.Length);
        while (currentPowerPoints < unitSkills[skill].powerCost)
        {
            skill -= 1;
        }
        chosenSkillIdentifier = skill;
    }
}
