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
        loanPrompt.GetComponent<Text>().text = "o que vocês desejam, meus caros?";
    }

    // Update is called once per frame
    void Update()
    {
        switch (menuState)
        {
            case AgiotaMenuState.StartingState:
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
            loanPrompt.GetComponent<Text>().text = "perfeito...quanto vocês precisam?";
        }
    }

    public void clickSecondButton()
    {

        resetButton(secondButton);
        switch (menuState)
        {
            case AgiotaMenuState.StartingState:
                menuState = AgiotaMenuState.PayingState;
                loanPrompt.GetComponent<Text>().text = "excelente...quanto vocês querem pagar?";
                break;
            case AgiotaMenuState.LendingState:
                int lendMoney = int.Parse(moneyInput.GetComponent<InputField>().text);

                if (lendMoney <= 0)
                {
                    loanPrompt.GetComponent<Text>().text = "vocês precisam definir um valor, meus caros";
                } else
                {
                    GameManager.instance.changeMoney(lendMoney);
                    GameManager.instance.changeDebt(lendMoney);

                    menuState = AgiotaMenuState.ReturnState;
                    loanPrompt.GetComponent<Text>().text = "muito bem. mais alguma coisa?";
                }
                break;
            case AgiotaMenuState.PayingState:
                int payMoney = int.Parse(moneyInput.GetComponent<InputField>().text);

                if (payMoney <= 0)
                {
                    loanPrompt.GetComponent<Text>().text = "vocês precisam definir um valor, meus caros";
                } else if (payMoney > GameManager.instance.groupMoney) {
                    loanPrompt.GetComponent<Text>().text = "meus caros, vocês não possuem esse dinheiro...";
                } else
                {
                    GameManager.instance.changeMoney(payMoney * -1);
                    GameManager.instance.changeDebt(payMoney * -1);

                    menuState = AgiotaMenuState.ReturnState;
                    loanPrompt.GetComponent<Text>().text = "muito bem. mais alguma coisa?";
                }
                break;
            case AgiotaMenuState.ReturnState:
                menuState = AgiotaMenuState.PayingState;
                loanPrompt.GetComponent<Text>().text = "excelente...quanto vocês querem pagar?";
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
                loanPrompt.GetComponent<Text>().text = "o que vocês desejam, meus caros?";
                break;
            case AgiotaMenuState.PayingState:
                menuState = AgiotaMenuState.StartingState;
                loanPrompt.GetComponent<Text>().text = "o que vocês desejam, meus caros?";
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
