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
    private bool dead1st = false; // Variável que evita a geração de mais de um objeto DeadHero para o mesmo inimigo morto
    

    // Resize health and magic bar
    private Transform healthTransform;
    private Transform magicTransform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;
    private GameObject GameControllerObj;
    public GameObject SpawnDeadObj;
    public GameObject SpawnDeadObjHero;

   
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

        if(health <= 0)
        {
            dead = true;
            if ((gameObject.tag == "Hero") || (gameObject.tag == "Hero2") || (gameObject.tag == "Hero3"))
            {
                if(dead1st != true) // Variável que evita a geração de mais de um objeto DeadHero para o mesmo heroi morto. Verifica se ele for morto pela primeira vez, basicamente, porque não dá pra destruir um héroi, tem que setar inativo.
                {
                    SpawnDeadObjHero = new GameObject("DeadHero");
                    SpawnDeadObjHero.tag = "DeadHero";
                    gameObject.SetActive(false);
                    Destroy(healthFill);
                    Destroy(magicFill);
                    dead1st = true;
                    
                    if(gameObject.tag == "Hero"){
                        EnemyActions.heroDead = true;
                    }

                    if(gameObject.tag == "Hero2"){
                        EnemyActions.hero2Dead = true;
                    }

                    if(gameObject.tag == "Hero3"){
                        EnemyActions.hero3Dead = true;
                    }
                    MouseClick.tagName="null";
                }
                
            }else
            {
                SpawnDeadObj = new GameObject("DeadEnemy");
                SpawnDeadObj.tag = "DeadEnemy";
                MouseClick.tagName="null";
                
                Debug.Log("CHAMEI O MAKE Death");
                MakeDeath();
                Destroy(gameObject);
                Destroy(healthFill);
                Destroy(magicFill);
            }

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

    public void ContinueGame()
    {
        //MouseClick.tagName="null"; (Descomentar isso se quiser desabilitar o lock-on do alvo)
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }

     public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public virtual void MakeDeath()
    {

        Debug.Log("Eita, tá chamando a função da classe pai");
    }

}
