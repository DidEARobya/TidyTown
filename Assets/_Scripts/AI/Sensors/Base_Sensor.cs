using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Sensor : MonoBehaviour
{
    protected enum SensorType
    {
        TAG,
        TARGET,
        TAGSET,
        TAGINFRONT
    }

    protected GameObject _target;

    protected float _sensorStrength;
    protected float _sensorDelay;
    protected float _sensorTimer = 0;

    protected LayerMask _layerMask;
    protected string _tag;

    protected SensorType _sensorType;

    public Action<bool> toggleCallback;

    private void Start()
    {
        _layerMask = GameManager.GetReferenceManager().GetLayerMask(referenceLayers.AI);
    }
    public void InitTagSensor(float sensorStrength, float sensorDelay, string tag, Action<bool> toggleFunc)
    {
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;
        _tag = tag;
        _sensorType = SensorType.TAG;

        toggleCallback = toggleFunc;
    }
    public void InitTargetSensor(float sensorStrength, float sensorDelay, GameObject target, Action<bool> toggleFunc)
    {
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;

        _target = target;
        _sensorType = SensorType.TARGET;

        toggleCallback = toggleFunc;
    }
}
