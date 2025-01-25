using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Car : Agent_Base
{
    [Header("Traffic Sensors")]
    [SerializeField]
    private Direction_Sensor _trafficSensor;
    [SerializeField]
    private Direction_Sensor _frTrafficSensor;
    [SerializeField]
    private Direction_Sensor _flTrafficSensor;
    [SerializeField]
    private Direction_Sensor _fraTrafficSensor;
    [SerializeField]
    private Direction_Sensor _flaTrafficSensor;
    [SerializeField]
    private List<string> _trafficTags;
    [SerializeField]
    private float _trafficSensorStrength;

    [Header("Player/Character Sensor")]//Just Player Currently
    [SerializeField]
    private Direction_Sensor _fPlayerSensor;
    [SerializeField]
    private Direction_Sensor _frPlayerSensor;
    [SerializeField]
    private Direction_Sensor _flPlayerSensor;

    [SerializeField]
    private Direction_Sensor _reversePlayerSensor;
    [SerializeField]
    private Direction_Sensor _reversePlayerSensor2;
    [SerializeField]
    private string _playerTag;
    [SerializeField]
    private float _playerSensorStrength;

    [Header("Waypoint Sensor")]
    [SerializeField]
    private Direction_Sensor _waypointSensor;
    [SerializeField]
    private float _waypointSensorStrength;

    [Header("Parking Sensor")]
    [SerializeField]
    private Direction_Sensor _parkingSensor;
    [SerializeField]
    private string _parkingSpaceTag;
    [SerializeField]
    private float _parkingSensorStrength;

    [Header("Reverse Sensor")]
    [SerializeField]
    private Direction_Sensor _reverseSensor;
    [SerializeField]
    private Direction_Sensor _reverseSensor2;
    [SerializeField]
    private float _reverseSensorStrength;

    [SerializeField]
    private AI_SpawnPoint customerSpawnpoint;

    private AI_WaypointPath _path;
    private AI_Waypoint _currentWaypoint;
    private AI_Waypoint _queuedWaypoint;
    private ParkingSpace _parkingSpace;

    private bool _fStop;
    private bool _frStop;
    private bool _flStop;
    private bool _fraStop;
    private bool _flaStop;

    private bool _fPlayerStop;
    private bool _flPlayerStop;
    private bool _frPlayerStop;

    private bool _isAtWaypoint;
    private bool _isParking;
    private bool _isLeaving;
    private bool _isReversing;

    private Collider _obstacleCollider;
    public bool HasPath => seeker.HasPath;

    private new void Start()
    {
        base.Start();

        type = AIType.CAR;

        _obstacleCollider = gameObject.GetComponent<BoxCollider>();

        customerSpawnpoint.isActive = false;
        customerSpawnpoint.isUsed = false;

        _currentWaypoint = GetNextWaypoint();

        _trafficSensor.InitTagSetSensor(_trafficSensorStrength, 0.1f, _trafficTags, FStop);
        _frTrafficSensor.InitTagSetSensor(_trafficSensorStrength, 0.1f, _trafficTags, FRStop);
        _flTrafficSensor.InitTagSetSensor(_trafficSensorStrength, 0.1f, _trafficTags, FLStop);
        _fraTrafficSensor.InitTagSetSensor(_trafficSensorStrength, 0.1f, _trafficTags, FRAStop);
        _flaTrafficSensor.InitTagSetSensor(_trafficSensorStrength, 0.1f, _trafficTags, FLAStop);
        _waypointSensor.InitTargetSensor(_waypointSensorStrength, 0.1f, _currentWaypoint.gameObject, HasReachedWaypoint);
        _parkingSensor.InitTagSensor(_parkingSensorStrength, 0.1f, _parkingSpaceTag, IsParking);
        _fPlayerSensor.InitTagSensor(_playerSensorStrength, 0.1f, _playerTag, FPlayerStop);
        _flPlayerSensor.InitTagSensor(_playerSensorStrength, 0.1f, _playerTag, FLPlayerStop);
        _frPlayerSensor.InitTagSensor(_playerSensorStrength, 0.1f, _playerTag, FRPlayerStop);

        _reversePlayerSensor.InitTagSensor(_playerSensorStrength, 0.1f, _playerTag, FPlayerStop);
        _reversePlayerSensor.enabled = false;
        _reversePlayerSensor2.InitTagSensor(_playerSensorStrength, 0.1f, _playerTag, FPlayerStop);
        _reversePlayerSensor2.enabled = false;

        _reverseSensor.InitTagSetSensor(_reverseSensorStrength, 0.1f, _trafficTags, FStop);
        _reverseSensor.enabled = false;
        _reverseSensor2.InitTagSetSensor(_reverseSensorStrength, 0.1f, _trafficTags, FStop);
        _reverseSensor2.enabled = false;

        seeker.SetSpeed(5f);
        seeker.SetPath(_currentWaypoint.transform.position);

        KeyValuePair<Mesh, Material> pair = GameManager.GetReferenceManager().GetRandomCarModel();

        if(pair.Key == null || pair.Value == null)
        {
            return;
        }

        MeshFilter filter = GetComponent<MeshFilter>();

        if(filter == null)
        {
            return;
        }

        filter.mesh = pair.Key;

        MeshRenderer renderer = GetComponent<MeshRenderer>();

        if(renderer != null)
        {
            renderer.material = pair.Value;
        }
    }
    public void InitPath(PathPlan path)
    {
        _path = new AI_WaypointPath(path.GetPath());
    }
    private void FixedUpdate()
    {
        ToggleObstacle();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (_isLeaving == true)
        {
            if (_isAtWaypoint == true)
            {
                UpdateWaypoint();
                return;
            }

            if (_isReversing == true && seeker.HasPath == false)
            {
                ToggleParkingSensors(false);

                _parkingSpace.Reset();

                seeker.SetSpeed(5f);
                seeker.SetPath(_currentWaypoint.transform.position);
                _isReversing = false;
            }

            return;
        }

        if (_isParking == true)
        {
            if(_parkingSpace == null)
            {
                InitParkingSpace();

                return;
            }
        }

        if(_parkingSpace != null)
        {
            if(_parkingSpace.OwnerIsParked == true)
            {
                ToggleParkingSensors(true);
                customerSpawnpoint.isActive = true;
                return;
            }

            seeker.SetPath(_parkingSpace.GetGuidePosition());
            return;
        }

        if (_isAtWaypoint == true)
        {
            UpdateWaypoint();
        }
    }

    private void ToggleParkingSensors(bool val)
    {
        _reverseSensor.enabled = val;
        _reverseSensor2.enabled = val;
        _reversePlayerSensor.enabled = val;
        _reversePlayerSensor2.enabled = val;

        _parkingSensor.enabled = !val;
        _trafficSensor.enabled = !val;
        _frTrafficSensor.enabled = !val;
        _fraTrafficSensor.enabled = !val;
        _flTrafficSensor.enabled = !val;
        _flaTrafficSensor.enabled = !val;
        _fPlayerSensor.enabled = !val;
        _flPlayerSensor.enabled = !val;
        _frPlayerSensor.enabled = !val;
        _waypointSensor.enabled = !val;
    }
    private void UpdateWaypoint()
    {
        _isAtWaypoint = false;

        AI_Waypoint next = GetNextWaypoint();

        if(next == null)
        {
            base.Destroy();
            return;
        }

        _currentWaypoint = next;
        _waypointSensor.InitTargetSensor(_waypointSensorStrength, 0.1f, _currentWaypoint.gameObject, HasReachedWaypoint);

        seeker.SetPath(_currentWaypoint.transform.position);
    }

    private AI_Waypoint GetNextWaypoint()
    {
        AI_Waypoint next;

        if (_queuedWaypoint != null)
        {
            next = _queuedWaypoint;
            _queuedWaypoint = null;

            return next;
        }

        next = _path.GetNextWaypoint();

        if (next == null)
        {
            return null;
        }

        if (next.IsSwitch == true)
        {
            List<AI_Waypoint> newPath = next.gameObject.GetComponent<AI_SwitchWaypoint>().GetNextPlan();

            if (newPath != null)
            {
                _path.SetNewPath(newPath);
                _queuedWaypoint = _path.GetNextWaypoint();
            }
        }

        return next;
    }
    private void InitParkingSpace()
    {
        _parkingSpace = _parkingSensor.GetHitData().collider.gameObject.GetComponent<ParkingSpace>();

        if(_parkingSpace == null || _parkingSpace.IsOwned == true)
        {
            _parkingSpace = null;
            _isParking = false;
            return;
        }

        seeker.SetSpeed(3f);
        seeker.EndPath();

        _parkingSpace.SetOwner(this);
    }
    public void LeaveCarPark()
    {
        seeker.SetSpeed(2f);
        seeker.Reverse(_parkingSpace.GetGuidePosition());

        _isLeaving = true;
        _isReversing = true;
    }
    public void SetIsObstacle(bool val)
    {
        if (_obstacleCollider.isTrigger == val)
        {
            return;
        }

        _obstacleCollider.isTrigger = val;
    }
    public void FStop(bool val)
    {
        if (val == _fStop)
        {
            return;
        }

        _fStop = val;
        NeedsToStop();
    }
    public void FRStop(bool val)
    {
        if (val == _frStop)
        {
            return;
        }

        _frStop = val;
        NeedsToStop();
    }
    public void FLStop(bool val)
    {
        if(val == _flStop)
        {
            return;
        }

        _flStop = val;
        NeedsToStop();
    }
    public void FRAStop(bool val)
    {
        if (val == _fraStop)
        {
            return;
        }

        _fraStop = val;
        NeedsToStop();
    }
    public void FLAStop(bool val)
    {
        if (val == _flaStop)
        {
            return;
        }

        _flaStop = val;
        NeedsToStop();
    }
    public void FPlayerStop(bool val)
    {
        if (val == _fPlayerStop)
        {
            return;
        }

        _fPlayerStop = val;
        NeedsToStop();
    }
    public void FLPlayerStop(bool val)
    {
        if (val == _flPlayerStop)
        {
            return;
        }

        _flPlayerStop = val;
        NeedsToStop();
    }
    public void FRPlayerStop(bool val)
    {
        if (val == _frPlayerStop)
        {
            return;
        }

        _frPlayerStop = val;
        NeedsToStop();
    }
    public void NeedsToStop()
    {
        if(_fStop == true || _frStop == true || _flStop == true || _fraStop == true || _flaStop == true || _fPlayerStop == true || _flPlayerStop == true || _frPlayerStop == true)
        {
            seeker.ToggleStop(true);
            return;
        }

        seeker.ToggleStop(false);
    }
    public void HasReachedWaypoint(bool val)
    {
        _isAtWaypoint = val;
    }
    public void IsParking(bool val)
    {
        _isParking = val;
    }
    void ToggleObstacle()
    {
        if(seeker.Agent.velocity.magnitude <= 0.15f)
        {
            SetIsObstacle(false);
            return;
        }

        SetIsObstacle(true);
    }
}
