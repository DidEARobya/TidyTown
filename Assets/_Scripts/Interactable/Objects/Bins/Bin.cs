/*
 * Branch: Main (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */

using PrimeTween;
using System;
using UnityEngine;

public class Bin : MonoBehaviour, IInteractable
{
    public LitterType litterType;

    [SerializeField] private int _storedBlackLitter = 0;
    [SerializeField] private int _storedBeigeLitter = 0;
    [SerializeField] private int _storedRedLitter = 0;

    public bool HasALid;
    public Transform lid;
    public Transform litterPos;
    public TweenSettings<Vector3> lidRotaionSettings;

    public void AnimateRecycling(Transform litter)
    {
        if (HasALid)
        {
            Tween.LocalRotation(lid, lidRotaionSettings);
        }
        Tween.Position(litter, litterPos.position, 0.2f).OnComplete(target: this, target => litter.gameObject.SetActive(false));
    }

    public void OnInteract(PlayerScript player)
    {

        // increase stored amount by player holdage
        switch (litterType)
        {
            case LitterType.Beige:
                _storedBeigeLitter += player.HeldBeigeLitter;
                Debug.Log("Stored Litter: " + _storedBeigeLitter);
                break;
            case LitterType.Red:
                _storedRedLitter += player.HeldRedLitter;
                Debug.Log("Stored Litter: " + _storedRedLitter);
                break;
            case LitterType.Black:
                _storedBlackLitter += player.HeldBlackLitter;
                Debug.Log("Stored Litter: " + _storedBlackLitter);
                break;
            default:
                _storedBeigeLitter += player.HeldBeigeLitter;
                break;
        }
        player.ClearLitter(litterType);
    }


    public int GetStoredBlackLitter
    {
        get { return _storedBlackLitter; }
    }

    //Here, we could only retrieve the values held by the trash bins,
    //but we also need the ability to modify these values. Therefore, I added setters to enable this functionality.(HS)
    public void SetStoredBlackLitter(int value)
    {
        _storedBlackLitter = value;
    }
    public int GetStoredBeigeLitter
    {
        get { return _storedBeigeLitter; }
    }
    public void SetStoredBeigeLitter(int value)
    {
        _storedBeigeLitter = value;
    }
    public int GetStoredRedLitter
    {
        get { return _storedRedLitter; }
    }
    public void SetStoredRedLitter(int value)
    {
        _storedRedLitter = value;
    }
    /*
    public void OnInteract(PlayerScript player)
    {
        // Check if litter count is less than 10.
        if (_storedLitter >= 10)
        {
            return;
        }

        // increase stored amount by player holdage
        switch (litterType)
        {
            case LitterType.Beige:
                _storedLitter += player.HeldBeigeLitter;
                break;
            case LitterType.Red:
                _storedLitter += player.HeldRedLitter;
                break;
            case LitterType.Black:
                _storedLitter += player.HeldBlackLitter;
                break;
            default:
                _storedLitter += player.HeldBeigeLitter;
                break;
        }


        Debug.Log("Stored Litter: " + _storedLitter);

        // remove all litter of one type from plauyer
        player.ClearLitter(litterType);
        Debug.Log("CLEARED");
    }
    */
}
