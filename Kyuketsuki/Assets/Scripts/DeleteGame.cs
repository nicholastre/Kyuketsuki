using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

enum DeleteState
{
    DefaultState,
    NoSaveExists,
    ConfirmDelete,
    GameDeleted
}

public class DeleteGame : MonoBehaviour
{

    private DeleteState buttonState;

    // Start is called before the first frame update
    void Start()
    {
        buttonState = DeleteState.DefaultState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickDelete()
    {
        resetButton(GetComponent<Button>());

        switch (buttonState)
        {
            case DeleteState.DefaultState:
                if (File.Exists(Application.persistentDataPath + "/gamesave.save")) {
                    GetComponentInChildren<Text>().text = "confirmar reset?";
                    buttonState = DeleteState.ConfirmDelete;
                } else
                {
                    GetComponentInChildren<Text>().text = "não há jogo salvo";
                    buttonState = DeleteState.NoSaveExists;
                }
                break;
            case DeleteState.ConfirmDelete:
                File.Delete(Application.persistentDataPath + "/gamesave.save");
                GetComponentInChildren<Text>().text = "jogo apagado";
                buttonState = DeleteState.GameDeleted;
                break;
            case DeleteState.GameDeleted:
                GetComponentInChildren<Text>().text = "não há jogo salvo";
                buttonState = DeleteState.NoSaveExists;
                break;
        }
    }

    private void resetButton(Button button)
    {
        button.enabled = !button.enabled;
        button.enabled = !button.enabled;
    }
}
