using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CutsceneScenes
{
    FirstScene,
    SecondScene,
    ThirdScene,
    FourthScene,
    FifthScene,
    DoneScene
}

public class CutsceneController : MonoBehaviour
{
    public Component confirmButton;
    public Component[] cutsceneImages;

    private CutsceneScenes currentScene;

    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack(1f);
        currentScene = CutsceneScenes.FirstScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene == CutsceneScenes.FifthScene)
        {
            GetComponentInChildren<ChangeScenes>().PrepareFadeChange(1f);
            gameObject.transform.Find("MenuBackground").gameObject.SetActive(false);
            currentScene = CutsceneScenes.DoneScene;
        }
    }

    IEnumerator FadeSetImage(Image img, float speed = 1)
    {
        for (float i = 1; i >= 0; i -= (speed * Time.deltaTime))
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

        switch (currentScene)
        {
            case CutsceneScenes.FirstScene:
                currentScene = CutsceneScenes.SecondScene; 
                break;
            case CutsceneScenes.SecondScene:
                currentScene = CutsceneScenes.ThirdScene; 
                break;
            case CutsceneScenes.ThirdScene:
                currentScene = CutsceneScenes.FourthScene;
                break;
            case CutsceneScenes.FourthScene:
                currentScene = CutsceneScenes.FifthScene;
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
                StartCoroutine(FadeSetImage(cutsceneImages[3].GetComponent<Image>(), 10));
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
