using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : Enemy
{
    public Transform[] _wayPoints;
    private int _nextWayPoint;
    private NavMeshAgent _agent;
    private bool isRunningSoundPlaying = false;
    private bool isWalkingSoundPlaying = false;
    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
        _stateColor = Color.green;
    }

    void Update()
    {
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        RaycastHit hit;
        if (_visionController.CanSeePlayer(out hit))
        {
            _navMeshController._currentTarget = hit.transform;
            _stateMachine.ChangeState(_stateMachine._chaseState);
            return;
        }
        if (_navMeshController.IsDestinationReached())
        {
            _nextWayPoint = (_nextWayPoint + 1) % _wayPoints.Length;
            UpdateWayPoints();
        }
    }

    void OnEnable()
    {
        SetAnimator(EnemyAnimatorState.Walk);
        UpdateWayPoints();
    }

    void UpdateWayPoints()
    {
        _navMeshController.SetDestination(_wayPoints[_nextWayPoint].position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enabled)
        {
            _agent.transform.LookAt(_playerPosition);
            _navMeshController._currentTarget = other.transform;
            _stateMachine.ChangeState(_stateMachine._chaseState);
        }
    }

}
