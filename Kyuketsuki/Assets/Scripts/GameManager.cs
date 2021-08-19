using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // Informacoes que precisam ser salvas e carregadas em cada jogo
    public string tempMissions;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public int groupMoney;
    public int groupDebt;

    private int maxDebt = 100000;
    public CharStats[] playerStats;     // Dados dos personagens carregados para uso em jogo
    public int currentGold;
    public Item[] referenceItems;       // Contem os Prefabs de cada item no jogo
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas,shopActive;

	// Use this for initialization
	void Start () {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
	}
	
	// Update is called once per frame
	void Update () {
        //Verifica os booleanos para travar o jogador
        if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive)
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
            Debug.Log("Inventario a seguir");
            for (int i = 0; i < numberOfItems.Length; i++)
            {
                Debug.Log(itemsHeld[i] + ": " + numberOfItems[i]);
            }
            playerStats[0].PrintStats();
            playerStats[1].PrintStats();
            playerStats[2].PrintStats();
        } else if (Input.GetKeyDown(KeyCode.L))
        {
            SaveGame();
        } else if (Input.GetKeyDown(KeyCode.T))
        {
            groupMoney += 200;
            groupDebt += 100;

            for (int i = 0; i < playerStats.Length; i++)
            {
                playerStats[i].changeHitPoints(-10);
                playerStats[i].changeMagicPoints(-10);
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Dagger");
            AddItem("teste");
            RemoveItem("HP Potion");
            RemoveItem("teste");
        }
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

        // Recupera os personagens
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i].changeHitPoints(playerStats[i].maxHP);
            playerStats[i].changeMagicPoints(playerStats[i].maxMP);
        }

        return changedLevels;
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
    private void GiveSavedToChar(SavedCharacterStats saved, CharStats current)
    {

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

            GiveSavedToChar(save.playerStats[0], playerStats[0]);
            GiveSavedToChar(save.playerStats[1], playerStats[1]);
            GiveSavedToChar(save.playerStats[2], playerStats[2]);

            tempMissions = save.tempMissions;
            itemsHeld = save.itemsHeld;
            numberOfItems = save.numberOfItems;
            groupMoney = save.groupMoney;
            groupDebt = save.groupDebt;
        } else
        {
            Debug.Log("Sem jogo salvo");
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

        GameMenu.instance.ShowItems();
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

            GameMenu.instance.ShowItems();
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
        checkDebt();
    }

    public void checkDebt()
    {
        if (groupDebt > maxDebt)
        {
            // implementar game over ruim
        } else if (groupDebt <= 0)
        {
            // implementar game over bom
        }
    }
}
