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

public class Menu : MonoBehaviour
{
    // Básico para as telas do menu principal
    public static Menu instance;
    public GameObject menuBackground;

    // Informações da tela principal do menu
    public GameObject[] charDisplays;
    public GameObject[] moneyTrackers;

    // Informações para inventário
    public GameObject[] summarizedChars;

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

            charInfo[i].transform.Find("Char Name").GetComponent<Text>().text = storedStats[i].charName;
            charInfo[i].transform.Find("Char Hit Points").GetComponent<Text>().text = "Vida: " + storedStats[i].currentHP + "/" + storedStats[i].maxHP;
            charInfo[i].transform.Find("Char Power Points").GetComponent<Text>().text = "Poder: " + storedStats[i].currentMP + "/" + storedStats[i].maxMP;
            charInfo[i].transform.Find("Char Level").GetComponent<Text>().text = "Nível " + storedStats[i].playerLevel;

            if (!isSummary)
            {
                charInfo[i].transform.Find("Char Exp").Find("Slider").GetComponent<Slider>().maxValue = storedStats[i].expToNextLevel[storedStats[i].playerLevel];
                charInfo[i].transform.Find("Char Exp").Find("Slider").GetComponent<Slider>().value = storedStats[i].currentEXP;
                charInfo[i].transform.Find("Char Exp").Find("Exp To Level").GetComponent<Text>().text = storedStats[i].currentEXP + "/" + storedStats[i].expToNextLevel[storedStats[i].playerLevel];
                charInfo[i].transform.Find("Char Str").GetComponent<Text>().text = "Força: " + storedStats[i].strength;
                charInfo[i].transform.Find("Char Def").GetComponent<Text>().text = "Defesa: " + storedStats[i].defence;
                charInfo[i].transform.Find("Char Agi").GetComponent<Text>().text = "Agilidade: " + storedStats[i].agility;
            }
        }
    }

    public void ClickChangeMenuState(int stateValue)
    {
        currentState = (MenuState)stateValue;
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
