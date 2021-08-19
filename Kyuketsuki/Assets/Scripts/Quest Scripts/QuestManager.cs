using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public string[] questMarkerNames;
    public bool[] questMarkersComplete;

    public static QuestManager instance;

	// Use this for initialization
	void Start () {
        instance = this;

        questMarkersComplete = new bool[questMarkerNames.Length];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckIfComplete("quest teste"));
            MarkQuestComplete("quest teste");
            MarkQuestIncomplete("mate o demonio");
        }
	}

    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if(questMarkerNames[i] == questToFind)
            {
                return i;
            }
        }
        //Deixar o array zero porque é o que vai ser retornado caso não ache nenhuma quest
        Debug.LogError("Quest " + questToFind + " não existe");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        if(GetQuestNumber(questToCheck) != 0)
        {
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }

        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = true;

        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }
    
    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if(questObjects.Length > 0)
        {
            for(int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }
    }
}
