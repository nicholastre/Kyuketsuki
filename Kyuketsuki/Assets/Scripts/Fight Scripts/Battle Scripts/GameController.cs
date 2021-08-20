using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Transactions;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour
{
   public List<FighterStats> fighterStats;

    private GameObject battleMenu;

    public Text battleText;

    public static bool hero1Turn;
    public static bool hero2Turn;
    public static bool hero3Turn;

    public static bool TwoEnemies = false;
    public static bool ThreeEnemies = true;



    private void Awake()
    {
        battleMenu = GameObject.Find("ActionMenu");
    }
    void Start()
    {   

        fighterStats = new List<FighterStats>();
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        FighterStats currentFighterStats = hero.GetComponent<FighterStats>();
        currentFighterStats.CalculateNextTurn(0);
        fighterStats.Add(currentFighterStats);

        GameObject hero2 = GameObject.FindGameObjectWithTag("Hero2");
        FighterStats currentFighterStats2 = hero2.GetComponent<FighterStats>();
        currentFighterStats2.CalculateNextTurn(0);
        fighterStats.Add(currentFighterStats2);

        GameObject hero3 = GameObject.FindGameObjectWithTag("Hero3");
        FighterStats currentFighterStats3 = hero3.GetComponent<FighterStats>();
        currentFighterStats3.CalculateNextTurn(0);
        fighterStats.Add(currentFighterStats3);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        if(TwoEnemies == true || ThreeEnemies == true )
        {
            GameObject enemy2 = GameObject.FindGameObjectWithTag("Enemy2");
            FighterStats currentEnemyStats2 = enemy2.GetComponent<FighterStats>();
            currentEnemyStats2.CalculateNextTurn(0);
            fighterStats.Add(currentEnemyStats2);
        }

        if(ThreeEnemies == true)
        {
            GameObject enemy3 = GameObject.FindGameObjectWithTag("Enemy3");
            FighterStats currentEnemyStats3 = enemy3.GetComponent<FighterStats>();
            currentEnemyStats3.CalculateNextTurn(0);
            fighterStats.Add(currentEnemyStats3);
        }

        fighterStats.Sort();
        

        NextTurn();
    }

    public void Update()
    {
        checkBattle();
    }

     public void NextTurn()
    {
        
        battleText.gameObject.SetActive(false);
        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);
        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            
        
            fighterStats.Sort();
            if(currentUnit.tag == "Hero")
            {
                this.battleMenu.SetActive(true);
                //Debug.Log("Wizrard 1 turno");
                hero1Turn = true;
                hero2Turn = false;
                hero3Turn = false;
                

            } 
            if(currentUnit.tag == "Hero2")
            {
                this.battleMenu.SetActive(true);
                //Debug.Log("Wizrard 2 turno");
                hero1Turn = false;
                hero2Turn = true;
                hero3Turn = false;
                
            }
            if(currentUnit.tag == "Hero3")
            {
                this.battleMenu.SetActive(true);
                //Debug.Log("Wizrard 3 turno");
                hero1Turn = false;
                hero2Turn = false;
                hero3Turn = true;
                
            }
            if(currentUnit.tag == "Enemy")
            {
                this.battleMenu.SetActive(false);
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "skill";
                currentUnit.GetComponent<EnemyActions>().SelectAttack(attackType);
            }
            if(currentUnit.tag == "Enemy2")
            {
                this.battleMenu.SetActive(false);
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "skill";
                currentUnit.GetComponent<EnemyActions>().SelectAttack(attackType);
            }
            if(currentUnit.tag == "Enemy3")
            {
                this.battleMenu.SetActive(false);
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "skill";
                currentUnit.GetComponent<EnemyActions>().SelectAttack(attackType);
            }
            
        } else
        {
            NextTurn();
        }
    }
    public void checkBattle()
    {

        GameObject[] deadEnemies = GameObject.FindGameObjectsWithTag("DeadEnemy");
        int numberDE = deadEnemies.Length;
        Debug.Log("O número de inimigos mortos é " + numberDE);
        if(numberDE >= 3){
            endBattle();
        }


    }
    public void endBattle()
    {
        Debug.Log("LUTA ENCERRADA");
        //enabled = false;
    }
    
}
