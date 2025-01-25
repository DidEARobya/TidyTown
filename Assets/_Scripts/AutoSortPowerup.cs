using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSortPowerup : PowerUpBase
{
   [SerializeField] private List<Bin> _bins = new List<Bin>();
   public override IEnumerator Effect(float time)
   {
      if (_player == null)
      {
         yield return null;
      }

      SortBinsInList();
      EndEffect();
   }

   //in the game environment, an object named BinParent has been added, and a tag (Bins) has been assigned to it.
   //This allows us to identify the trash bins in the environment and keep them in a list for reference.(HS)
   private void SortBinsInList()
   {
      GameObject BinsParent = GameObject.FindWithTag("Bins");
      for (int i = 0; i < BinsParent.transform.childCount ; i++)
      {
         _bins.Add(BinsParent.transform.GetChild(i).GetComponent<Bin>());
      }
      
   }

   //In this function, after colliding with the auto-sort power-up,
   //all the trash collected by the player is allocated to its corresponding trash bin.(HS)
   private void LitterAllocator()
   {
      for (int i = 0; i < _bins.Count; i++)
      {
         _bins[i].SetStoredBlackLitter(_player.HeldBlackLitter);
         _bins[i].SetStoredBeigeLitter(_player.HeldBeigeLitter);
         _bins[i].SetStoredRedLitter(_player.HeldRedLitter);
      }
      
      //The player’s fields are reset here.(HS)
      _player.HeldBeigeLitter_Setter(0);
      _player.HeldRedLitter_Setter(0);
      _player.HeldBlackLitter_Setter(0);

   }

   protected override void EndEffect()
   {
      print("Auto Sort");
      LitterAllocator();
      base.EndEffect();
   }
}
