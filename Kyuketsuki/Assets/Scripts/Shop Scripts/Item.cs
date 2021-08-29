using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;

    public int armorStrength;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use(int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.GetCharStats(charToUseOn);

        if (isItem)
        {
            if (affectHP)
            {
                selectedChar.changeHitPoints(amountToChange);
            }

            if (affectMP)
            {
                selectedChar.changeMagicPoints(amountToChange);
            }

            if (affectStr)
            {
                selectedChar.statsInstance.strength += amountToChange;
            }
        }

        if (isWeapon)
        {
            if (selectedChar.statsInstance.equippedWpn != "")
            {
                GameManager.instance.AddItem(selectedChar.statsInstance.equippedWpn);
            }

            selectedChar.statsInstance.equippedWpn = itemName;
            selectedChar.statsInstance.wpnPwr = weaponStrength;
        }

        if (isArmour)
        {
            if (selectedChar.statsInstance.equippedArmr != "")
            {
                GameManager.instance.AddItem(selectedChar.statsInstance.equippedArmr);
            }

            selectedChar.statsInstance.equippedArmr = itemName;
            selectedChar.statsInstance.armrPwr = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
