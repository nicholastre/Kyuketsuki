using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState
{
    Disabled = 0,
    MainScreen = 1,
    OptionsScreen = 2,
    ExitScreen = 3
}

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public GameObject menuBackground;
    public GameObject[] charDisplays;
    public GameObject[] moneyTrackers;

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
                menuBackground.transform.Find("Options").gameObject.SetActive(false);
                menuBackground.transform.Find("Exit").gameObject.SetActive(false);
                UpdateCharStats();
                break;
            case MenuState.OptionsScreen:
                menuBackground.transform.Find("Main").gameObject.SetActive(false);
                menuBackground.transform.Find("Options").gameObject.SetActive(true);
                menuBackground.transform.Find("Exit").gameObject.SetActive(false);
                break;
            case MenuState.ExitScreen:
                menuBackground.transform.Find("Main").gameObject.SetActive(false);
                menuBackground.transform.Find("Options").gameObject.SetActive(false);
                menuBackground.transform.Find("Exit").gameObject.SetActive(true);
                break;
            case MenuState.Disabled:
                menuBackground.SetActive(false);
                break;
        }
    }

    private void UpdateCharStats()
    {
        CharStats[] storedStats = GameManager.instance.playerStats;

        moneyTrackers[0].transform.Find("Value").GetComponent<Text>().text = GameManager.instance.groupMoney.ToString() + " bits";
        moneyTrackers[1].transform.Find("Value").GetComponent<Text>().text = GameManager.instance.groupDebt.ToString() + " bits";

        for (int i = 0; i < charDisplays.Length; i++)
        {
            charDisplays[i].transform.Find("Char Name").GetComponent<Text>().text = storedStats[i].charName;
            charDisplays[i].transform.Find("Char Hit Points").GetComponent<Text>().text = "Vida: " + storedStats[i].currentHP + "/" + storedStats[i].maxHP;
            charDisplays[i].transform.Find("Char Power Points").GetComponent<Text>().text = "Poder: " + storedStats[i].currentMP + "/" + storedStats[i].maxMP;
            charDisplays[i].transform.Find("Char Level").GetComponent<Text>().text = "Nível " + storedStats[i].playerLevel;
            charDisplays[i].transform.Find("Char Exp").Find("Slider").GetComponent<Slider>().maxValue = storedStats[i].expToNextLevel[storedStats[i].playerLevel];
            charDisplays[i].transform.Find("Char Exp").Find("Slider").GetComponent<Slider>().value = storedStats[i].currentEXP;
            charDisplays[i].transform.Find("Char Exp").Find("Exp To Level").GetComponent<Text>().text = storedStats[i].currentEXP + "/" + storedStats[i].expToNextLevel[storedStats[i].playerLevel];
            charDisplays[i].transform.Find("Char Str").GetComponent<Text>().text = "Força: " + storedStats[i].strength;
            charDisplays[i].transform.Find("Char Def").GetComponent<Text>().text = "Defesa: " + storedStats[i].defence;
            charDisplays[i].transform.Find("Char Agi").GetComponent<Text>().text = "Agilidade: " + storedStats[i].agility;
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
