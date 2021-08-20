using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hotelEntrance : MonoBehaviour
{

    public Text hotelPrompt;

    private ChangeScenes changeSceneComponent;

    // Start is called before the first frame update
    void Start()
    {
        changeSceneComponent = GetComponent<ChangeScenes>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void confirmStay()
    {
        if (GameManager.instance.groupMoney >= 100)
        {
            GameManager.instance.changeMoney(-100);

            int textNumber = Random.Range(1, 3);
            string randomText = "";

            switch (textNumber)
            {
                case 1:
                    randomText = "Quarto 213, tenham uma boa estadia!";
                    break;
                case 2:
                    randomText = "Terceiro andar, segunda porta.";
                    break;
                case 3:
                    randomText = "Perfeito, por favor me acompanhem.";
                    break;
            }

            hotelPrompt.text = randomText;

            changeSceneComponent.PrepareFadeChange(1f);
        } else
        {
            hotelPrompt.text = "Vocês não têm bits suficientes!";
        }
    }
}
