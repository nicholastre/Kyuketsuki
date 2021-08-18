using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterAction : MonoBehaviour
{
    private GameObject hero;
    private GameObject hero2;
    private GameObject hero3;
    private GameObject enemy;
    private GameObject enemy2;
    private GameObject enemy3;


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
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        hero2 = GameObject.FindGameObjectWithTag("Hero2");
        hero3 = GameObject.FindGameObjectWithTag("Hero3");
        enemy2 = GameObject.FindGameObjectWithTag("Enemy2");
        enemy3 = GameObject.FindGameObjectWithTag("Enemy3");
    }
    public void SelectAttack(string btn)
    {
        
        GameObject victim = null;

        if (tag == "Enemy" || tag == "Enemy2" || tag == "Enemy3" )
        {

            int target = Random.Range(0, 3); //Define qual dos 3 hérois será atacado

            if (target == 0)
            {
                victim = hero;
            }

            if (target == 1)
            {
                victim = hero2;
            }

            if (target == 2)
            {
                victim = hero3;
            }
        }


        if (tag == "Hero" || tag == "Hero2" || tag == "Hero3" ) // Caso quem execute esse script seja um dos 3 hérois, ele define um inimigo como alvo
        {

            
            if (MouseClick.tagName == "Enemy")
            {
                victim = enemy;
            }
            if (MouseClick.tagName == "Enemy2")
            {
                victim = enemy2;
            }
            if (MouseClick.tagName == "Enemy3")
            {
                victim = enemy3;
            }
            
            
        }
        if (btn.CompareTo("melee") == 0)
        {
         
            if((MouseClick.tagName!="null") && (tag == "Hero" || tag == "Hero2" || tag == "Hero3"))
            {
                meleePrefab.GetComponent<AttackScript>().Attack(victim);
                Debug.Log("ALVO SELECIONADO");
                Debug.Log("O alvo é " + MouseClick.tagName );
            }
            else if(tag == "Enemy" || tag == "Enemy2" || tag == "Enemy3"  )
            {
                meleePrefab.GetComponent<AttackScript>().Attack(victim);
            }
            else
            {
                Debug.Log("SEM ALVO SELECIONADO");
            }
            

        } 
        else if (btn.CompareTo("skill") == 0)
        {
            
            if((MouseClick.tagName!="null") && (tag == "Hero" || tag == "Hero2" || tag == "Hero3"))
            {
                skillPrefab.GetComponent<AttackScript>().Attack(victim);
                Debug.Log("ALVO SELECIONADO");
                Debug.Log("O alvo é " + MouseClick.tagName );

            }
            else if(tag == "Enemy" || tag == "Enemy2" || tag == "Enemy3"  )
            {
                meleePrefab.GetComponent<AttackScript>().Attack(victim);
            }
            else
            {
              Debug.Log("SEM ALVO SELECIONADO");  
            }
        } 
        else if (btn.CompareTo("defend") == 0)
        {
            //Debug.Log("C");
        }
        victim = null;
    }
    
}
