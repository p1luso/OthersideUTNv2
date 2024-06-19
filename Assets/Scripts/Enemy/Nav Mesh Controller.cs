using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    [HideInInspector]
    public Transform _currentTarget;
    private NavMeshAgent _navMeshAgent;
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void SetDestinationPlayer(float minDistance)
    {
        Vector3 directionToPlayer = (_currentTarget.position - transform.position).normalized;
    Vector3 targetPosition = _currentTarget.position - directionToPlayer * minDistance;

    _navMeshAgent.SetDestination(targetPosition);
    _navMeshAgent.isStopped = false;
    }
    public void SetDestination(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
        _navMeshAgent.isStopped = false;
    }

    public void Stop()
    {
        _navMeshAgent.isStopped = true;
    }

    public bool IsDestinationReached()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool HasReachedLastKnownPlayerPosition(Vector3 lastKnownPlayerPosition)
{
    _navMeshAgent.SetDestination(lastKnownPlayerPosition);
    float stoppingDistance = _navMeshAgent.stoppingDistance; // Aumenta la distancia de parada en 1.0f
    if (!_navMeshAgent.pathPending)
    {
        if (_navMeshAgent.remainingDistance <= stoppingDistance)
        {
            if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
    }
    return false;
}
}
