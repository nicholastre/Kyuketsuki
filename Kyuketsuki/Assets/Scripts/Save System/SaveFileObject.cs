using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// Representa os arquivos sendo salvos
public class SaveFileObject
{
    public SavedCharacterStats[] playerStats;
    public string tempMissions;     // Variaveis temporarias enquanto nao houver as reais
    public string[] itemsHeld;
    public int[] numberOfItems;
    public int groupMoney;
    public int groupDebt;
}
