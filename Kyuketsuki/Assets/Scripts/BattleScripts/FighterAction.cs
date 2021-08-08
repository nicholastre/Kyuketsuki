using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterAction : MonoBehaviour
{
    private GameObject hero;
    private GameObject hero2;
    private GameObject enemy;

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
    }
    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if (tag == "Hero" || tag == "Hero2" )
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
