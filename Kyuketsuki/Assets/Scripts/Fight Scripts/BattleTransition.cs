using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransition : AreaExit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            AreaMaps currentMap = AreaMaps.ForestArea;

            switch (areaTransitionName)
            {
                case "forestToBattle":
                    currentMap = AreaMaps.ForestArea;
                    break;
                case "mineToBattle":
                    currentMap = AreaMaps.MineArea;
                    break;
                case "monasteryToBattle":
                    currentMap = AreaMaps.MonasteryArea;
                    break;
            }

            string battleSceneName = "";
            switch (currentMap)
            {
                case AreaMaps.ForestArea:
                    int battleId = Random.Range(0, 1);
                    battleSceneName = "ForestBattle" + battleId.ToString();
                    break;
            }

            areaToLoad = battleSceneName;

            PlayerController.instance.areaTransitionName = "";
            GameManager.instance.EnteredBattle(other.gameObject.transform.position, currentMap, gameObject.transform.parent.name);

            base.OnTriggerEnter2D(other);
        }
    }
}
