using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinAuraEnabler : MonoBehaviour
{
    //Bin Aura vfx
    public GameObject binAuraVFX1;
    public GameObject binAuraVFX2;
    public GameObject binAuraVFX3;
    public GameObject binAuraVFX4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetLitterManager()._worldLitter.Count >= 100)
        {
            binAuraVFX1.SetActive(true);
            binAuraVFX2.SetActive(true);
            binAuraVFX3.SetActive(true);
            binAuraVFX4.SetActive(true);
        }
        else
        {
            binAuraVFX1.SetActive(false);
            binAuraVFX2.SetActive(false);
            binAuraVFX3.SetActive(false);
            binAuraVFX4.SetActive(false);
        }
    }
}
