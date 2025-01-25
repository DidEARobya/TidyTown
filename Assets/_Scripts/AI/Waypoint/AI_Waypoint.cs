using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Waypoint : MonoBehaviour
{
    protected bool isSwitch = false;
    public bool IsSwitch => isSwitch;


    [SerializeField]
    protected bool isFinalWaypoint = false;
    public bool IsFinalWaypoint => isFinalWaypoint;

    public void SetIsFinalWaypoint(bool val)
    {
        isFinalWaypoint = val;
    }
}
