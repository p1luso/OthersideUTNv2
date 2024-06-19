using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : Enemy
{
    private NavMeshAgent _agent;
    public float _highAcceleration = 20f;
    public float _normalAcceleration = 8f;
    private Transform _playerTransform;
    private bool _isChasing = false;
    private float _timeChasing = 3f;
    private float _timeChasingCounter = 0f;


    protected override void Awake()
    {
        base.Awake();
        _chaseTimer = 0f;
        _agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    void OnEnable()
    {
        _chaseTimer = 0;
        _timeChasingCounter = 0;
        _agent.speed = _chaseSpeed;
        _agent.acceleration = _highAcceleration;
        SetAnimator(EnemyAnimatorState.Run);
        _stateColor = Color.red;
    }

    void Update()
    {
        //if(Pause) return;

        RaycastHit hit;
        if (_visionController.CanSeePlayer(out hit, true))
        {
            _timeChasingCounter = 0;
            _isChasing = true;
            _lastKnownPlayerPosition = _playerTransform.position;
        }
        else
        {
            _timeChasingCounter += Time.deltaTime;
            if (_timeChasingCounter > _timeChasing)
            {
                _isChasing = false;
            }

        }

        if (_isChasing)
        {
            _navMeshController._currentTarget = _playerTransform;
            _enemyPosition = _agent.transform.position;
            _distance = Vector3.Distance(_navMeshController._currentTarget.position, _enemyPosition);
            _navMeshController.SetDestinationPlayer(5f);

            _chaseTimer += Time.deltaTime;

            if (_chaseTimer >= _chaseDuration)
            {
                _agent.speed = _normalSpeed;
                SetAnimator(EnemyAnimatorState.Walk);
                _agent.acceleration = _normalAcceleration; // Asume que _normalAcceleration es una variable que has definido
            }

            if (_distance <= _attackRange)
            {
                _stateMachine.ChangeState(_stateMachine._attackState);
                return;
            }
        }
        else if (!_isChasing)
        {
            if (_navMeshController.HasReachedLastKnownPlayerPosition(_lastKnownPlayerPosition))
            {
                _stateMachine.ChangeState(_stateMachine._alertState);
                return;
            }
        }

    }

}
