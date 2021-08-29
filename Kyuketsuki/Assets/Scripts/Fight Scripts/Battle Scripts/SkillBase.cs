using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackResult
{
    Hit,
    Critical,
    Miss
}

public class SkillBase : MonoBehaviour
{
    public string skillName;
    public int baseDamage;
    public float critChance;
    public float baseHitChance;
    public int powerCost;

    private AttackResult usedSkillResult;
    private int usedSkillEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseSkill(int userAttack, int targetDefense, int targetAgility)
    {
        int baseEffect = baseDamage;
        int effectModifier = userAttack - targetDefense;
        int initialEffect = Mathf.Max(baseEffect + effectModifier, 1);

        float finalHitChance = Mathf.Max(baseHitChance - targetAgility, 5.0f);
        if (Random.Range(0.0f, 100.0f) > finalHitChance)
        {
            usedSkillResult = AttackResult.Miss;
        }
        else
        {
            usedSkillResult = AttackResult.Hit;
        }

        if (usedSkillResult == AttackResult.Hit && Random.Range(0.0f, 100.0f) < critChance)
        {
            initialEffect = 2 * initialEffect;
            usedSkillResult = AttackResult.Critical;
        }

        int finalEffect = initialEffect + Random.Range(initialEffect/20, initialEffect/10);
        finalEffect = Mathf.Max(1, finalEffect);

        usedSkillEffect = finalEffect;
    }

    public AttackResult GetSkillResult()
    {
        return usedSkillResult;
    }

    public int GetSkillEffect()
    {
        return usedSkillEffect;
    }
}
