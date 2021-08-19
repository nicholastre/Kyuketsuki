using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class restingEvents : MonoBehaviour
{
    public Text restingDescription;
    public Text buttonLabel;

    private bool[] changedLevels;

    // Controlam o fluxo de eventos sendo verificados ao descansar
    private int eventsState = 0;

    // Start is called before the first frame update
    void Start()
    {

        changedLevels = GameManager.instance.RestCharacters();

        if (changedLevels[0] == true || changedLevels[1] == true || changedLevels[2] == true)
        {
            // Representa os eventos de avanco de nivel, caso tenha
            eventsState = 1;
        }
        else
        {
            eventsState = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkingRestEvents()
    {
        if (eventsState == 1)
        {
            CharStats[] storedCharacters = GameManager.instance.playerStats;

            if (changedLevels[0] == true)
            {
                restingDescription.text = storedCharacters[0].charName + " avançou para o nível " + storedCharacters[0].playerLevel + "!";
                changedLevels[0] = false;
            }
            else if (changedLevels[1] == true)
            {
                restingDescription.text = storedCharacters[1].charName + " avançou para o nível " + storedCharacters[1].playerLevel + "!";
                changedLevels[1] = false;
            }
            else if (changedLevels[2] == true)
            {
                restingDescription.text = storedCharacters[2].charName + " avançou para o nível " + storedCharacters[2].playerLevel + "!";
                changedLevels[2] = false;
            }

            if (changedLevels[0] == false && changedLevels[1] == false && changedLevels[2] == false)
            {
                // Da uma ultima mensagem sobre o nivel atual do grupo
                eventsState = 2;
            }
        }
        else if (eventsState == 2)
        {
            int levelSum = 0;
            CharStats[] storedCharacters = GameManager.instance.playerStats;

            for (int i = 0; i < storedCharacters.Length; i++)
            {
                levelSum += storedCharacters[i].playerLevel;
            }

            int averageLevel = levelSum / 3;

            if (averageLevel == storedCharacters[0].maxLevel)
            {
                restingDescription.text = "Com o poder que temos agora...tem outra solução pro Agiota.";
            }
            else if (averageLevel > storedCharacters[0].maxLevel / 2)
            {
                restingDescription.text = "Estamos bem fortes agora...um pouco mais e...quem sabe?";
            }
            else
            {
                restingDescription.text = "Mais um dia trabalhando para pagar o Agiota...";
            }

            eventsState = 3;
        }
        else if (eventsState == 3)
        {
            restingDescription.text = "O progresso do jogo foi salvo.";

            GameManager.instance.SaveGame();

            buttonLabel.text = "Sair";
            eventsState = 4;
        }
        else if (eventsState == 4)
        {
            ChangeScenes changeSceneComponent = GetComponent<ChangeScenes>();

            changeSceneComponent.PrepareFadeChange();
        }
    }
}
