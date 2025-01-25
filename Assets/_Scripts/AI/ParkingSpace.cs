using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpace : MonoBehaviour
{
    [SerializeField]
    private Transform parkingGuide;
    [SerializeField]
    private float guideSpeed;

    private Vector3 _directionToSpot;

    private Agent_Car _owner;
    private bool _isOwned;
    public bool IsOwned => _isOwned;
    private bool _ownerIsParked;
    public bool OwnerIsParked => _ownerIsParked;
    // Start is called before the first frame update
    void Start()
    {
        if(parkingGuide == null)
        {
            Debug.LogError(gameObject.name + " is missing parking guide");
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isOwned == false)
        {
            return;
        }

        if(_ownerIsParked == false)
        {
            if(parkingGuide.transform.localPosition.x < -2)
            {
                parkingGuide.transform.Translate(Vector3.right * Time.fixedDeltaTime * guideSpeed);
                return;
            }

            if (_owner.HasPath == false)
            {
                _ownerIsParked = true;
                parkingGuide.transform.localPosition += new Vector3(-6, 0, 0);
            }

            return;
        }
    }
    public Vector3 GetGuidePosition()
    {
        return parkingGuide.transform.position;
    }
    public void SetOwner(Agent_Car owner)
    {
        _owner = owner;
        _isOwned = true;
    }
    public void Reset()
    {
        _owner = null;
        _isOwned = false;
        _ownerIsParked = false;
    }
}
