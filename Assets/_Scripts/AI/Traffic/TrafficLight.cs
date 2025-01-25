using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool _isGreen;
    public bool IsGreen => _isGreen;

    public void ToggleLight(bool isGreen)
    {
        _isGreen = isGreen;
    }
}
