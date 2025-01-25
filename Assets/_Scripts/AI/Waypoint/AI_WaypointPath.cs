using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AI_WaypointPath
{
    private List<AI_Waypoint> _path;
    private int _currentWaypointIndex = 0;

    public AI_WaypointPath(List<AI_Waypoint> path)
    {
        _path = path;

        if( _path.Count == 0)
        {
            Debug.LogError("Waypoint path is empty");
            return;
        }
    }

    public void SetNewPath(List<AI_Waypoint> path)
    {
        _currentWaypointIndex = 0;
        _path = path;
    }

    public AI_Waypoint GetNextWaypoint()
    {
        if(_currentWaypointIndex >= _path.Count)
        {
            return null;
        }

        int current = _currentWaypointIndex;
        _currentWaypointIndex += 1;

        return _path[current];
    }
}
