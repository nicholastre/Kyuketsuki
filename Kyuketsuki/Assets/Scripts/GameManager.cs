using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        save.playerStats[0] = DeepCopyStats(playerStats[0]);
        save.playerStats[1] = DeepCopyStats(playerStats[1]);
        save.playerStats[2] = DeepCopyStats(playerStats[2]);

        save.tempMissions = tempMissions;
        save.tempInventory = tempInventory;
        save.groupMoney = groupMoney;
        save.groupDebt = groupDebt;

        return save;
    }

    private CharStats DeepCopyStats(CharStats original)
    {
        CharStats temp = new CharStats();

        temp.charName = original.charName;
        temp.playerLevel = original.playerLevel;
        temp.currentEXP = original.currentEXP;
        temp.expToNextLevel = original.expToNextLevel;
        temp.maxLevel = original.maxLevel;
        temp.baseEXP = original.baseEXP;

        temp.currentHP = original.currentHP;
        temp.maxHP = original.maxHP;
        temp.currentMP = original.currentMP;
        temp.maxMP = original.maxMP;

        temp.strength = original.strength;
        temp.defence = original.defence;
        temp.agility = original.agility;

        temp.wpnPwr = original.wpnPwr;
        temp.armrPwr = original.armrPwr;

        temp.equippedWpn = original.equippedWpn;
        temp.equippedArmr = original.equippedArmr;

        temp.charIamge = original.charIamge;

        return temp;
    }

    public void SaveGame ()
    {
        SaveFileObject save = CreateSaveFileObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Jogo salvo");
    }

    public void LoadGame ()
    {

    }
}
