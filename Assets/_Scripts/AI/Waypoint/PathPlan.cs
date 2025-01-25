using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlan : MonoBehaviour
{
    [SerializeField]
    private List<AI_Waypoint> _path;

    public List<AI_Waypoint> GetPath()
    {
        return _path;
    }
}
