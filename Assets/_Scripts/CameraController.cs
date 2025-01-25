using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 _offsetPos;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    public bool FollowPlayer = true;

    Vector3 originalPos;
    Vector3 originalRot;
    private void Awake()
    {
        _offsetPos = transform.position - target.position;
        originalRot = transform.localEulerAngles;

    }
    private void LateUpdate()
    {
        if (FollowPlayer)
        {
            Vector3 targetPosition = target.position + _offsetPos;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        }
    }

    public void ResetCamera()
    {
        FollowPlayer=true;
        transform.localEulerAngles=originalRot;
        //transform.position = originalPos;
    }



}
