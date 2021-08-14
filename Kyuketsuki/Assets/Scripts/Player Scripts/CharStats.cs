using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour {

    public string charName;
    public int playerLevel;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 100;

    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;

    public int strength;
    public int defence;
    public int agility;

    public int wpnPwr;
    public int armrPwr;

    public string equippedWpn;
    public string equippedArmr;

    public Sprite charIamge;

	// Use this for initialization
	void Start () {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        // Configura XP necessaria para avancar em cada nivel ate o nivel maximo
        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PrintStats()
    {
        Debug.Log("Nome: " + charName);
        Debug.Log("Nivel: " + playerLevel);
        Debug.Log("Experiencia: " + currentEXP);
        Debug.Log("HP Atual: " + currentHP);
        Debug.Log("HP Maximo: " + maxHP);
        Debug.Log("MP Atual: " + currentMP);
        Debug.Log("MP Maximo: " + maxMP);
        Debug.Log("Ataque: " + strength);
        Debug.Log("Defesa: " + defence);
        Debug.Log("Agilidade " + agility);
    }

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel)
        {

            if (currentEXP > expToNextLevel[playerLevel])
            {
                //retira a xp equivalente do lvl anterior
                currentEXP -= expToNextLevel[playerLevel];
                //level up
                playerLevel++;

                //a cada lvl up alterna adicionando força e defesa
                if (playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                maxMP = Mathf.FloorToInt(maxMP * 1.05f);
                currentMP = maxMP;
            }

        }

        if(playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
