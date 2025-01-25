using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLitter : Litter
{
    // Used to place directly in the scene, purely for testing. 
    public LitterData testData;
    public bool randomiseTool;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetLitterManager().AddLitter(this);
        gameObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        if(testData == null)
        {
            Init(GameManager.GetLitterManager().GetRandomLitterData(), gameObject);
        }
        else
        {
            Init(testData, gameObject);
        }
    }
}
