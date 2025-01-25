/*
 * Branch: BenH (Higham, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 * 
 * Branch: Hossein (Soroor, Hossein)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938, b19f37c3e27817dba491819664ea3e95d924333e
 */

using System.Collections.Generic;
using UnityEngine;


public class LitterDropper : MonoBehaviour
{
    // Function to spawn litter
    public void DropLitter()
    {
        if (GameManager.GetLitterManager()._worldLitter.Count >= GameManager.GetReferenceManager().MaxLitterAmount)
        {
            //Debug.Log("Litter cap reached");
            return;
        }

        //Creates a new Litter object
        GameObject litterObject = new GameObject();

        Litter instance = litterObject.AddComponent<Litter>();

        instance.gameObject.layer = GameManager.GetReferenceManager().GetLayerFromMask(referenceLayers.INTERACTABLE);
        instance.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        instance.Init(GameManager.GetLitterManager().GetRandomLitterData(), litterObject);

        // Adds the litter item to the LitterManager
        GameManager.GetLitterManager().AddLitter(instance);
    }
}
