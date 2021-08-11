using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Configuram cena para carregar, tempo para esperar a mudanca e variavel de controle da mudanca
    public string areaToLoad;
    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Uma vez que o fade terminou, aguarda o tempo configurado e muda de tela
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    // Funcao chamada para iniciar a transicao de tela
    public void PrepareFadeChange()
    {
        // Inicia um fade para trocar de tela posteriormente
        UIFade.instance.FadeToBlack();
        shouldLoadAfterFade = true;
    }

    // Encerra o jogo nessa cena. Chamada pelo ButtonToEnd na cena mainMenu
    public void EndGameOnScene()
    {
        Application.Quit();
    }
}
