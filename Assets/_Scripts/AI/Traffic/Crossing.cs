using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossing : MonoBehaviour
{
    [SerializeField]
    private List<CrossingPoint> _crossingPoints;

    [SerializeField]
    private List<TrafficLight> _trafficLights;
    [SerializeField]
    private float _timeToChange;

    public bool canCross;

    private int _lightIndex = 0;
    private float _timer = 0;

    private void Start()
    {
        if(_trafficLights.Count == 0)
        {
            Debug.LogError("Cross has no lights listed");
            return;
        }

        _trafficLights[_lightIndex].ToggleLight(true);

        foreach(CrossingPoint point in _crossingPoints)
        {
            point.SetCrossing(this);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _timeToChange)
        {
            ToggleLights();
            _timer = 0;
        }
    }

    private void ToggleLights()
    {
        if(_lightIndex != -1)
        {
            _trafficLights[_lightIndex].ToggleLight(false);
        }

        _lightIndex += 1;

        if (_lightIndex == _trafficLights.Count)
        {
            foreach(TrafficLight light in _trafficLights)
            {
                light.ToggleLight(false);
            }

            _lightIndex = -1;
            canCross = true;
            return;
        }

        canCross = false;

        _trafficLights[_lightIndex].ToggleLight(true);
    }
}
