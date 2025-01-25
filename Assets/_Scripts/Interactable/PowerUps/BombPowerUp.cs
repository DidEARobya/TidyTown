/*
 * Branch: Hossein (Soroor, Hossein)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: b19f37c3e27817dba491819664ea3e95d924333e
 */

// Updated 14/10/24 (Higham, Ben), changes commented

using System.Collections;
using UnityEngine;

public class BombPowerUp : PowerUpBase 
{
    [SerializeField] 
    int collectedTrash = 0;

    public override IEnumerator Effect(float time)
    {
        if (_player == null)
        {
            yield return null;
        }

        EndEffect();
    }

    //Changed from OnBombExplode to make use of inherited functions, the outcome is the same. (BH)
    //Current implementation does nothing with the collectedTrash integer. 
    protected override void EndEffect()
    {
        if (GameManager.GetLitterManager() == null)
        {
            Debug.LogError("LitterManager does not exist.");
        }
        else
        {
            PlayerScript player = GameManager.GetPlayerScript();

            foreach (Litter litter in GameManager.GetLitterManager()._worldLitter)
            {
                player.AddLitter(litter);
            }

            GameManager.GetLitterManager().ClearLitter();

            /*collectedTrash = GameManager.GetLitterManager().Count();
            GameManager.GetLitterManager().ClearLitter();

            //We pass the amount of collected litter to the function so it can calculate and update the player’s current litter count.(HS)
            GameManager.GetPlayerScript().CalculateCollectedLitter(true, collectedTrash);*/

            Debug.Log("Trash Collected by bomb:" + collectedTrash);
        }

        base.EndEffect();
    }
}

/* void OnBombExplode()
   {
      if (GameManager.instance.LitterInstantiated != null)
      {
         int listAmount;
         listAmount = GameManager.instance.LitterInstantiated.Count;
         for (int i = 0; i < listAmount; i++)
         {
            if (GameManager.instance.LitterInstantiated[i] != null)
            {
               Destroy(GameManager.instance.LitterInstantiated[i].gameObject);
               collectedTrash++;
            }
         }
         GameManager.instance.LitterInstantiated.Clear();
      }
   } */