using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public static string tagName = "null";

    // Update is called once per frame
    public void Awake()
    {
        tagName = "null";
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {    
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit,100))
            {
                if(hit.transform != null)
                {
                    printTag(hit.transform.gameObject);
                }
            }
        }    
    }

    public void printTag(GameObject go)
    {
       tagName = go.tag;
       //Debug.Log("a tag é " + tagName);
    }
}
