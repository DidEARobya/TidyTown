using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Direction_Sensor : Base_Sensor
{
    private List<string> _tagSet;
    private RaycastHit hit;

    public void InitTagSetSensor(float sensorStrength, float sensorDelay, List<string> tags, Action<bool> toggleFunc)
    {
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;
        _tagSet = tags;
        _sensorType = SensorType.TAGSET;

        toggleCallback = toggleFunc;
    }
    void FixedUpdate()
    {
        if(toggleCallback == null)
        {
            return;
        }

        _sensorTimer += Time.fixedDeltaTime;

        if (_sensorTimer >= _sensorDelay)
        {
            switch (_sensorType)
            {
                case SensorType.TAG:
                    ScanForTag();
                    break;
                case SensorType.TAGSET:
                    ScanForTagSet();
                    break;
                case SensorType.TARGET:
                    ScanForTarget();
                    break;
            }

            _sensorTimer = 0;
        }
    }

    private void ScanForTag()
    {
        //Debug.DrawRay(transform.position, transform.forward * _sensorStrength, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, _sensorStrength, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.tag == _tag)
            {
                toggleCallback(true);
                return;
            }
        }

        toggleCallback(false);
    }
    private void ScanForTagSet()
    {
       //Debug.DrawRay(transform.position, transform.forward * _sensorStrength, Color.green);

        if (Physics.Raycast(transform.position, transform.forward , out hit, _sensorStrength, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (_tagSet.Contains(hit.collider.tag) == true)
            {
                if (hit.collider.tag != "Traffic Light")
                {
                    toggleCallback(true);
                    return;
                }

                if (hit.collider.gameObject.GetComponentInParent<TrafficLight>().IsGreen == false)
                {
                    toggleCallback(true);
                    return;
                }
            }
        }

        toggleCallback(false);
    }
    private void ScanForTarget()
    {
        if(_target == null)
        {
            Debug.LogError("Target is null");
            return;
        }
        //Debug.DrawRay(transform.position, transform.forward * _sensorStrength, Color.white);

        if (Physics.Raycast(transform.position, transform.forward, out hit, _sensorStrength, _layerMask, QueryTriggerInteraction.Collide))
        {
            if(hit.collider.gameObject == _target)
            {
                toggleCallback(true);
                return;
            }
        }

        toggleCallback(false);
    }

    public RaycastHit GetHitData()
    {
        return hit;
    }
}
