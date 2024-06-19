using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : Enemy
{

    private NavMeshAgent _agent;
    private Player _player;
    private float _attackDuration = 1.15f;
    private bool _isAttacking = false;
    private float _attackTimer = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
        _stateColor = Color.yellow;
    }
    void Update()
    {
        //if(Pause) return;

        _playerPosition = _navMeshController._currentTarget.position;
        _enemyPosition = _agent.transform.position;
        _distance = Vector3.Distance(_playerPosition, _enemyPosition);

        _attackTimer += Time.deltaTime;

        if (_attackTimer > _attackCD && _isAttacking == false)
        {
            _attackTimer = 0;
            // _sfx.PlaySoundAttack();
            SetAnimator(EnemyAnimatorState.Attack);
            _isAttacking = true;

        }
        else if (_attackTimer > _attackDuration && _isAttacking == true)
        {
            _attackTimer = 0;
            _player.TakeDamage(_damage);
            _isAttacking = false;
            SetAnimator(EnemyAnimatorState.Walk);
        }
        /* else
         {
             if (_agent.speed == _normalSpeed)
             {
                 SetAnimator(EnemyAnimatorState.Walk);
             }
             else if (_agent.speed == _chaseSpeed)
             {
                 SetAnimator(EnemyAnimatorState.Run);
             }
         }*/

        // Mover la lógica de cambio de estado aquí
        if (_distance > _attackRange)
        {
            _stateMachine.ChangeState(_stateMachine._chaseState);
        }
    }

}
