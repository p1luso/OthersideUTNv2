using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Color _stateColor;
    protected StateMachine _stateMachine;
    protected NavMeshController _navMeshController;
    protected SFX _sfx;
    protected Animator _animator;
    protected VisionController _visionController;
    public float _chaseSpeed = 15f;  // Velocidad durante la persecución
    public float _normalSpeed = 5.5f;  // Velocidad normal del agente
    public float _chaseDuration = 2f;  // Duración de la persecución en segundos
    protected float _chaseTimer;
    protected bool _distanceAttack;
    public float _attackRange = 15f;
    protected Vector3 _playerPosition;
    protected Vector3 _enemyPosition;
    protected float _distance;
    protected bool _attack = false;
    protected Vector3 _lastKnownPlayerPosition;
    public float _searchChaseTime = 4f;
    public float _timeSearchingChase;
    protected float _timeSearching;
    public float _turnSpeed = 250f;
    protected float _damage = 25f;
    protected float _attackCD = 1.5f;

    //public bool _pause = false;
    //public bool Pause { get {return _pause;} set {_pause = value;}}
    

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _navMeshController = GetComponent<NavMeshController>();
        _visionController = GetComponent<VisionController>();
        _animator = GetComponent<Animator>();
        _sfx = GetComponent<SFX>();
    }

    public enum EnemyAnimatorState
    {
        Walk,
        Run,
        Idle,
        Attack,
    }

    protected void SetAnimator(EnemyAnimatorState state)
    {
        _animator.SetBool("Walk", EnemyAnimatorState.Walk == state);
        _animator.SetBool("Run", EnemyAnimatorState.Run == state);
        _animator.SetBool("Idle", EnemyAnimatorState.Idle == state);
        _animator.SetBool("Attack", EnemyAnimatorState.Attack == state);
    }



}
