using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedCharacterStats
{
    public string charName;
    public int playerLevel;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 100;
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
}
