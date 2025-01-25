using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mesh;

public class PlaceableLitter : Litter
{
    void Start()
    {
        GameManager.GetLitterManager().AddLitter(this);
        gameObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        Init(GameManager.GetLitterManager().GetRandomLitterData(), gameObject);
    }

    public void InitDroppedLitter()
    {
        GameManager.GetLitterManager().AddLitter(this);
        StartCoroutine(TogglePhysics());
    }
    IEnumerator TogglePhysics()
    {
        yield return new WaitForSeconds(3);
        gameObject.layer = GameManager.GetReferenceManager().GetLayerFromMask(referenceLayers.INTERACTABLE);
        // Remove physics stuff here (BH)
    }
}
