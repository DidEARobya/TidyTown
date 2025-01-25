using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingPoint : MonoBehaviour
{
    protected Crossing _crossing;
    [SerializeField]
    private CrossingPoint _opposingPoint;
    public bool CanCross => _crossing.canCross;

    public void SetCrossing(Crossing crossing)
    {
        _crossing = crossing;
    }
    public Vector3 GetDirectionToFace()
    {
        if(_opposingPoint == null)
        {
            return new Vector3(_crossing.transform.position.x, 1, _crossing.transform.position.z);
        }

        return new Vector3(_opposingPoint.transform.position.x, 1, _opposingPoint.transform.position.z);
    }
}
