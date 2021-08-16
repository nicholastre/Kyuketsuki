using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FighterStats : MonoBehaviour, IComparable
{
    
    
     [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject magicFill;

    [Header("Stats")]
    public float health;
    public float magic;
    public float melee;
    public float range;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    // Resize health and magic bar
    private Transform healthTransform;
    private Transform magicTransform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;
    private GameObject GameControllerObj;
    public GameObject SpawnDeadObj;

   
    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        magicTransform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;

        startHealth = health;
        startMagic = magic;

        GameControllerObj = GameObject.Find("GameControllerObject");
        
    }
    public void ReceiveDamage(float damage)
    {
        health = health - damage;
        animator.Play("DamageTaken");

        // Set damage text

        if(health <= 0)
        {
            dead = true;
            if (gameObject.tag == "Hero1" || gameObject.tag == "Hero2" || gameObject.tag == "Hero3")
            {
                gameObject.tag = "DeadHero";
            }else
            {
                //gameObject.tag = "DeadEnemy";
                SpawnDeadObj = new GameObject("DeadEnemy");
                SpawnDeadObj.tag = "DeadEnemy";
            }
            
            Destroy(healthFill);
            Destroy(gameObject);
            
            //healthFill.SetActive(false);
            //gameObject.SetActive(false);
            
            
        } else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }
          if(damage > 0)
        {
            GameControllerObj.GetComponent<GameController>().battleText.gameObject.SetActive(true);
            GameControllerObj.GetComponent<GameController>().battleText.text = damage.ToString();
        }
        Invoke("ContinueGame", 1);
    }

    public void updateMagicFill(float cost)
    {
        if(cost > 0)
        {
            magic = magic - cost;
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
        }
    }

    public bool GetDead()
    {
        return dead;
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return nex;
    }

    void ContinueGame()
    {
        //Debug.Log("Entrou 2");
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }

     public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }
}
