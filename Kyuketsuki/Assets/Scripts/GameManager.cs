using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // Informacoes que precisam ser salvas e carregadas em cada jogo
    public int savedStats;
    public string tempMissions;
    public string tempInventory;
    public int groupMoney;
    public int groupDebt;

    public CharStats[] playerStats;     // Dados dos personagens carregados para uso em jogo
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

	// Use this for initialization
	void Start () {
        instance = this;

        DontDestroyOnLoad(gameObject);

        LoadGame();
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            TempAddExp();
        } else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Dinheiro: " + groupMoney);
            Debug.Log("Divida: " + groupDebt);
            playerStats[0].PrintStats();
            //playerStats[1].PrintStats();
            //playerStats[2].PrintStats();
        } else if (Input.GetKeyDown(KeyCode.L))
        {
            SaveGame();
        } else if (Input.GetKeyDown(KeyCode.T))
        {
            groupDebt += 100;
        }
    }

    // Cria o objeto que contem as informacoes salvas e passa os dados para ele
    private SaveFileObject CreateSaveFileObject () {

        SaveFileObject save = new SaveFileObject();

        save.playerStats = new SavedCharacterStats[3];

        save.playerStats[0] = PrepareCharToSaved(playerStats[0]);
        //save.playerStats[1] = DeepCopyStats(playerStats[1]);
        //save.playerStats[2] = DeepCopyStats(playerStats[2]);

        save.tempMissions = tempMissions;
        save.tempInventory = tempInventory;
        save.groupMoney = groupMoney;
        save.groupDebt = groupDebt;

        return save;
    }

    // Funcao que copia CharStats para o formato de SavedCharacterStats
    private SavedCharacterStats PrepareCharToSaved(CharStats original)
    {
        SavedCharacterStats temp = new SavedCharacterStats();

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

        return temp;
    }

    // Funcao que copia SavedCharacterStats para o formato de CharStats
    private CharStats PrepareSavedToChar(SavedCharacterStats original)
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

        return temp;
    }

    private void TempAddExp()
    {
        playerStats[0].AddExp(50);
        playerStats[1].AddExp(50);
        playerStats[2].AddExp(50);
    }

    // Utiliza o objeto de save para guardar em um local de dados persistentes da maquina
    public void SaveGame ()
    {
        SaveFileObject save = CreateSaveFileObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Jogo salvo");
    }

    // Procura um arquivo de save no local padrao de dados persistentes da maquina
    public void LoadGame ()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveFileObject save = (SaveFileObject)bf.Deserialize(file);
            file.Close();

            playerStats[0] = PrepareSavedToChar(save.playerStats[0]);
            //playerStats[1] = DeepCopyStats(save.playerStats[1]);
            //playerStats[2] = DeepCopyStats(save.playerStats[2]);

            tempMissions = save.tempMissions;
            tempInventory = save.tempInventory;
            groupMoney = save.groupMoney;
            groupDebt = save.groupDebt;
        } else
        {
            Debug.Log("Sem jogo salvo");
        }
    }
}
