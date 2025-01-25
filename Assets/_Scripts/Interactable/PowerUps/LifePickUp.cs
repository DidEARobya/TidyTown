using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Updated 14/10/24 (Higham, Ben), changes commented

public class LifePickUp : PowerUpBase
{
    //Unneccessary boolean
    //private bool _hasExecuted = false;
    public override IEnumerator Effect(float time)
    {
        if (_player == null)
        {
            yield return null;
        }

        EndEffect();
    }

    //Changed from AddLife to make use of inherited functions, the outcome is the same. (BH)
    protected override void EndEffect()
    {
        GameManager.GetPlayerScript().CalculatePlayerLife();
        base.EndEffect();
    }
}
/*     public void AddLife()
    {
        if (GameManager.instance.LitterInstantiated != null && !_hasExecuted)
        {
            GameManager.instance.PlayerLife += 1;
            UiManager.instance.LifeAmountText.text = GameManager.instance.PlayerLife + "";
            Destroy(gameObject);
            _hasExecuted = true;
        }
    } */