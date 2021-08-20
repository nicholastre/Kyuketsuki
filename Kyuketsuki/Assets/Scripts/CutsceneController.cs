using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CutsceneScenes
{
    StarterScene,
    FirstScene,
    SecondScene,
    ThirdScene,
    FourthScene
}

public class CutsceneController : MonoBehaviour
{
    public Image initialFade;
    public Component cutsceneText;
    public Component confirmButton;
    public Component[] cutsceneImages;

    private CutsceneScenes currentScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeSetImage(initialFade));
        currentScene = CutsceneScenes.StarterScene;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentScene)
        {
            case CutsceneScenes.FirstScene:
                cutsceneText.GetComponent<Text>().text = "agora que estamos todos aqui, primeiro: não importa o motivo da dívida de cada um, ok?";
                break;
            case CutsceneScenes.SecondScene:
                cutsceneText.GetComponent<Text>().text = "o que importa é que o povo paga muito bem para matarem monstros e nossos poderes servem bem para isso";
                break;
            case CutsceneScenes.ThirdScene:
                cutsceneText.GetComponent<Text>().text = "então é pegar umas missões ali, matar bichos, pegar a grana e pagar o agiota. facinho e todo mundo sai quite";
                break;
            case CutsceneScenes.FourthScene:
                cutsceneText.GetComponent<Text>().text = "só lembrem de não aumentar a dívida... com poderes maiores que os nossos, vai que ele resolve cobrar de outro jeito";
                break;
        }
    }

    IEnumerator FadeSetImage(Image img)
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

        switch (currentScene)
        {
            case CutsceneScenes.StarterScene:
                img.gameObject.SetActive(false);
                currentScene = CutsceneScenes.FirstScene;
                break;
            case CutsceneScenes.FirstScene:
                currentScene = CutsceneScenes.SecondScene; 
                break;
            case CutsceneScenes.SecondScene:
                currentScene = CutsceneScenes.ThirdScene; 
                break;
            case CutsceneScenes.ThirdScene:
                currentScene = CutsceneScenes.FourthScene;
                break;
        }
    }

    public void ClickConfirm()
    {
        resetButton(confirmButton);

        switch (currentScene)
        {
            case CutsceneScenes.FirstScene:
                StartCoroutine(FadeSetImage(cutsceneImages[0].GetComponent<Image>()));
                break;
            case CutsceneScenes.SecondScene:
                StartCoroutine(FadeSetImage(cutsceneImages[1].GetComponent<Image>()));
                break;
            case CutsceneScenes.ThirdScene:
                StartCoroutine(FadeSetImage(cutsceneImages[2].GetComponent<Image>()));
                break;
            case CutsceneScenes.FourthScene:
                GetComponentInChildren<ChangeScenes>().PrepareFadeChange();
                break;
        }
    }

    private void resetButton(Component compButton)
    {
        Button button = compButton.GetComponent<Button>();
        button.enabled = !button.enabled;
        button.enabled = !button.enabled;
    }
}
