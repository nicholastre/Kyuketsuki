using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string characterName;
    public CharStatsBase statsInstance = new CharStatsBase();

    // Use this for initialization
    void Start()
    {
        statsInstance.StartupStats(characterName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintStats()
    {
        statsInstance.PrintStats();
    }

    public void AddExp(int expToAdd)
    {
        statsInstance.AddExp(expToAdd);
    }

    public void changeHitPoints(int modifier)
    {
        statsInstance.changeHitPoints(modifier);
    }

    public void changeMagicPoints(int modifier)
    {
        statsInstance.changeHitPoints(modifier);
    }

    public void restoreCharacter()
    {
        statsInstance.restoreCharacter();
    }

    public bool CheckLevelUp()
    {
        return statsInstance.CheckLevelUp();
    }

    public void ResetCharacter()
    {
        statsInstance.ResetCharacter();
    }
}
