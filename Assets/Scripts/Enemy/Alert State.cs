using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : Enemy
{
    public float _searchTime = 4f;
    protected override void Awake()
    {
        base.Awake();
        _stateColor = Color.blue;
    }

    void OnEnable()
    {
        SetAnimator(EnemyAnimatorState.Idle);
        _navMeshController.Stop();
        _timeSearching = 0;
    }

    void Update()
    {
        //if(Pause) return;

        RaycastHit hit;
        if (_visionController.CanSeePlayer(out hit))
        {
            _navMeshController._currentTarget = hit.transform;
            _stateMachine.ChangeState(_stateMachine._chaseState);
            return;
        }
        transform.Rotate(0f, _turnSpeed * Time.deltaTime, 0f);
        _timeSearching += Time.deltaTime;
        if (_timeSearching >= _searchTime)
        {
            _stateMachine.ChangeState(_stateMachine._patrolState);
            return;
        }
    }
}
