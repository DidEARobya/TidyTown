/*
 * Branch: Hossein (Soroor, Hossein)
 * Commit: 04540918794770097c705bb200dad0a67b0791e9
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: b19f37c3e27817dba491819664ea3e95d924333e
 */

// Updated 14/10/24 (Higham, Ben), changes commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerup : PowerUpBase
{
    [SerializeField] 
    private float _speed;   //litters move to player magnet with this speed
    [SerializeField] 
    private float _distance;    //The farthest distance at which a magnet affects a piece of litter.

    // Unneccessary as the base class has an effect duration (BH)
    //[SerializeField] private float _magnetDuration;

    // Updated to utilise the new LitterManager class. (BH)
    // Previous implementation did not update the used list for any newly spawned/destroyed litter objects. (BH)
    // Previous implementation also destroyed the litter when that is already handled via collision with the player. (BH)
    public override IEnumerator Effect(float time)
    {
        base.StartEffect();

        print("Magnet Activated");

        float timer = time;

        while (timer > 0)
        {
            foreach (Litter litter in GameManager.GetLitterManager().GetWorldLitter())
            {
                if (litter == null)
                {
                    Debug.LogError("WorldLitter contains null element.");
                    continue;
                }

                if (Vector3.Distance(_player.transform.position, litter.transform.position) <= _distance)
                {
                    print("Magnet Attraction");

                    litter.transform.position = Vector3.MoveTowards(litter.transform.position, _player.transform.position, _speed * Time.deltaTime);
                }
            }

            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }

        print("Magnet Deactivated");
        base.EndEffect();
    }
}
/*     public override IEnumerator Effect(float time)
    {
        print("Magnet Activated");

        var litterObjcts = GameManager.instance.LitterInstantiated;
        float timer = _magnetDuration;

        while (timer > 0)       //The purpose of this loop is to create a countdown timer.
        {
            for (int i = 0; i < litterObjcts.Count; i++)    //Here, the litter that was identified is measured in terms of distance from the player, and if it falls within a specified range, attraction occurs.
            {
                if (GameManager.instance.LitterInstantiated[i] != null)
                {
                    if (Vector3.Distance(_player.transform.position, litterObjcts[i].transform.position) <= _distance)
                    {
                        print("Magnet Attraction");

                        litterObjcts[i].transform.position = Vector3.MoveTowards(litterObjcts[i].transform.position, _player.transform.position,
                            _speed * Time.deltaTime);

                        if (litterObjcts[i].transform.position == _player.transform.position)
                        {
                            Destroy(litterObjcts[i].gameObject);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1f);
            timer--;
        }

        print("Magnet Deactivated");
    } */
