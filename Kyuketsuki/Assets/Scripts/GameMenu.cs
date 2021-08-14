using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject[] windows;
    public GameObject theMenu;
    private CharStats[] playerStats;
    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Abre e fecha o menu dentro do jogo
        if (Input.GetButtonDown("Fire2")){
            if (theMenu.activeInHierarchy)
            {
                //theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false;
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }
    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for( int i = 0; i < playerStats.Length; i++)
        {
            //carrega os status de cada personagem puxando dos status dentro do GameManager
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "" + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel]; 
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charIamge;
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        //Pra escolher qual aba vai abrir é preciso ir no botão, adicionar um evento de click para o botão, jogar o UI CANVAS dentro do botão
        // e escolhe essa função passando qual seu número do array. Depois disso no UICANVAS vá em windows no script de GameMenu e determine
        // o numero da tela que corresponde ao botão
        for(int i = 0; i< windows.Length; i++)
        {
            if(i == windowNumber)
            {
                //verifica se esta ativo ou não no momento
                windows[i].SetActive(windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }
}
