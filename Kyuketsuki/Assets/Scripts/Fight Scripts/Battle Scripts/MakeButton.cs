using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physical;


    private GameObject hero;
    private GameObject hero2;
    private GameObject hero3;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
        hero2 = GameObject.FindGameObjectWithTag("Hero2");
        hero3 = GameObject.FindGameObjectWithTag("Hero3");
    }

    private void AttachCallback(string btn)
    {
        if (btn.CompareTo("AttackButton") == 0)
        {
            if(GameController.hero1Turn == true)
            {
                hero.GetComponent<PlayerActions>().SelectAttack("melee");
            }

            if(GameController.hero2Turn == true)
            {
                hero2.GetComponent<PlayerActions>().SelectAttack("melee");
            }

            if(GameController.hero3Turn == true)
            {
                hero3.GetComponent<PlayerActions>().SelectAttack("melee");
            }
            

        } else if (btn.CompareTo("SkillButton") == 0)
        {
            if(GameController.hero1Turn == true)
            {
                hero.GetComponent<PlayerActions>().SelectAttack("skill");
            }

            if(GameController.hero2Turn == true)
            {
                hero2.GetComponent<PlayerActions>().SelectAttack("skill");
            }

            if(GameController.hero3Turn == true)
            {
                hero3.GetComponent<PlayerActions>().SelectAttack("skill");
            }
            

            
            
        } else if (btn.CompareTo("DefendButton") == 0)
        {
            hero.GetComponent<PlayerActions>().SelectAttack("defend");
        }
    }
}