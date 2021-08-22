using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityShop : MonoBehaviour
{
    public string[] itemsForSale;
    public ItemButton[] itemButtons;
    public GameObject itemInfoPanel;
    public Text feedbackText, buyItemName, buyItemDescription, buyItemValue;
    public Button buyButton;

    private Item selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        OpenBuyMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBuyMenu()
    {

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                itemButtons[i].GetComponent<Image>().enabled = true;
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                itemButtons[i].amountText.gameObject.SetActive(false);
            }
            else
            {
                itemButtons[i].GetComponent<Image>().enabled = false;
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.gameObject.SetActive(false);
            }
        }
    }

    public void SelectBuyItem(int buttonValue)
    {
        feedbackText.gameObject.SetActive(false);
        selectedItem = GameManager.instance.GetItemDetails(itemsForSale[buttonValue]);
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = "Custo: " + selectedItem.value + " bits";
        itemInfoPanel.SetActive(true);
        buyButton.gameObject.SetActive(true);
    }

    public void BuyItem()
    {
        buyButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(true);

        if (GameManager.instance.groupMoney >= selectedItem.value)
        {
            GameManager.instance.groupMoney -= selectedItem.value;
            GameManager.instance.AddItem(selectedItem.itemName);

            feedbackText.text = selectedItem.itemName + " foi comprado!";
        } else
        {
            feedbackText.text = "sem bits suficientes!";
        }

        selectedItem = null;
        itemInfoPanel.SetActive(false);
        buyButton.gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(true);
    }
}
