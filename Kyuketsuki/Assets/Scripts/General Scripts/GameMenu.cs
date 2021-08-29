using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState
{
    Disabled = 0,
    MainScreen = 1,
    ItemScreen = 2,
    OptionsScreen = 3,
    ExitScreen = 4
}

public class GameMenu : MonoBehaviour
{
    // Básico para as telas do menu principal
    public static GameMenu instance;
    public GameObject menuBackground;

    // Informações da tela principal do menu
    public GameObject[] charDisplays;
    public GameObject[] moneyTrackers;

    // Informações para inventário
    public GameObject itemInfoPanel;
    public Text itemName, itemDescription;
    public GameObject itemUseButton;
    public ItemButton[] itemButtons;
    public GameObject charPanel;
    public GameObject[] summarizedChars;
    public Text itemUseFeedback;
    private Item activeItem;

    private MenuState currentState;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        currentState = MenuState.Disabled;
        menuBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != MenuState.Disabled)
        {
            PlayerController.instance.canMove = false;
        }

        UpdateShownScreen();

        switch (currentState)
        {
            case MenuState.Disabled:
                if (ValidRightClick())
                {
                    currentState = MenuState.MainScreen;
                    menuBackground.SetActive(true);
                }
                break;
            case MenuState.MainScreen:
            case MenuState.ExitScreen:
                if (ValidRightClick())
                {
                    currentState = MenuState.Disabled;
                    menuBackground.SetActive(false);
                    PlayerController.instance.canMove = true;
                }
                break;
            case MenuState.ItemScreen:
            case MenuState.OptionsScreen:
                if (ValidRightClick())
                {
                    currentState = MenuState.MainScreen;
                }
                break;
        }
    }

    private void UpdateShownScreen()
    {
        switch (currentState)
        {
            case MenuState.MainScreen:
                menuBackground.transform.Find("Main").gameObject.SetActive(true);
                menuBackground.transform.Find("Inventory").gameObject.SetActive(false);
                menuBackground.transform.Find("Options").gameObject.SetActive(false);
                menuBackground.transform.Find("Exit").gameObject.SetActive(false);
                UpdateCharStats(charDisplays, false);
                break;
            case MenuState.ItemScreen:
                menuBackground.transform.Find("Main").gameObject.SetActive(false);
                menuBackground.transform.Find("Inventory").gameObject.SetActive(true);
                menuBackground.transform.Find("Options").gameObject.SetActive(false);
                menuBackground.transform.Find("Exit").gameObject.SetActive(false);
                UpdateCharStats(summarizedChars, true);
                ShowItems();
                break;
            case MenuState.OptionsScreen:
                menuBackground.transform.Find("Main").gameObject.SetActive(false);
                menuBackground.transform.Find("Inventory").gameObject.SetActive(false);
                menuBackground.transform.Find("Options").gameObject.SetActive(true);
                menuBackground.transform.Find("Exit").gameObject.SetActive(false);                
                break;
            case MenuState.ExitScreen:
                menuBackground.transform.Find("Main").gameObject.SetActive(false);
                menuBackground.transform.Find("Inventory").gameObject.SetActive(false);
                menuBackground.transform.Find("Options").gameObject.SetActive(false);
                menuBackground.transform.Find("Exit").gameObject.SetActive(true);
                break;
            case MenuState.Disabled:
                menuBackground.SetActive(false);
                break;
        }
    }

    private void UpdateCharStats(GameObject[] charInfo, bool isSummary)
    {
        CharStats[] storedStats = GameManager.instance.playerStats;

        moneyTrackers[0].transform.Find("Value").GetComponent<Text>().text = GameManager.instance.groupMoney.ToString() + " bits";
        moneyTrackers[1].transform.Find("Value").GetComponent<Text>().text = GameManager.instance.groupDebt.ToString() + " bits";

        for (int i = 0; i < charInfo.Length; i++)
        {

            charInfo[i].transform.Find("Char Name").GetComponent<Text>().text = storedStats[i].statsInstance.charName;
            charInfo[i].transform.Find("Char Hit Points").GetComponent<Text>().text = "Vida: " + 
                storedStats[i].statsInstance.currentHP + "/" + storedStats[i].statsInstance.maxHP;
            charInfo[i].transform.Find("Char Power Points").GetComponent<Text>().text = "Poder: " + 
                storedStats[i].statsInstance.currentMP + "/" + storedStats[i].statsInstance.maxMP;
            charInfo[i].transform.Find("Char Level").GetComponent<Text>().text = "Nível " + 
                storedStats[i].statsInstance.playerLevel;

            if (!isSummary)
            {
                if (storedStats[i].statsInstance.playerLevel == storedStats[i].statsInstance.maxLevel)
                {
                    charInfo[i].transform.Find("Char Exp").Find("Slider").
                        GetComponent<Slider>().maxValue = storedStats[i].statsInstance.expToNextLevel[19];
                    charInfo[i].transform.Find("Char Exp").Find("Slider").
                        GetComponent<Slider>().value = storedStats[i].statsInstance.currentEXP;
                    charInfo[i].transform.Find("Char Exp").Find("Exp To Level").
                        GetComponent<Text>().text = storedStats[i].statsInstance.currentEXP + "/" + 
                        storedStats[i].statsInstance.currentEXP;
                } else
                {
                    charInfo[i].transform.Find("Char Exp").Find("Slider").
                        GetComponent<Slider>().maxValue = storedStats[i].statsInstance.
                        expToNextLevel[storedStats[i].statsInstance.playerLevel];
                    charInfo[i].transform.Find("Char Exp").Find("Slider").
                        GetComponent<Slider>().value = storedStats[i].statsInstance.currentEXP;
                    charInfo[i].transform.Find("Char Exp").Find("Exp To Level").
                        GetComponent<Text>().text = storedStats[i].statsInstance.currentEXP + "/" + 
                        storedStats[i].statsInstance.expToNextLevel[storedStats[i].statsInstance.playerLevel];
                }
                
                charInfo[i].transform.Find("Char Str").GetComponent<Text>().text = "Força: " + storedStats[i].statsInstance.strength;
                charInfo[i].transform.Find("Char Def").GetComponent<Text>().text = "Defesa: " + storedStats[i].statsInstance.defence;
                charInfo[i].transform.Find("Char Agi").GetComponent<Text>().text = "Agilidade: " + storedStats[i].statsInstance.agility;
            }
        }
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

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
        itemInfoPanel.SetActive(true);
    }

    public void OpenItemCharChoice()
    {
        itemUseFeedback.gameObject.SetActive(false);
        itemUseButton.SetActive(false);
        itemUseButton.SetActive(true);
        charPanel.SetActive(true);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        charPanel.SetActive(false);
        itemInfoPanel.SetActive(false);
        itemUseFeedback.gameObject.SetActive(true);
        itemUseFeedback.GetComponent<Text>().text = GameManager.instance.playerStats[selectChar].
            statsInstance.charName + " usou " + activeItem.itemName + "!";
        activeItem = null;
    }

    public MenuState GetGameMenuState()
    {
        return currentState;
    }

    public void ClickChangeMenuState(int stateValue)
    {
        currentState = (MenuState)stateValue;

        itemInfoPanel.SetActive(false);
        charPanel.SetActive(false);
        itemUseFeedback.gameObject.SetActive(false);
    }

    private bool ValidRightClick()
    {
        if (Input.GetButtonDown("Fire2") && 
            PlayerController.instance != null &&
            PlayerController.instance.gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void EmergencyDisableMenu()
    {
        currentState = MenuState.Disabled;
    }
}
