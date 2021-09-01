using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public enum AreaMaps
{
    ForestArea,
    MineArea,
    MonasteryArea
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // Informacoes que precisam ser salvas e carregadas em cada jogo
    public string tempMissions;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public int groupMoney;
    public int groupDebt;

    public int averageLevel;
    public int maxDebt = 100000;
    public CharStats[] playerStats = new CharStats[3];     // Dados dos personagens carregados para uso em jogo
    public Item[] referenceItems;       // Contem os Prefabs de cada item no jogo
    public bool dialogActive, fadingBetweenAreas, battleActive;

    public AreaMaps currentArea;
    public string enemyEncountered = "";
    public Vector3 playerPosition;
    public bool groupWasDefeated = false;

	// Use this for initialization
	void Start () {
        instance = this;

        playerPosition = Vector3.zero;

        DontDestroyOnLoad(gameObject);

        //SortItems();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(playerPosition);

        //Verifica os booleanos para travar o jogador
        if(dialogActive || fadingBetweenAreas || battleActive)
        {
            PlayerController.instance.canMove = false;
        }
        else if (PlayerController.instance != null)
        {
            PlayerController.instance.canMove = true;
        }

        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            int sumHalfwayExp = 0;

            for (int i = 0; i < (playerStats[0].statsInstance.maxLevel / 2) + 1; i++)
            {
                sumHalfwayExp += playerStats[0].statsInstance.expToNextLevel[i];
            }

            for (int i = 0; i < playerStats.Length; i++)
            {
                playerStats[i].AddExp(sumHalfwayExp);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            int sumTotalExp = 0;

            for (int i = 0; i < playerStats[0].statsInstance.maxLevel; i++)
            {
                sumTotalExp += playerStats[0].statsInstance.expToNextLevel[i];
            }

            for (int i = 0; i < playerStats.Length; i++)
            {
                playerStats[i].AddExp(sumTotalExp);
            }
        } else if (Input.GetKeyDown(KeyCode.T))
        {
            groupMoney += 10000;
        } else if (Input.GetKeyDown(KeyCode.U))
        {
            groupDebt += 20000;
        } else if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 1; i < playerStats.Length; i++)
            {
                playerStats[i].statsInstance.currentHP = 0;
                playerStats[i].statsInstance.currentMP = 0;
            }

            playerStats[0].statsInstance.currentHP = 1;
        }*/
    }

    public bool[] RestCharacters()
    {
        // Guarda quais personagens avancaram de nivel
        bool[] changedLevels = { false, false, false };

        // Verifica se cada personagem avancou de nivel
        for (int i = 0; i < playerStats.Length; i++)
        {
            changedLevels[i] = playerStats[i].CheckLevelUp();
        }

        averageLevel = (playerStats[0].statsInstance.playerLevel + playerStats[1].statsInstance.playerLevel 
            + playerStats[2].statsInstance.playerLevel) / 3;

        // Recupera os personagens
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i].restoreCharacter();
        }

        return changedLevels;
    }

    public void RestoreCharacters()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i].restoreCharacter();
        }
    }

    // Cria o objeto que contem as informacoes salvas e passa os dados para ele
    private SaveFileObject CreateSaveFileObject () {

        SaveFileObject save = new SaveFileObject();

        save.playerStats = new SavedCharacterStats[3];

        save.playerStats[0] = PrepareCharToSaved(playerStats[0]);
        save.playerStats[1] = PrepareCharToSaved(playerStats[1]);
        save.playerStats[2] = PrepareCharToSaved(playerStats[2]);

        save.tempMissions = tempMissions;
        save.itemsHeld = itemsHeld;
        save.numberOfItems = numberOfItems;
        save.groupMoney = groupMoney;
        save.groupDebt = groupDebt;

        return save;
    }

    // Funcao que copia CharStats para o formato de SavedCharacterStats
    private SavedCharacterStats PrepareCharToSaved(CharStats original)
    {
        SavedCharacterStats temp = new SavedCharacterStats();

        temp.charName = original.statsInstance.charName;
        temp.playerLevel = original.statsInstance.playerLevel;
        temp.currentEXP = original.statsInstance.currentEXP;
        temp.expToNextLevel = original.statsInstance.expToNextLevel;
        temp.maxLevel = original.statsInstance.maxLevel;
        temp.baseEXP = original.statsInstance.baseEXP;

        temp.currentHP = original.statsInstance.currentHP;
        temp.maxHP = original.statsInstance.maxHP;
        temp.currentMP = original.statsInstance.currentMP;
        temp.maxMP = original.statsInstance.maxMP;

        temp.strength = original.statsInstance.strength;
        temp.defence = original.statsInstance.defence;
        temp.agility = original.statsInstance.agility;

        temp.wpnPwr = original.statsInstance.wpnPwr;
        temp.armrPwr = original.statsInstance.armrPwr;

        temp.equippedWpn = original.statsInstance.equippedWpn;
        temp.equippedArmr = original.statsInstance.equippedArmr;

        return temp;
    }

    // Funcao que copia SavedCharacterStats para o formato de CharStats
    public CharStatsBase GiveSavedToChar(SavedCharacterStats saved)
    {

        CharStatsBase current = new CharStatsBase();

        current.charName = saved.charName;
        current.playerLevel = saved.playerLevel;
        current.currentEXP = saved.currentEXP;
        current.expToNextLevel = saved.expToNextLevel;
        current.maxLevel = saved.maxLevel;
        current.baseEXP = saved.baseEXP;

        current.currentHP = saved.currentHP;
        current.maxHP = saved.maxHP;
        current.currentMP = saved.currentMP;
        current.maxMP = saved.maxMP;

        current.strength = saved.strength;
        current.defence = saved.defence;
        current.agility = saved.agility;

        current.wpnPwr = saved.wpnPwr;
        current.armrPwr = saved.armrPwr;

        current.equippedWpn = saved.equippedWpn;
        current.equippedArmr = saved.equippedArmr;

        return current;
    }

    public void GiveExpToChar(int charIdentifier, int expValue)
    {
        playerStats[charIdentifier].AddExp(expValue);
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

            playerStats[0].statsInstance = GiveSavedToChar(save.playerStats[0]);
            playerStats[1].statsInstance = GiveSavedToChar(save.playerStats[1]);
            playerStats[2].statsInstance = GiveSavedToChar(save.playerStats[2]);

            tempMissions = save.tempMissions;
            itemsHeld = save.itemsHeld;
            numberOfItems = save.numberOfItems;
            groupMoney = save.groupMoney;
            groupDebt = save.groupDebt;
            averageLevel = (playerStats[0].statsInstance.playerLevel + playerStats[1].statsInstance.playerLevel 
                + playerStats[2].statsInstance.playerLevel) / 3;
        } else
        {
            tempMissions = "";
            for (int i = 0; i < itemsHeld.Length; i++)
            {
                itemsHeld[i] = "";
                numberOfItems[i] = 0;
            }
            groupMoney = 0;
            groupDebt = 50000;
            averageLevel = 1;
            for (int i = 0; i < playerStats.Length; i++)
            {
                playerStats[i].ResetCharacter();
            }
        }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;
        //listando os itens para que não fiquem jogados espalhados
        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if(itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }

        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for( int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if (referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }

            if (itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + "Não existe");
            }
        }

    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem =false;
        int itemPositon = 0;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPositon = i;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPositon]--;

            if(numberOfItems[itemPositon] <= 0)
            {
                itemsHeld[itemPositon] = "";
            }
        }
        else
        {
            Debug.LogError("não encontrado" + itemToRemove);
        }
    }

    public bool changeMoney(int modifier)
    {
        if (groupMoney + modifier <= 0)
        {
            return false;
        } else
        {
            groupMoney += modifier;
            return true;
        }
    }

    public void changeDebt(int modifier)
    {
        groupDebt += modifier;
    }

    public CharStats GetCharStats(int identifier)
    {
        return playerStats[identifier];
    }

    public void EnteredBattle(Vector3 player, AreaMaps areaId, string mobName)
    {
        playerPosition = player;
        currentArea = areaId;
        enemyEncountered = mobName;
    }
}
