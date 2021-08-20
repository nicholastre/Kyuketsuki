using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{

    public Image initialFade;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeSetImage(initialFade));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeSetImage(Image img)
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

        img.gameObject.SetActive(false);
    }
}
