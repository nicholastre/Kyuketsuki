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
        base.OnTriggerEnter2D(other);

        if (other.tag == "Player")
        {
            AreaMaps currentMap = AreaMaps.ForestArea;

            if (areaTransitionName == "forestToBattle")
            {
                currentMap = AreaMaps.ForestArea;
            }

            GameManager.instance.EnteredBattle(currentMap, gameObject.transform.parent.name);
        }
    }
}
