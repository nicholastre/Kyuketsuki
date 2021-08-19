using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum AgiotaMenuState
{
    StartingState,
    LendingState,
    PayingState,
    ReturnState
}

public class agiotaMenu : MonoBehaviour
{
    public Component loanPrompt;
    public Component moneyInput;
    public Component firstButton;
    public Component secondButton;
    public Component thirdButton;

    private AgiotaMenuState menuState;

    // Start is called before the first frame update
    void Start()
    {
        menuState = AgiotaMenuState.StartingState;
    }

    // Update is called once per frame
    void Update()
    {
        switch (menuState)
        {
            case AgiotaMenuState.StartingState:
                loanPrompt.GetComponent<Text>().text = "o que vocês desejam, meus caros?";

                moneyInput.gameObject.SetActive(false);
                moneyInput.GetComponent<InputField>().interactable = false;

                firstButton.gameObject.SetActive(true);
                firstButton.GetComponent<Button>().interactable = true;

                secondButton.gameObject.SetActive(true);
                secondButton.GetComponent<Button>().interactable = true;
                secondButton.GetComponentInChildren<Text>().text = "Pagar";

                thirdButton.GetComponentInChildren<Text>().text = "Sair";

                break;
            case AgiotaMenuState.LendingState:
                loanPrompt.GetComponent<Text>().text = "perfeito...quanto vocês precisam?";

                moneyInput.gameObject.SetActive(true);
                moneyInput.GetComponent<InputField>().interactable = true;

                firstButton.gameObject.SetActive(false);
                firstButton.GetComponent<Button>().interactable = false;

                secondButton.gameObject.SetActive(true);
                secondButton.GetComponent<Button>().interactable = true;
                secondButton.GetComponentInChildren<Text>().text = "Confirmar";

                thirdButton.GetComponentInChildren<Text>().text = "Voltar";
                break;
            case AgiotaMenuState.PayingState:
                loanPrompt.GetComponent<Text>().text = "excelente...quanto vocês querem pagar?";

                moneyInput.gameObject.SetActive(true);
                moneyInput.GetComponent<InputField>().interactable = true;

                firstButton.gameObject.SetActive(false);
                firstButton.GetComponent<Button>().interactable = false;

                secondButton.gameObject.SetActive(true);
                secondButton.GetComponent<Button>().interactable = true;
                secondButton.GetComponentInChildren<Text>().text = "Confirmar";

                thirdButton.GetComponentInChildren<Text>().text = "Voltar";
                break;
            case AgiotaMenuState.ReturnState:
                loanPrompt.GetComponent<Text>().text = "muito bem. mais alguma coisa?";

                moneyInput.gameObject.SetActive(false);
                moneyInput.GetComponent<InputField>().interactable = false;

                firstButton.gameObject.SetActive(true);
                firstButton.GetComponent<Button>().interactable = true;

                secondButton.gameObject.SetActive(true);
                secondButton.GetComponent<Button>().interactable = true;
                secondButton.GetComponentInChildren<Text>().text = "Pagar";

                thirdButton.GetComponentInChildren<Text>().text = "Sair";
                break;
        }
    }

    public void clickFirstButton()
    {
        if (menuState == AgiotaMenuState.StartingState || menuState == AgiotaMenuState.ReturnState)
        {
            resetButton(firstButton);
            menuState = AgiotaMenuState.LendingState;
        }
    }

    public void clickSecondButton()
    {

        resetButton(secondButton);
        switch (menuState)
        {
            case AgiotaMenuState.StartingState:
                menuState = AgiotaMenuState.PayingState;
                break;
            case AgiotaMenuState.LendingState:
                menuState = AgiotaMenuState.ReturnState;
                break;
            case AgiotaMenuState.PayingState:

                menuState = AgiotaMenuState.ReturnState;
                break;
            case AgiotaMenuState.ReturnState:
                menuState = AgiotaMenuState.PayingState;
                break;
        }
    }

    public void clickThirdButton()
    {
        resetButton(thirdButton);
        switch (menuState)
        {
            case AgiotaMenuState.StartingState:
                GetComponent<ChangeScenes>().PrepareFadeChange();
                break;
            case AgiotaMenuState.LendingState:
                menuState = AgiotaMenuState.StartingState;
                break;
            case AgiotaMenuState.PayingState:
                menuState = AgiotaMenuState.StartingState;
                break;
            case AgiotaMenuState.ReturnState:
                GetComponent<ChangeScenes>().PrepareFadeChange();
                break;
        }
    }

    private void resetButton (Component compButton)
    {
        Button button = compButton.GetComponent<Button>();
        button.enabled = !button.enabled;
        button.enabled = !button.enabled;
    }
}
