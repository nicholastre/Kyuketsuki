using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // Informacoes que precisam ser salvas e carregadas em cada jogo
    public CharStats[] playerStats;
    public string tempMissions;
    public string tempInventory;
    public int groupMoney;
    public int groupDebt;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

	// Use this for initialization
	void Start () {
        instance = this;

        DontDestroyOnLoad(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
        //Verifica os booleanos para travar o jogador
        if(gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    private SaveFileObject CreateSaveFileObject () {

        SaveFileObject save = new SaveFileObject();

        save.playerStats[0] = playerStats[0];
        save.playerStats[1] = playerStats[1];
        save.playerStats[2] = playerStats[2];

        save.tempMissions = tempMissions;
        save.tempInventory = tempInventory;
        save.groupMoney = groupMoney;
        save.groupDebt = groupDebt;

        return save;
    }


}
