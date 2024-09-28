using System.Collections;
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

    private Vector3 _lastPosition;
    private float _positionCheckCounter = 0f;
    private float _positionCheckDuration = 3f;

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
        _enemySFX.PlaySoundEnemyRun();
        if (_stateMachine.lastState != _stateMachine._chaseState || _stateMachine.lastState != _stateMachine._attackState)
        {
            _enemySFX.PlaySoundBreathChase();
        }
        _chaseTimer = 0;
        _timeChasingCounter = 0;
        _agent.speed = _chaseSpeed;
        _agent.acceleration = _highAcceleration;
        SetAnimator(EnemyAnimatorState.Run);
        _stateColor = Color.red;

        _lastPosition = transform.position;
        _positionCheckCounter = 0f;
    }

    void Update()
    {
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
                _agent.acceleration = _normalAcceleration;
            }

            if (_distance <= _attackRange)
            {
                _stateMachine.ChangeState(_stateMachine._attackState);
                return;
            }

            CheckIfStuck();
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

    private void CheckIfStuck()
    {
        if (Vector3.Distance(transform.position, _lastPosition) < 0.2f)  // Ajusta la distancia m�nima
        {
            _positionCheckCounter += Time.deltaTime;
            if (_positionCheckCounter >= _positionCheckDuration)
            {
                _stateMachine.ChangeState(_stateMachine._alertState);
                return;
            }
        }
        else
        {
            _positionCheckCounter = 0f;
        }

        _lastPosition = transform.position;
    }
}
