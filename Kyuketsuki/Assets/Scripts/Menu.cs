using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    Disabled = 0,
    MainScreen = 1,
    ExitScreen = 2
}

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public GameObject menuBackground;

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
        UpdateShownScreen();

        switch (currentState)
        {
            case MenuState.Disabled:
                if (ValidRightClick())
                {
                    currentState = MenuState.MainScreen;
                    menuBackground.SetActive(true);
                    PlayerController.instance.canMove = false;
                }
                break;
            case MenuState.MainScreen:
                if (ValidRightClick())
                {
                    currentState = MenuState.Disabled;
                    menuBackground.SetActive(false);
                }

                PlayerController.instance.canMove = false;
                break;
            case MenuState.ExitScreen:
                if (ValidRightClick())
                {
                    currentState = MenuState.Disabled;
                    menuBackground.SetActive(false);
                    PlayerController.instance.canMove = true;
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
                menuBackground.transform.Find("Exit").gameObject.SetActive(false);
                break;
            case MenuState.ExitScreen:
                menuBackground.transform.Find("Main").gameObject.SetActive(false);
                menuBackground.transform.Find("Exit").gameObject.SetActive(true);
                break;
            case MenuState.Disabled:
                menuBackground.SetActive(false);
                break;
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
