using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum BattleState
{
    BattleStart,
    UnitTurn,
    TurnTransition,
    BattleEscape,
    BattleVictory,
    BattleDefeat
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public int averageExpGain;
    public int averageBitsGain;
    public GameObject battleMenu;
    public GameObject[] playerUnits;
    public GameObject[] enemyUnits;

    private GameObject[] combatOrder;
    private BattleState battleState;
    private float timeToWait;
    private int currentActor;
    private bool flowControl;
    private float enemyWaitTime = 1f;
    private float startBattleWait = 3f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        GetStatsFromManager();
        flowControl = true;

        combatOrder = new GameObject[playerUnits.Length + enemyUnits.Length];

        SetCombatOrder();

        battleState = BattleState.BattleStart;
        battleMenu.GetComponent<BattleMenu>().SetBattleMenuState(BattleMenuState.BattleStartAlert);
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleState)
        {
            case BattleState.BattleStart:
                StartingBattle();
                break;
            case BattleState.UnitTurn:
                RunningTurns();
                break;
            case BattleState.TurnTransition:
                MakingTurnTransition();
                break;
            case BattleState.BattleEscape:
                MakingBattleEnd();
                break;
            case BattleState.BattleVictory:
                battleMenu.GetComponent<BattleMenu>().SetBattleMenuState(BattleMenuState.VictoryMenu);
                MakingBattleEnd();
                break;
            case BattleState.BattleDefeat:
                battleMenu.GetComponent<BattleMenu>().SetBattleMenuState(BattleMenuState.DefeatMenu);
                MakingBattleDefeat();
                break;
        }
        
    }

    private void GetStatsFromManager()
    {
        for (int i = 0; i < playerUnits.Length; i++)
        {
            playerUnits[i].GetComponent<PlayerBase>().unitName = GameManager.instance.
                playerStats[i].statsInstance.charName;

            playerUnits[i].GetComponent<PlayerBase>().currentHitPoints = GameManager.instance.
                playerStats[i].statsInstance.currentHP;
            playerUnits[i].GetComponent<PlayerBase>().maxHitPoints = GameManager.instance.
                playerStats[i].statsInstance.maxHP;
            playerUnits[i].GetComponent<PlayerBase>().currentPowerPoints = GameManager.instance.
                playerStats[i].statsInstance.currentMP;
            playerUnits[i].GetComponent<PlayerBase>().maxPowerPoints = GameManager.instance.
                playerStats[i].statsInstance.maxMP;

            playerUnits[i].GetComponent<PlayerBase>().attack = GameManager.instance.
                playerStats[i].statsInstance.strength;
            playerUnits[i].GetComponent<PlayerBase>().defense = GameManager.instance.
                playerStats[i].statsInstance.defence;
            playerUnits[i].GetComponent<PlayerBase>().agility = GameManager.instance.
                playerStats[i].statsInstance.agility;

            playerUnits[i].GetComponent<PlayerBase>().UpdateUnitState();
        }
    }

    private void GiveStatsToManager()
    {
        for (int i = 0; i < playerUnits.Length; i++)
        {
            GameManager.instance.playerStats[i].statsInstance.charName = playerUnits[i].
                GetComponent<PlayerBase>().unitName;

            GameManager.instance.playerStats[i].statsInstance.currentHP = playerUnits[i].
                GetComponent<PlayerBase>().currentHitPoints;
            GameManager.instance.playerStats[i].statsInstance.maxHP = playerUnits[i].
                GetComponent<PlayerBase>().maxHitPoints;
            GameManager.instance.playerStats[i].statsInstance.currentMP = playerUnits[i].
                GetComponent<PlayerBase>().currentPowerPoints;
            GameManager.instance.playerStats[i].statsInstance.maxMP = playerUnits[i].
                GetComponent<PlayerBase>().maxPowerPoints;

            GameManager.instance.playerStats[i].statsInstance.strength = playerUnits[i].
                GetComponent<PlayerBase>().attack;
            GameManager.instance.playerStats[i].statsInstance.defence = playerUnits[i].
                GetComponent<PlayerBase>().defense;
            GameManager.instance.playerStats[i].statsInstance.agility = playerUnits[i].
                GetComponent<PlayerBase>().agility;
        }
    }

    private void SetCombatOrder()
    {
        for (int i = 0; i < playerUnits.Length; i++)
        {
            combatOrder[i] = playerUnits[i];
        }

        for (int j = playerUnits.Length; j < combatOrder.Length; j++)
        {
            combatOrder[j] = enemyUnits[j - playerUnits.Length];
        }

        GameObject placeholder;

        for (int k = 0; k < combatOrder.Length; k++)
        {
            for (int m = k + 1; m < combatOrder.Length; m++)
            {
                if (combatOrder[k].GetComponent<CombatUnitStats>().agility < combatOrder[m].GetComponent<CombatUnitStats>().agility)
                {
                    placeholder = combatOrder[k];
                    combatOrder[k] = combatOrder[m];
                    combatOrder[m] = placeholder;
                }
            }
        }
    }

    private void ProgressCombatOrder()
    {
        currentActor = (currentActor + 1) % combatOrder.Length;
    }

    private void StartingBattle()
    {
        currentActor = 0;
        if (startBattleWait > 0.0f)
        {
            startBattleWait -= Time.deltaTime;

            if (startBattleWait <= 0.0f)
            {
                SetBattleState(BattleState.UnitTurn);
            }
        }
    }

    private void RunningTurns()
    {
        if (combatOrder[currentActor].GetComponent<PlayerBase>() != null)
        {
            if (combatOrder[currentActor].GetComponent<PlayerBase>().CheckUnitState() == UnitState.Dead)
            {
                ProgressCombatOrder();
            } else
            {
                RunPlayerTurn();
            }
        } else if (combatOrder[currentActor].GetComponent<EnemyBase>() != null)
        {
            if (combatOrder[currentActor].GetComponent<EnemyBase>().CheckUnitState() == UnitState.Dead)
            {
                ProgressCombatOrder();
            }
            else
            {
                RunEnemyTurn();
            }
        }
    }

    private void MakingTurnTransition()
    {
        battleMenu.GetComponent<BattleMenu>().SetBattleMenuState(BattleMenuState.TurnResult);

        if (timeToWait > 0.0f)
        {
            timeToWait -= Time.deltaTime;

            if (timeToWait <= 0.0f)
            {
                ResolveUnitAction();
            }
        }
    }

    private void RunEnemyTurn()
    {
        if (timeToWait >= 0.0f)
        {
            string turnText = combatOrder[currentActor].GetComponent<CombatUnitStats>().unitName + " ataca!";
            battleMenu.GetComponent<BattleMenu>().SetTurnDescription(turnText);

            timeToWait -= Time.deltaTime;
        }

        if (flowControl && timeToWait < 0.0f)
        {
            flowControl = false;

            int randomPlayer = Random.Range(0, playerUnits.Length);
            while (playerUnits[randomPlayer].GetComponent<CombatUnitStats>().CheckUnitState() == UnitState.Dead)
            {
                randomPlayer = (randomPlayer + 1) % playerUnits.Length;
            }

            combatOrder[currentActor].GetComponent<EnemyBase>().ChooseSkill();
            int targetDefense = playerUnits[randomPlayer].GetComponent<CombatUnitStats>().defense;
            int targetAgility = playerUnits[randomPlayer].GetComponent<CombatUnitStats>().agility;

            combatOrder[currentActor].GetComponent<CombatUnitStats>().AttackTarget(randomPlayer, targetDefense, targetAgility);
            SetBattleState(BattleState.TurnTransition);
        }        
    }

    private void RunPlayerTurn()
    {
        if (flowControl)
        {
            battleMenu.GetComponent<BattleMenu>().SetBattleMenuState(BattleMenuState.PlayerStartMenu);
            flowControl = false;
        } else
        {
            battleMenu.GetComponent<BattleMenu>().SetTargetButtons(enemyUnits);
            battleMenu.GetComponent<BattleMenu>().SetPlayerStatsDisplay(playerUnits);
            battleMenu.GetComponent<BattleMenu>().SetPlayerName(combatOrder[currentActor].GetComponent<CombatUnitStats>().unitName);
        }
    }

    private void MakingBattleEnd()
    {
        if (timeToWait > 0.0f)
        {
            timeToWait -= Time.deltaTime;

            if (timeToWait <= 0.0f)
            {
                GiveStatsToManager();
                switch (GameManager.instance.currentArea) {
                    case AreaMaps.ForestArea:
                        BattleManager.instance.GetComponent<ChangeScenes>().areaToLoad = "forestMap";
                        break;
                    case AreaMaps.MineArea:
                        BattleManager.instance.GetComponent<ChangeScenes>().areaToLoad = "mineMap";
                        break;
                    case AreaMaps.MonasteryArea:
                        BattleManager.instance.GetComponent<ChangeScenes>().areaToLoad = "monasteryMap";
                        break;
                }
                BattleManager.instance.GetComponent<ChangeScenes>().PrepareFadeChange();
            }
        }
    }

    private void MakingBattleDefeat()
    {
        if (timeToWait > 0.0f)
        {
            timeToWait -= Time.deltaTime;

            if (timeToWait <= 0.0f)
            {
                GameManager.instance.groupWasDefeated = true;
                PlayerController.instance.areaTransitionName = "cityToLoan";
                BattleManager.instance.GetComponent<ChangeScenes>().areaToLoad = "loanScene";
                BattleManager.instance.GetComponent<ChangeScenes>().PrepareFadeChange();
            }
        }
    }

    public void PlayerAttackTarget(int targetIdentifier)
    {
        int targetDefense = enemyUnits[targetIdentifier].GetComponent<CombatUnitStats>().defense;
        int targetAgility = enemyUnits[targetIdentifier].GetComponent<CombatUnitStats>().agility;
        combatOrder[currentActor].GetComponent<CombatUnitStats>().AttackTarget(targetIdentifier, targetDefense, targetAgility);
    }

    public void PlayerUseItem(int targetIdentifier, int itemSlot)
    {
        Item usedItem = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[itemSlot]);
        GameManager.instance.RemoveItem(usedItem.itemName);

        playerUnits[targetIdentifier].transform.Find("DamageIndicator").
            gameObject.GetComponent<Text>().color = new Color(0.04f, 0.67f, 0.8f, 1);
        playerUnits[targetIdentifier].transform.Find("DamageIndicator").
            gameObject.GetComponent<Text>().text = usedItem.amountToChange.ToString();

        if (usedItem.affectHP)
        {
            playerUnits[targetIdentifier].GetComponent<CombatUnitStats>().ChangeHitPoints(usedItem.amountToChange);
        } else if (usedItem.affectMP)
        {
            playerUnits[targetIdentifier].GetComponent<CombatUnitStats>().ChangePowerPoints(usedItem.amountToChange);
        }

        string attackText = combatOrder[currentActor].GetComponent<CombatUnitStats>().unitName 
            + " usou " + usedItem.itemName + " em " 
            + playerUnits[targetIdentifier].GetComponent<CombatUnitStats>().unitName + "!";
        battleMenu.GetComponent<BattleMenu>().SetTurnDescription(attackText);
        playerUnits[targetIdentifier].transform.Find("DamageIndicator").gameObject.SetActive(true);
        SetBattleState(BattleState.TurnTransition);
    }

    private void ResetActionFeedback()
    {
        for (int i = 0; i < combatOrder.Length; i++)
        {
            combatOrder[i].transform.Find("DamageIndicator").gameObject.GetComponent<Text>().text = "";
            combatOrder[i].transform.Find("DamageIndicator").gameObject.SetActive(false);
        }

        battleMenu.GetComponent<BattleMenu>().ResetMenu();
    }

    private void ResolveUnitAction()
    {
        int deadEnemyCounter = 0;
        for (int i = 0; i < enemyUnits.Length; i++)
        {
            enemyUnits[i].GetComponent<CombatUnitStats>().UpdateUnitState();
            if (enemyUnits[i].GetComponent<CombatUnitStats>().CheckUnitState() == UnitState.Dead)
            {
                deadEnemyCounter += 1;
            }
        }

        int deadPlayerCounter = 0;
        for (int j = 0; j < playerUnits.Length; j++)
        {
            playerUnits[j].GetComponent<CombatUnitStats>().UpdateUnitState();
            if (playerUnits[j].GetComponent<CombatUnitStats>().CheckUnitState() == UnitState.Dead)
            {
                deadPlayerCounter += 1;
            }
        }

        if (deadPlayerCounter == playerUnits.Length)
        {
            ResetActionFeedback();
            timeToWait = 3.0f;
            SetBattleState(BattleState.BattleDefeat);
        }
        else if (deadEnemyCounter == enemyUnits.Length)
        {
            ResetActionFeedback();
            timeToWait = 3.0f;
            int expGain = Mathf.RoundToInt(Random.Range(averageExpGain * 0.85f, averageExpGain * 1.15f));
            int bitsGain = Mathf.RoundToInt(Random.Range(averageBitsGain * 0.85f, averageBitsGain * 1.15f));

            for (int i = 0; i < playerUnits.Length; i++)
            {
                if (playerUnits[i].GetComponent<CombatUnitStats>().currentState == UnitState.Dead)
                {
                    GameManager.instance.GiveExpToChar(i, Mathf.RoundToInt(expGain / 2));
                } else
                {
                    GameManager.instance.GiveExpToChar(i, expGain);
                }
            }
            GameManager.instance.changeMoney(bitsGain);
            
            battleMenu.GetComponent<BattleMenu>().SetBattleRewards(expGain, bitsGain);
            SetBattleState(BattleState.BattleVictory);
        } else
        {
            ProgressCombatOrder();

            if (combatOrder[currentActor].GetComponent<EnemyBase>() != null)
            {
                timeToWait = enemyWaitTime;
            }

            SetBattleState(BattleState.UnitTurn);
            ResetActionFeedback();
        }
    }

    private void SetBattleState(BattleState newState)
    {
        if (newState == BattleState.TurnTransition)
        {
            timeToWait = 1.5f;
        }

        battleState = newState;
        flowControl = true;
    }

    public void SetEscapeBattle()
    {
        timeToWait = 2.0f;
        SetBattleState(BattleState.BattleEscape);
    }

    public void GetAttackResult(string attackName, AttackResult result, int attackEffect, int targetIdentifier)
    {
        if (combatOrder[currentActor].GetComponent<PlayerBase>() != null)
        {
            GetAppropriateTarget(enemyUnits, result, attackEffect, targetIdentifier, attackName);
        } else if (combatOrder[currentActor].GetComponent<EnemyBase>() != null)
        {
            GetAppropriateTarget(playerUnits, result, attackEffect, targetIdentifier, attackName);
        }
    }

    private void GetAppropriateTarget(GameObject[] targetsArray, AttackResult result, int attackEffect, int targetIdentifier, string attackName)
    {
        if (result == AttackResult.Hit)
        {
            targetsArray[targetIdentifier].transform.Find("DamageIndicator").gameObject.GetComponent<Text>().color = new Color(0.93f, 0.88f, 0.88f, 1);
            targetsArray[targetIdentifier].transform.Find("DamageIndicator").gameObject.GetComponent<Text>().text = attackEffect.ToString();
            targetsArray[targetIdentifier].GetComponent<CombatUnitStats>().ChangeHitPoints(-1 * attackEffect);
        }
        else if (result == AttackResult.Critical)
        {
            targetsArray[targetIdentifier].transform.Find("DamageIndicator").gameObject.GetComponent<Text>().color = new Color(0.8f, 0.04f, 0.05f, 1);
            targetsArray[targetIdentifier].transform.Find("DamageIndicator").gameObject.GetComponent<Text>().text = attackEffect.ToString();
            targetsArray[targetIdentifier].GetComponent<CombatUnitStats>().ChangeHitPoints(-1 * attackEffect);
        }
        else if (result == AttackResult.Miss)
        {
            targetsArray[targetIdentifier].transform.Find("DamageIndicator").gameObject.GetComponent<Text>().color = new Color(0.93f, 0.88f, 0.88f, 1);
            targetsArray[targetIdentifier].transform.Find("DamageIndicator").gameObject.GetComponent<Text>().text = "errou";
        }
        string attackText = combatOrder[currentActor].GetComponent<CombatUnitStats>().unitName + " usou " + attackName + "!";
        battleMenu.GetComponent<BattleMenu>().SetTurnDescription(attackText);
        targetsArray[targetIdentifier].transform.Find("DamageIndicator").gameObject.SetActive(true);
        SetBattleState(BattleState.TurnTransition);
    }
}
