/*
 * Branch: BenH (Higham, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 *
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */

using System.Collections;
using UnityEngine;

// Example Powerup
public class SpeedPowerUp : PowerUpBase
{
    [SerializeField] 
    private float speedMultiplier;

    public override IEnumerator Effect(float time)
    {
        if(_player == null)
        {
            yield return null;
        }

        StartEffect();
        yield return new WaitForSeconds(time);
        EndEffect();
    }

    protected override void StartEffect()
    {
        base.StartEffect();
        _player.MultiplyBaseSpeed(speedMultiplier);
    }

    protected override void EndEffect()
    {
        _player.ResetMovementSpeed();
        base.EndEffect();
    }
}
