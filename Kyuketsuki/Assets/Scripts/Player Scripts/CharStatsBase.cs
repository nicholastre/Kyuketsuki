using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStatsBase
{
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

    public void StartupStats(string name)
    {
        charName = name;
        playerLevel = 1;
        maxLevel = 20;
        currentEXP = 0;
        baseEXP = 100;

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        // Configura XP necessaria para avancar em cada nivel ate o nivel maximo
        for (int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.30f);
        }

        ResetCharacter();
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

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
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
                        strength += Random.Range(1, 4);
                        defence += Random.Range(0, 3);
                        agility += Random.Range(2, 5);

                        maxHP = Mathf.RoundToInt(maxHP * Random.Range(1.05f, 1.1f));
                        currentHP = maxHP;

                        maxMP = Mathf.RoundToInt(maxMP * Random.Range(1.1f, 1.11f));
                        currentMP = maxMP;
                        break;
                    case "maki":
                        strength += Random.Range(2, 5);
                        defence += Random.Range(1, 4);
                        agility += Random.Range(0, 3);

                        maxHP = Mathf.RoundToInt(maxHP * Random.Range(1.1f, 1.11f));
                        currentHP = maxHP;

                        maxMP = Mathf.RoundToInt(maxMP * Random.Range(1.11f, 1.125f));
                        currentMP = maxMP;
                        break;
                    case "hanzo":
                        strength += Random.Range(0, 3);
                        defence += Random.Range(2, 5);
                        agility += Random.Range(1, 4);

                        maxHP = Mathf.RoundToInt(maxHP * Random.Range(1.11f, 1.125f));
                        currentHP = maxHP;

                        maxMP = Mathf.RoundToInt(maxMP * Random.Range(1.1f, 1.11f));
                        currentMP = maxMP;
                        break;
                }

                if (playerLevel == maxLevel || currentEXP < expToNextLevel[playerLevel])
                {
                    return true;
                }
            }

            return false;

        }
        else
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
                maxHP = 80;
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
                maxHP = 90;
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
                maxHP = 100;
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
