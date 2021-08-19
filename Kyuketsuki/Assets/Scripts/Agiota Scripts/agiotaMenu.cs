using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum AgiotaMenuState
{
    StartingState,
    LendingState,
    PayingState,
    ReturnState,
    DebtLimitState,
    PaidDebtState,
    MaxLevelState,
    ChallengeState
}

enum EndingType
{
    DebtPaid,
    DebtOverLimit,
    NoEnding
}

public class agiotaMenu : MonoBehaviour
{
    public Component loanPrompt;
    public Component moneyInput;
    public Component firstButton;
    public Component secondButton;
    public Component thirdButton;
    public Component challengeButton;

    private AgiotaMenuState menuState;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.averageLevel == GameManager.instance.playerStats[0].maxLevel)
        {
            menuState = AgiotaMenuState.MaxLevelState;
            loanPrompt.GetComponent<Text>().text = "olá, meus caros...aconteceu alguma coisa?";
        } else
        {
            menuState = AgiotaMenuState.StartingState;
            loanPrompt.GetComponent<Text>().text = "o que vocês desejam, meus caros clientes?";
        }
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
            case AgiotaMenuState.DebtLimitState:
                moneyInput.gameObject.SetActive(false);
                moneyInput.GetComponent<InputField>().interactable = false;

                firstButton.gameObject.SetActive(true);
                firstButton.GetComponent<Button>().interactable = true;
                firstButton.GetComponentInChildren<Text>().text = "Continuar";

                secondButton.gameObject.SetActive(false);
                secondButton.GetComponent<Button>().interactable = false;

                thirdButton.gameObject.SetActive(false);
                thirdButton.GetComponent<Button>().interactable = false;
                break;
            case AgiotaMenuState.PaidDebtState:
                moneyInput.gameObject.SetActive(false);
                moneyInput.GetComponent<InputField>().interactable = false;

                firstButton.gameObject.SetActive(true);
                firstButton.GetComponent<Button>().interactable = true;
                firstButton.GetComponentInChildren<Text>().text = "Continuar";

                secondButton.gameObject.SetActive(false);
                secondButton.GetComponent<Button>().interactable = false;

                thirdButton.gameObject.SetActive(false);
                thirdButton.GetComponent<Button>().interactable = false;
                break;
            case AgiotaMenuState.MaxLevelState:
                moneyInput.gameObject.SetActive(false);
                moneyInput.GetComponent<InputField>().interactable = false;

                firstButton.gameObject.SetActive(false);
                firstButton.GetComponent<Button>().interactable = false;

                secondButton.gameObject.SetActive(false);
                secondButton.GetComponent<Button>().interactable = false;

                challengeButton.gameObject.SetActive(true);
                challengeButton.GetComponent<Button>().interactable = true;

                thirdButton.GetComponentInChildren<Text>().text = "ainda não";
                break;
            case AgiotaMenuState.ChallengeState:
                moneyInput.gameObject.SetActive(false);
                moneyInput.GetComponent<InputField>().interactable = false;

                firstButton.gameObject.SetActive(true);
                firstButton.GetComponent<Button>().interactable = true;
                firstButton.GetComponentInChildren<Text>().text = "lutar";

                secondButton.gameObject.SetActive(false);
                secondButton.GetComponent<Button>().interactable = false;

                challengeButton.gameObject.SetActive(false);
                challengeButton.GetComponent<Button>().interactable = false;

                thirdButton.gameObject.SetActive(false);
                thirdButton.GetComponent<Button>().interactable = false;

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
        } else if (menuState == AgiotaMenuState.MaxLevelState)
        {
            resetButton(challengeButton);
            menuState = AgiotaMenuState.ChallengeState;
            loanPrompt.GetComponent<Text>().text = "hahaha...essa ousadia vai custar muito, muito caro...";
        } else if (menuState == AgiotaMenuState.ChallengeState)
        {
            resetButton(firstButton);
            GetComponent<ChangeScenes>().areaToLoad = "finalBattle";
            GetComponent<ChangeScenes>().PrepareFadeChange();
        } else if (menuState == AgiotaMenuState.DebtLimitState)
        {
            resetButton(firstButton);
            GetComponent<ChangeScenes>().areaToLoad = "badEndScene";
            GetComponent<ChangeScenes>().PrepareFadeChange();
        } else if (menuState == AgiotaMenuState.PaidDebtState)
        {
            resetButton(firstButton);
            GetComponent<ChangeScenes>().areaToLoad = "goodEndScene";
            GetComponent<ChangeScenes>().PrepareFadeChange();
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

                    if (checkDebt() == EndingType.DebtOverLimit)
                    {
                        loanPrompt.GetComponent<Text>().text = "com uma dívida tão grande... vocês realmente falharam";
                        menuState = AgiotaMenuState.DebtLimitState;
                    } else
                    {
                        menuState = AgiotaMenuState.ReturnState;
                        loanPrompt.GetComponent<Text>().text = "muito bem. mais alguma coisa?";
                    }
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

                    if (checkDebt() == EndingType.DebtPaid)
                    {
                        loanPrompt.GetComponent<Text>().text = "impressionante... acredito que suas dívidas foram quitadas";
                        menuState = AgiotaMenuState.PaidDebtState;
                    }
                    else
                    {
                        menuState = AgiotaMenuState.ReturnState;
                        loanPrompt.GetComponent<Text>().text = "muito bem. mais alguma coisa?";
                    }
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
            case AgiotaMenuState.MaxLevelState:
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

    private EndingType checkDebt()
    {
        if (GameManager.instance.groupDebt > GameManager.instance.maxDebt)
        {
            return EndingType.DebtOverLimit;
        }
        else if (GameManager.instance.groupDebt <= 0)
        {
            return EndingType.DebtPaid;
        }

        return EndingType.NoEnding;
    }
}
