using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyActions : FighterStats
{
    private GameObject hero;
    private GameObject hero2;
    private GameObject hero3;


    public static bool EnemyGotKill = false;

    public static bool heroDead = false;
    public static bool hero2Dead = false;
    public static bool hero3Dead = false;


    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject skillPrefab;

    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAttack;
    
     void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        hero2 = GameObject.FindGameObjectWithTag("Hero2");
        hero3 = GameObject.FindGameObjectWithTag("Hero3");
    }
    public void SelectAttack(string btn)
    {
         
        GameObject victim = null;

        int target = Random.Range(0, 3); //Define qual dos 3 hérois será atacado
        if (target == 0)
        {   
                if(heroDead == false)
                {
                    victim = hero;  
                }
                else if(hero2Dead == false)
                {
                    victim = hero2;
                }
                else if(hero3Dead == false)
                {
                    victim = hero3;
                }
                  
        }

        if (target == 1)
        {
                if(hero2Dead == false)
                {
                    victim = hero2;  
                }
                else if(heroDead == false)
                {
                    victim = hero;
                }
                else if(hero3Dead == false)
                {
                    victim = hero3;
                }

        }
            
        if (target == 2)
        {
                if(hero3Dead == false)
                {
                    victim = hero3;  
                }
                else if(heroDead == false)
                {
                    victim = hero;
                }
                else if(hero2Dead == false)
                {
                    victim = hero2;
                }   
        }

        Debug.Log("O alvo escolhido foi " + target);
        
        
        if (btn.CompareTo("melee") == 0)
        {
         
            meleePrefab.GetComponent<AttackScript>().Attack(victim);
  
        } 
        else if (btn.CompareTo("skill") == 0)
        {
            
            skillPrefab.GetComponent<AttackScript>().Attack(victim);

        } 
        else if (btn.CompareTo("defend") == 0)
        {
            //Debug.Log("C");
        }
        victim = null;
    }

    public override void MakeDeath()
    {
        
        base.MakeDeath();
        Debug.Log("ETA PORRA O INIMIGO MORREU");
        
    }
    
}