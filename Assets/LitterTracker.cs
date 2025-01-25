using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LitterTracker : MonoBehaviour
{
    public List<GameObject> litters = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            litters.Add(transform.GetChild(i).gameObject);
        }
    }

    public void AddLitter (GameObject litter)
    {
        litters.Add(litter);
    }
}
