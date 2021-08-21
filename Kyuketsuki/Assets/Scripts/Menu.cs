using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MenuState
{
    Disabled,
    Enabled,
    ConfirmExit
}

public class Menu : MonoBehaviour
{
    public static Menu instance;

    private MenuState currentState;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        currentState = MenuState.Disabled;
        gameObject.transform.Find("Menu Background").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case MenuState.Disabled:
                if (ValidMouseClick())
                {
                    currentState = MenuState.Enabled;
                    gameObject.transform.Find("Menu Background").gameObject.SetActive(true);
                    PlayerController.instance.canMove = false;
                }
                break;
            case MenuState.Enabled:
                if (ValidMouseClick())
                {
                    currentState = MenuState.Disabled;
                    gameObject.transform.Find("Menu Background").gameObject.SetActive(false);
                }

                PlayerController.instance.canMove = false;
                break;
            case MenuState.ConfirmExit:
                if (ValidMouseClick())
                {
                    currentState = MenuState.Disabled;
                    gameObject.transform.Find("Menu Background").gameObject.SetActive(false);
                    PlayerController.instance.canMove = true;
                }
                break;
        }
    }

    private bool ValidMouseClick()
    {
        if (PlayerController.instance != null &&
            PlayerController.instance.gameObject.GetComponent<SpriteRenderer>().enabled &&
            Input.GetButtonDown("Fire2"))
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
        gameObject.transform.Find("Menu Background").gameObject.SetActive(false);
    }
}
