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
        GameObject victim = hero;

        int target = Random.Range(0, 3);

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


        if (tag == "Hero" || tag == "Hero2" || tag == "Hero3" )
        {
            victim = enemy;
            
        }
        if (btn.CompareTo("melee") == 0)
        {
            meleePrefab.GetComponent<AttackScript>().Attack(victim);
            //Debug.Log("A");

        } 
        else if (btn.CompareTo("skill") == 0)
        {
            skillPrefab.GetComponent<AttackScript>().Attack(victim);
            //Debug.Log("B");
        } 
        else if (btn.CompareTo("defend") == 0)
        {
            //Debug.Log("C");
        }
    }
}
