using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestingCharStatsBase
{

    [Test]
    public void TestInstanceNotNull()
    {
        CharStatsBase instance = new CharStatsBase();

        Assert.IsNotNull(instance);
    }

    [Test]
    public void TestStartupStats()
    {
        CharStatsBase instance = new CharStatsBase();
        string givenname = "sumino";
        instance.StartupStats(givenname);

        int expToCheck = 100;

        Assert.AreEqual(expToCheck, instance.baseEXP);
    }

    [Test]
    public void TestResetCharacter()
    {
        CharStatsBase instance = new CharStatsBase();
        string givenName = "sumino";
        instance.StartupStats(givenName);

        int starterLevel = 1;
        int levelToGive = 20;
        instance.playerLevel = levelToGive;
        instance.ResetCharacter();

        Assert.AreEqual(starterLevel, instance.playerLevel);
    }

    [Test]
    public void TestAddHitPointsLessThanMax()
    {
        CharStatsBase instance = new CharStatsBase();
        string givenName = "sumino";
        instance.StartupStats(givenName);

        int maxHP = 50;
        int currentHP = 40;
        int changeToHP = 5;

        instance.maxHP = maxHP;
        instance.currentHP = currentHP;

        instance.changeHitPoints(changeToHP);

        Assert.AreEqual(changeToHP + currentHP, instance.currentHP);
    }

    [Test]
    public void TestAddHitPointsOverMax()
    {
        CharStatsBase instance = new CharStatsBase();
        string givenName = "sumino";
        instance.StartupStats(givenName);

        int maxHP = 50;
        int currentHP = 40;
        int changeToHP = 25;

        instance.maxHP = maxHP;
        instance.currentHP = currentHP;

        instance.changeHitPoints(changeToHP);

        Assert.AreEqual(maxHP, instance.currentHP);
    }

    [Test]
    public void TestRestoreCharacter()
    {
        CharStatsBase instance = new CharStatsBase();
        string givenName = "sumino";
        instance.StartupStats(givenName);

        int maxHP = 50;
        int currentHP = 40;

        instance.maxHP = maxHP;
        instance.currentHP = currentHP;

        instance.restoreCharacter();

        Assert.AreEqual(instance.currentHP, instance.maxHP);
    }

    [Test]
    public void TestAddExpUnderLevel()
    {
        CharStatsBase instance = new CharStatsBase();
        string givenName = "sumino";
        instance.StartupStats(givenName);

        int currentLevel = 1;
        int maxLevel = 20;
        int currentExp = 0;
        int addedExp = 200;

        instance.playerLevel = currentLevel;
        instance.maxLevel = maxLevel;
        instance.currentEXP = currentExp;

        instance.AddExp(addedExp);

        Assert.AreEqual(instance.currentEXP, currentExp + addedExp);
    }

    [Test]
    public void TestAddExpOverLevel()
    {
        CharStatsBase instance = new CharStatsBase();
        string givenName = "sumino";
        instance.StartupStats(givenName);

        int currentLevel = 20;
        int maxLevel = 20;
        int currentExp = 3000;
        int addedExp = 200;

        instance.playerLevel = currentLevel;
        instance.maxLevel = maxLevel;
        instance.currentEXP = currentExp;

        instance.AddExp(addedExp);

        Assert.AreEqual(instance.currentEXP, 0);
    }
}
