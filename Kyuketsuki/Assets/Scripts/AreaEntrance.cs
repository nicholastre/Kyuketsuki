﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour {

    public string transitionName;

	// Use this for initialization
	void Start () {
		if(transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = transform.position;
			PlayerController.instance.canMove = true;
        }

		UIFade.instance.FadeFromBlack();
		GameManager.instance.fadingBetweenAreas = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
