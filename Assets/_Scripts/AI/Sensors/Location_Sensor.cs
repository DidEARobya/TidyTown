using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Location_Sensor : Base_Sensor
{
    public List<GameObject> _hitData;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggleCallback == null)
        {
            return;
        }

        _sensorTimer += Time.fixedDeltaTime;

        if(_sensorTimer >= _sensorDelay)
        {
            switch(_sensorType)
            {
                case SensorType.TARGET:
                    ScanForLocation();
                    break;
                case SensorType.TAG:
                    ScanForTag();
                    break;
                case SensorType.TAGINFRONT:
                    ScanForTagInFront();
                    break;
            }

            _sensorTimer = 0;
        }
    }

    void ScanForTag()
    {
        Collider[] hitData = Physics.OverlapSphere(transform.position, _sensorStrength, _layerMask);
        _hitData = new List<GameObject>();

        if(hitData.Length == 0)
        {
            toggleCallback(false);
            return;
        }

        bool isConfirmedTrue = false;

        foreach(Collider collider in hitData)
        {
            if(collider.gameObject.tag != _tag)
            {
                continue;
            }

            isConfirmedTrue = true;
            _hitData.Add(collider.gameObject);
            toggleCallback(true);
        }

        if(isConfirmedTrue == false)
        {
            toggleCallback(false);
        }
    }

    void ScanForLocation()
    {
        Vector3 distance = transform.position - _target.transform.position;

        if (distance.magnitude <= _sensorStrength)
        {
            toggleCallback(true);
            return;
        }

        toggleCallback(false);
    }
    void ScanForTagInFront()
    {
        Collider[] hitData = Physics.OverlapSphere(transform.position, _sensorStrength, _layerMask);

        Debug.DrawRay(gameObject.transform.position, transform.forward * 10, Color.blue);

        if (hitData.Length == 0)
        {
            toggleCallback(false);
            return;
        }

        bool isConfirmedTrue = false;
        _hitData = new List<GameObject>();

        foreach (Collider collider in hitData)
        {
            if (collider.gameObject.tag != _tag)
            {
                continue;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toCollider = collider.transform.position - transform.position;

            if( toCollider.magnitude < 10f)
            {
                continue;
            }

            toCollider = toCollider.normalized;

            if (Vector3.Dot(forward, toCollider) < 0)
            {
                continue;
            }

            _hitData.Add(collider.gameObject);
            isConfirmedTrue = true;
            toggleCallback(true);
        }

        if (isConfirmedTrue == false)
        {
            toggleCallback(false);
        }
    }
}
