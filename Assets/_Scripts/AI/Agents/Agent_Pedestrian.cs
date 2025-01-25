using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class Agent_Pedestrian : Agent_Base
{
    [Header("Linger Variables")]
    [SerializeField]
    private int lingerTimeMin;
    [SerializeField]
    private int lingerTimeMax;
    [SerializeField]
    private int delayBetweenLinger;

    private Location_Sensor _waypointSensor;
    private Location_Sensor _streetCornerSensor;
    private Location_Sensor _crossingSensor;

    [Header("Sensor Variables")]
    [SerializeField]
    private int streetCornerSensorDelay;
    [SerializeField]
    private string streetCornerSensorTag;
    [SerializeField]
    private int _waypointSensorStrength;
    [SerializeField]
    private string waypointTag;
    [SerializeField]
    private int _crossingSensorStrength;
    [SerializeField]
    private string _crossingSensorTag;
    [SerializeField]
    private Animator _pedestrianAnimator;
    
    private float _lingerTimer = 0;
    private float _lingerDelayTimer = 0;

    public bool isInStreetCorner = false;
    public bool isCrossing = false;

    private float _crossingCooldown;

    private CrossingPoint _crossingPoint;
    private List<GameObject> _waypoints;
    private AI_Waypoint currentWaypoint;

    private new void Start()
    {
        base.Start();

        type = AIType.PEDESTRIAN;

        _streetCornerSensor = gameObject.AddComponent<Location_Sensor>();
        _streetCornerSensor.InitTagSensor(10, 1, streetCornerSensorTag, SetStreetCornerBool);

        _waypointSensor = gameObject.AddComponent<Location_Sensor>();
        _waypointSensor.InitTagSensor(_waypointSensorStrength, 1f, waypointTag, UpdateWaypoints);

        _crossingSensor = gameObject.AddComponent<Location_Sensor>();
        _crossingSensor.InitTagSensor(_crossingSensorStrength, 0.1f, _crossingSensorTag, SetIsCrossing);

        _lingerDelayTimer = delayBetweenLinger;
        _crossingCooldown = 5;
    }

    private void LateUpdate()
    {
        _crossingCooldown += Time.deltaTime;

        if (seeker.HasPath == true && seeker.DistanceRemaining > 1f)
        {
            Moving();

            return;
        }

        if(_lingerDelayTimer > 0)
        {
            _lingerDelayTimer -= Time.deltaTime;

            if (isInStreetCorner == true && _lingerTimer <= 0)
            {
                StartIdle();
                _lingerTimer = Random.Range(lingerTimeMin, lingerTimeMax);
            }
        }

        if(_lingerTimer > 0)
        {
            _lingerTimer -= Time.deltaTime;

            if(_lingerTimer <= 0)
            {
                EndIdle();
                EndLinger();
            }

            return;
        }

        SetRandomWaypoint();
    }
    private void Moving()
    {
        if (isCrossing == true && _crossingPoint != null)
        {
            if (_crossingPoint.CanCross == false && _crossingCooldown > 5.0f)
            {
                transform.LookAt(_crossingPoint.GetDirectionToFace());

                StartIdle();
                seeker.ToggleStop(true);
                return;
            }

            if (_crossingPoint.CanCross == true)
            {
                EndIdle();
                _crossingCooldown = 0;
                seeker.ToggleStop(false);
            }
        }
    }
    private void StartIdle()
    {
        _pedestrianAnimator.SetBool("isSteady",true);
    }
    private void EndIdle()
    {
        _pedestrianAnimator.SetBool("isSteady",false);
    }
    private void EndLinger()
    {
        litterDropper.DropLitter();
        _lingerDelayTimer = delayBetweenLinger;
    }

    private void SetRandomWaypoint()
    {
        _crossingPoint = null;

        if(_waypoints == null)
        {
            return;
        }

        if (currentWaypoint != null && currentWaypoint.IsFinalWaypoint)
        {
            base.Destroy();
            return;
        }


        if (_waypoints.Count == 0)
        {
            Debug.LogError("No Waypoints");
            transform.Rotate(new Vector3(0, Random.Range(-10f, 10f), 0));
            return;
        }

        AI_Waypoint next = _waypoints[Random.Range(0, _waypoints.Count)].GetComponent<AI_Waypoint>();

        if(next == null)
        {
            //Debug.LogError(next.gameObject.name + " Waypoint component is null");
            return;
        }

        currentWaypoint = next;
        seeker.SetPath(currentWaypoint.transform.position);
    }
    public void SetStreetCornerBool(bool val)
    {
        isInStreetCorner = val;
    }
    public void SetIsCrossing(bool val)
    {
        if(isCrossing == val)
        {
            return;
        }

        if(val == true)
        {
            if(_crossingSensor._hitData.Count == 0)
            {
                Debug.LogError("Crossing, but crossing point not stored");
            }
            else
            {
                _crossingPoint = _crossingSensor._hitData[0].GetComponent<CrossingPoint>();
            }
        }

        isCrossing = val;
    }
    public void UpdateWaypoints(bool val)
    {
        if (val == false)
        {
            //Debug.LogError("No available waypoints");
            return;
        }
        //Debug.LogError("Waypoints");
        _waypoints = _waypointSensor._hitData;
    }
}
