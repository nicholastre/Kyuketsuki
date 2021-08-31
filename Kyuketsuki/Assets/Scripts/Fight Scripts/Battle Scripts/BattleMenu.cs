using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleMenuState
{
    BattleStartAlert,
    PlayerStartMenu,
    PlayerSkillsMenu,
    PlayerItemMenu,
    ItemUseMenu,
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
    public GameObject[] userButtons;
    public ItemButton[] itemButtons;
    public Text itemName;
    public Text itemDescription;
    public Text currentActorName;
    public string startingDescription;

    private int gainedExp;
    private int gainedBits;
    private CombatUnitStats[] playerStats = new CombatUnitStats[3];
    private int[] buttonTargets = new int[3];
    private int currentPlayer;
    private int activeSkill = -1;
    private SkillBase[] playerSkills = new SkillBase[3];
    private int activeItem = -1;

    // Start is called before the first frame update
    void Start()
    {
        SetTurnDescription(startingDescription);
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
            case BattleMenuState.PlayerItemMenu:
                ChoosingItem();
                break;
            case BattleMenuState.ItemUseMenu:
                UsingItem();
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
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(true);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
    }

    private void StartingPlayerTurn()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(true);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
    }

    private void ChoosingPlayerSkill()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(true);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);

        if (activeSkill != -1)
        {
            gameObject.transform.Find("ChooseSkillScreen").Find("ChosenAttack").
                Find("AttackName").gameObject.GetComponent<Text>().text = playerSkills[activeSkill].skillName;
            gameObject.transform.Find("ChooseSkillScreen").Find("ChosenAttack").
                 Find("AttackDesc").gameObject.GetComponent<Text>().text = "Dano Base " + 
                 playerSkills[activeSkill].baseDamage + "\n" + "Poder Usado " + playerSkills[activeSkill].powerCost;
            gameObject.transform.Find("ChooseSkillScreen").Find("ChosenAttack").gameObject.SetActive(true);

            if (playerSkills[activeSkill].powerCost <= playerStats[currentPlayer].currentPowerPoints)
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
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
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
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
    }

    private void ChoosingItem()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(true);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
        ShowItems();

        if (activeItem != -1)
        {
            gameObject.transform.Find("ChooseItemScreen").Find("ChosenItem").gameObject.SetActive(true);
        } else
        {
            gameObject.transform.Find("ChooseItemScreen").Find("ChosenItem").gameObject.SetActive(false);
        }
    }

    private void UsingItem()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(true);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);

        if (activeItem != -1)
        {
            gameObject.transform.Find("ConfirmItemScreen").
                Find("ChosenItem").Find("ItemName").GetComponent<Text>().text = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[activeItem]).itemName;
            gameObject.transform.Find("ConfirmItemScreen").
                Find("ChosenItem").Find("ItemDesc").GetComponent<Text>().text = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[activeItem]).description;
        }
        else
        {
            gameObject.transform.Find("ConfirmItemScreen").Find("ChosenItem").gameObject.SetActive(false);
        }
    }

    private void ConfirmingPlayerEscape()
    {
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(true);
    }

    private void ShowEscapeMenu()
    {
        SetTurnDescription("O grupo encontra uma oportunidade e foge do combate!");
        gameObject.transform.Find("StartPlayerScreen").gameObject.SetActive(false);
        gameObject.transform.Find("CurrentTurnDescription").gameObject.SetActive(true);
        gameObject.transform.Find("ChooseSkillScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmAttackScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ChooseItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmItemScreen").gameObject.SetActive(false);
        gameObject.transform.Find("ConfirmEscapeScreen").gameObject.SetActive(false);
    }

    private void ShowVictoryMenu()
    {
        if (SceneManager.GetActiveScene().name == "finalBattle")
        {
            SetTurnDescription("Com um último grito aterrador, o Agiota se desfaz " +
                "no ar e desaparece");
        }
        else
        {
            SetTurnDescription("Monstros eliminados! O grupo ganha " + gainedExp.ToString() +
            " pontos de experiência e " + gainedBits.ToString() + " Bits.");
        }
    }

    private void ShowDefeatMenu()
    {
        if (SceneManager.GetActiveScene().name == "finalBattle")
        {
            SetTurnDescription("E o Agiota derrota o grupo...");
        } else
        {
            SetTurnDescription("O grupo foi derrotado...");
        }
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

    public void ClickInventoryButton()
    {
        SetBattleMenuState(BattleMenuState.PlayerItemMenu);
    }

    public void SelectItem(int buttonValue)
    {
        activeItem = buttonValue;
        itemName.text = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[activeItem]).itemName;
        itemDescription.text = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[activeItem]).description;
    }

    public void ClickConfirmItemButton()
    {
        SetUserButtons(playerStats);
        SetBattleMenuState(BattleMenuState.ItemUseMenu);
    }

    public void ClickEscapeButton()
    {
        SetBattleMenuState(BattleMenuState.ConfirmEscape);
    }

    public void ConfirmEscapeButton()
    {
        SetBattleMenuState(BattleMenuState.EscapeMenu);
        BattleManager.instance.SetEscapeBattle();
    }

    public void ClickCancelButton()
    {
        if (currentMenuState == BattleMenuState.PlayerSkillsMenu)
        {
            activeSkill = -1;
            SetBattleMenuState(BattleMenuState.PlayerStartMenu);
        } else if (currentMenuState == BattleMenuState.PlayerAttackMenu)
        {
            activeSkill = -1;
            SetBattleMenuState(BattleMenuState.PlayerSkillsMenu);
        } else if (currentMenuState == BattleMenuState.PlayerItemMenu)
        {
            activeItem = -1;
            SetBattleMenuState(BattleMenuState.PlayerStartMenu);
        } else if (currentMenuState == BattleMenuState.ItemUseMenu) 
        {
            activeItem = -1;
            SetBattleMenuState(BattleMenuState.PlayerItemMenu);
        } else if (currentMenuState == BattleMenuState.ConfirmEscape)
        {
            SetBattleMenuState(BattleMenuState.PlayerStartMenu);
        }
    }

    public void SetBattleMenuState(BattleMenuState newState)
    {
        currentMenuState = newState;
    }

    private void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].GetComponent<Image>().enabled = true;
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.gameObject.SetActive(true);
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].GetComponent<Image>().enabled = false;
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SetUserButtons(CombatUnitStats[] playerUnits)
    {
        for (int j = 0; j < userButtons.Length; j++)
        {
            userButtons[j].SetActive(false);
        }

        int activeButtonCounter = 0;
        for (int i = 0; i < playerUnits.Length; i++)
        {
            GameObject buttonText = userButtons[activeButtonCounter].transform.Find("Text").gameObject;

            string displayText = "";
            if (GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[activeItem]).affectHP)
            {
                displayText = playerUnits[i].unitName + " - vida: " + playerUnits[i].currentHitPoints +
                " / " + playerUnits[i].maxHitPoints;
            } else if (GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[activeItem]).affectMP)
            {
                displayText = playerUnits[i].unitName + " - poder: " + playerUnits[i].currentPowerPoints +
                " / " + playerUnits[i].maxPowerPoints;
            }

            buttonText.GetComponent<Text>().text = displayText;
            buttonTargets[activeButtonCounter] = i;
            userButtons[activeButtonCounter].SetActive(true);

            activeButtonCounter += 1;
        }
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

    public void ClickItemUserButton(int buttonIdentifier)
    {
        BattleManager.instance.PlayerUseItem(buttonIdentifier, activeItem);
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

    public void PlaySFX(bool isCancel)
    {
        if (isCancel)
        {
            MusicController.instance.PlaySFX(GameSFX.CancelSound);
        }
        else
        {
            MusicController.instance.PlaySFX(GameSFX.ConfirmSound);
        }
    }
}
