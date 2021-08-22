using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour {

    public string charName;
    public int playerLevel;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel;
    public int baseEXP;

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
        playerLevel = 1;
        maxLevel = 20;
        currentEXP = 0;
        baseEXP = 100;

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        // Configura XP necessaria para avancar em cada nivel ate o nivel maximo
        for(int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.30f);
        }

        // Garante que os atributos carregados nao sejam sobrescritos pela inicializacao
        GameManager.instance.LoadGame();
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

        if(playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }

    public void changeHitPoints(int modifier)
    {
        currentHP += modifier;

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void changeMagicPoints(int modifier)
    {
        currentMP += modifier;

        currentMP = Mathf.Clamp(currentMP, 0, maxMP);
    }

    public void restoreCharacter()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    public bool CheckLevelUp()
    {
        if (playerLevel < maxLevel)
        {
            while (currentEXP >= expToNextLevel[playerLevel])
            {
                // Retira a xp equivalente do lvl anterior
                currentEXP -= expToNextLevel[playerLevel];

                // Level up
                playerLevel += 1;

                // Progressao de atributo de acordo com o personagem
                switch (charName)
                {
                    case "sumino":
                        strength += 2;
                        defence += 1;
                        agility += 3;

                        maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                        currentHP = maxHP;

                        maxMP = Mathf.FloorToInt(maxMP * 1.1f);
                        currentMP = maxMP;
                        break;
                    case "maki":
                        strength += 3;
                        defence += 2;
                        agility += 1;

                        maxHP = Mathf.FloorToInt(maxHP * 1.1f);
                        currentHP = maxHP;

                        maxMP = Mathf.FloorToInt(maxMP * 1.15f);
                        currentMP = maxMP;
                        break;
                    case "hanzo":
                        strength += 1;
                        defence += 3;
                        agility += 2;

                        maxHP = Mathf.FloorToInt(maxHP * 1.15f);
                        currentHP = maxHP;

                        maxMP = Mathf.FloorToInt(maxMP * 1.05f);
                        currentMP = maxMP;
                        break;
                }

                if (playerLevel == maxLevel || currentEXP < expToNextLevel[playerLevel])
                {
                    return true;
                }
            }

            return false;

        } else
        {
            return false;
        }
    }

    public void ResetCharacter()
    {
        switch (charName)
        {
            case "sumino":
                playerLevel = 1;
                currentEXP = 0;
                maxHP = 75;
                currentHP = maxHP;
                maxMP = 20;
                currentMP = maxMP;
                strength = 5;
                defence = 3;
                agility = 8;
                break;
            case "maki":
                playerLevel = 1;
                currentEXP = 0;
                maxHP = 100;
                currentHP = maxHP;
                maxMP = 25;
                currentMP = maxMP;
                strength = 8;
                defence = 5;
                agility = 3;
                break;
            case "hanzo":
                playerLevel = 1;
                currentEXP = 0;
                maxHP = 125;
                currentHP = maxHP;
                maxMP = 15;
                currentMP = maxMP;
                strength = 3;
                defence = 8;
                agility = 5;
                break;
        }
    }
}
