using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    public bool HasPath => _agent.hasPath;
    public float DistanceRemaining => _agent.remainingDistance;

    public void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetPath(Vector3 destination)
    {
        _agent.updateRotation = true;
        _agent.destination = destination;
    }
    public void Reverse(Vector3 destination)
    {
        _agent.updateRotation = false;
        _agent.destination = destination;
    }
    public void SetRandomPath(int searchRadius)
    {
        if(searchRadius <= 0)
        {
            Debug.LogError(gameObject.name + " has invalid path search radius");
            return;
        }

        Vector3 direction = Random.onUnitSphere * searchRadius;

        direction += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(direction, out hit, searchRadius, 1);

        _agent.destination = hit.position;
    }
    public void ToggleStop(bool toStop)
    {
        _agent.isStopped = toStop;
    }
    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }
    public void Seek(Transform target)
    {
        _agent.destination = target.position;
    }
    public void EndPath()
    {
        _agent.ResetPath();
    }
}
