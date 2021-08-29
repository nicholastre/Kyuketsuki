﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleMenuState
{
    BattleStartAlert,
    PlayerStartMenu,
    PlayerSkillsMenu,
    PlayerAttackMenu,
    TurnResult,
    ConfirmEscape,
    EscapeMenu,
    VictoryMenu,
    DefeatMenu
}

public class BattleMenu : MonoBehaviour
{
    public BattleMenuState currentMenuState;
    public Text turnDescription;
    public GameObject[] skillButtons;
    public GameObject[] targetButtons;
    public Text currentActorName;
    public string startingDescription;

    private int gainedExp;
    private int gainedBits;
    private CombatUnitStats[] playerStats = new CombatUnitStats[3];
    private int[] buttonTargets = new int[3];
    private int currentPlayer;
    private int activeSkill = -1;
    private SkillBase[] playerSkills = new SkillBase[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentMenuState)
        {
            case BattleMenuState.BattleStartAlert:
                StartingBattle();
                break;
            case BattleMenuState.PlayerStartMenu:
                StartingPlayerTurn();
                break;
            case BattleMenuState.PlayerSkillsMenu:
                ChoosingPlayerSkill();
                break;
            case BattleMenuState.PlayerAttackMenu:
                ConfirmingPlayerAttack();
                break;
            case BattleMenuState.TurnResult:
                ShowTurnResult();
                break;
            case BattleMenuState.ConfirmEscape:
                ConfirmingPlayerEscape();
                break;
            case BattleMenuState.EscapeMenu:
                ShowEscapeMenu();
                break;
            case BattleMenuState.VictoryMenu:
                ShowVictoryMenu();
                break;
            case BattleMenuState.DefeatMenu:
                ShowDefeatMenu();
                break;
        }
    }

    private void StartingBattle()
    {
        SetTurnDescription("Monstros encontrados! " + startingDescription);
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(true);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
    }

    private void StartingPlayerTurn()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(true);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
    }

    private void ChoosingPlayerSkill()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(true);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);

        if (activeSkill != -1)
        {
            gameObject.transform.Find("ChooseSkillScreen").Find("ChosenAttack").
                Find("AttackName").gameObject.GetComponent<Text>().text = playerSkills[activeSkill].skillName;
            gameObject.transform.Find("ChooseSkillScreen").Find("ChosenAttack").
                 Find("AttackDesc").gameObject.GetComponent<Text>().text = "Dano Base " + 
                 playerSkills[activeSkill].baseDamage + "\n" + "Poder Usado " + playerSkills[activeSkill].powerCost;
            gameObject.transform.Find("ChooseSkillScreen").Find("ChosenAttack").gameObject.SetActive(true);

            if (playerSkills[activeSkill].powerCost < playerStats[currentPlayer].currentPowerPoints)
            {
                gameObject.transform.Find("ChooseSkillScreen").Find("ConfirmButton").
                    Find("Text").gameObject.GetComponent<Text>().text = "Confirmar ataque";
                gameObject.transform.Find("ChooseSkillScreen").Find("ConfirmButton").
                    GetComponent<Button>().interactable = true;
            } else
            {
                gameObject.transform.Find("ChooseSkillScreen").Find("ConfirmButton").
                    Find("Text").gameObject.GetComponent<Text>().text = "Sem Poder!";
                gameObject.transform.Find("ChooseSkillScreen").Find("ConfirmButton").
                    GetComponent<Button>().interactable = false;
            }
            gameObject.transform.Find("ChooseSkillScreen").Find("ConfirmButton").gameObject.SetActive(true);
        } else
        {
            gameObject.transform.Find("ChooseSkillScreen").Find("ChosenAttack").gameObject.SetActive(false);
            gameObject.transform.Find("ChooseSkillScreen").Find("ConfirmButton").gameObject.SetActive(false);
        }
    }

    private void ConfirmingPlayerAttack()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(true);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);

        gameObject.transform.Find("ConfirmAttackScreen").Find("ChosenAttack").Find("AttackName").
            GetComponent<Text>().text = playerSkills[activeSkill].skillName;
        string attackText = "Dano Base " +
                 playerSkills[activeSkill].baseDamage + "\n" + "Poder Usado " +
                 playerSkills[activeSkill].powerCost;
        gameObject.transform.Find("ConfirmAttackScreen").Find("ChosenAttack").Find("AttackDesc").
            GetComponent<Text>().text = attackText;
    }

    private void ShowTurnResult()
    {
        activeSkill = -1;
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(true);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
    }

    private void ConfirmingPlayerEscape()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(true);
    }

    private void ShowEscapeMenu()
    {
        SetTurnDescription("O grupo encontra uma oportunidade e foge do combate!");
    }

    private void ShowVictoryMenu()
    {
        SetTurnDescription("Monstros eliminados! O grupo ganha " + gainedBits.ToString() +
            " pontos de experiência e " + gainedBits.ToString() + " Bits.");
    }

    private void ShowDefeatMenu()
    {
        SetTurnDescription("O grupo foi derrotado...");
    }

    public void SetPlayerStatsDisplay(GameObject[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            playerStats[i] = players[i].GetComponent<CombatUnitStats>();
            int currentStat = i + 1;
            gameObject.transform.Find("StartPlayerScreen").Find("Player" + currentStat + "Stats").
                GetComponent<Text>().text = players[i].GetComponent<CombatUnitStats>().unitName + " - Vida: "
                + players[i].GetComponent<CombatUnitStats>().currentHitPoints + "/" 
                + players[i].GetComponent<CombatUnitStats>().maxHitPoints + " - Poder: " 
                + players[i].GetComponent<CombatUnitStats>().currentPowerPoints + "/" 
                + players[i].GetComponent<CombatUnitStats>().maxPowerPoints;
        }

        for (int j = 0; j < playerStats.Length; j++)
        {
            if (currentActorName.text == "Turno de " + playerStats[j].unitName)
            {
                currentPlayer = j;
            }
        }
    }

    public void ClickAttackButton()
    {
        SetBattleMenuState(BattleMenuState.PlayerSkillsMenu);
        SetSkillButtons();
    }

    public void ClickSkillButton(int skillIdentifier)
    {
        activeSkill = skillIdentifier;
    }

    public void ClickConfirmSkillButton()
    {
        SetBattleMenuState(BattleMenuState.PlayerAttackMenu);
    }

    public void ClickCancelAttackButton()
    {
        if (currentMenuState == BattleMenuState.PlayerSkillsMenu)
        {
            activeSkill = -1;
            SetBattleMenuState(BattleMenuState.PlayerStartMenu);
        } else if (currentMenuState == BattleMenuState.PlayerAttackMenu)
        {
            activeSkill = -1;
            SetBattleMenuState(BattleMenuState.PlayerSkillsMenu);
        }
    }

    public void SetBattleMenuState(BattleMenuState newState)
    {
        currentMenuState = newState;
    }

    public void SetPlayerName(string name)
    {
        currentActorName.text = "Turno de " + name;
    }

    private void SetSkillButtons()
    {
        playerSkills = playerStats[currentPlayer].unitSkills;
        
        for (int j = 0; j < playerSkills.Length; j++)
        {
            skillButtons[j].transform.Find("Text").gameObject.GetComponent<Text>().text = playerSkills[j].skillName;
        }
    }

    public void SetTargetButtons(GameObject[] enemyUnits)
    {
        for (int j = 0; j < targetButtons.Length; j++)
        {
            targetButtons[j].SetActive(false);
        }

        int activeButtonCounter = 0;
        for (int i = 0; i < enemyUnits.Length; i++)
        {
            if (enemyUnits[i].GetComponent<CombatUnitStats>().currentState != UnitState.Dead)
            {
                GameObject buttonText = targetButtons[activeButtonCounter].transform.Find("Text").gameObject;
                CombatUnitStats currentUnit = enemyUnits[i].GetComponent<CombatUnitStats>();
                string displayText = currentUnit.unitName + " - vida: " + currentUnit.currentHitPoints + 
                    " / " + currentUnit.maxHitPoints;
                buttonText.GetComponent<Text>().text = displayText;
                buttonTargets[activeButtonCounter] = i;
                targetButtons[activeButtonCounter].SetActive(true);

                activeButtonCounter += 1;
            }
        }
    }

    public void ClickTargetButton(int buttonIdentifier)
    {
        playerStats[currentPlayer].GetComponent<PlayerBase>().SetChosenSkill(activeSkill);
        BattleManager.instance.PlayerAttackTarget(buttonTargets[buttonIdentifier]);
    }

    public void SetTurnDescription(string description)
    {
        turnDescription.text = description;
        turnDescription.gameObject.SetActive(true);
    }

    public void ResetMenu()
    {
        turnDescription.gameObject.SetActive(false);
    }

    public void SetBattleRewards(int exp, int bits)
    {
        gainedExp = exp;
        gainedBits = bits;
    }
}