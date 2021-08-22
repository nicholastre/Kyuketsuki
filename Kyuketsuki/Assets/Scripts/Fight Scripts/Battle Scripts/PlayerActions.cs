using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : FighterStats
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
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemy2 = GameObject.FindGameObjectWithTag("Enemy2");
        enemy3 = GameObject.FindGameObjectWithTag("Enemy3");
        hero = GameObject.FindGameObjectWithTag("Hero"); // Coloquei pra eles buscarem e reconhecer os hérois pra possibilitar o fogo amigo, uso de itens em skills nele mesmo e nos aliados.
        hero2 = GameObject.FindGameObjectWithTag("Hero2");
        hero3 = GameObject.FindGameObjectWithTag("Hero3");
    }
    public void SelectAttack(string btn)
    {
        
        GameObject victim = null;
          
        //Definição do alvo de acordo com a string guardada em MouseClick.tagName (Inimigo que foi selecionado pelo jogador)  
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
        if (MouseClick.tagName == "Hero") //Reconhecimento de Heróis para fogo amigo, uso de itens e skills de suporte nele mesmo ou nos aliados
        {
            victim = hero;
        }
        if (MouseClick.tagName == "Hero2") //Reconhecimento de Heróis para fogo amigo, uso de itens e skills de suporte nele mesmo ou nos aliados
        {
            victim = hero2;
        }
        if (MouseClick.tagName == "Hero3") //Reconhecimento de Heróis para fogo amigo, uso de itens e skills de suporte nele mesmo ou nos aliados
        {
            victim = hero3;
        }
                
        //Checagem de qual botão foi apertado pelo jogador
        if (btn.CompareTo("melee") == 0)
        {
         
            if((MouseClick.tagName!="null"))
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
            
            if((MouseClick.tagName!="null"))
            {
                skillPrefab.GetComponent<AttackScript>().Attack(victim);

            }
            else
            {
              Debug.Log("SEM ALVO SELECIONADO");  
            }
        } 
        else if (btn.CompareTo("defend") == 0)
        {
        }
        victim = null;
    }

    public override void MakeDeath()
    {
        Debug.Log("ETA PORRA O aliado MORREU");
    }
    
}