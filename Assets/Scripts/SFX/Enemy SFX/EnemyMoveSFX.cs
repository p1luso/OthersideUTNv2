using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSFX : MonoBehaviour
{
    public EnemySFX _enemySFX;
    public StateMachine stateMachine;
    public Movement _playerMove;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        _playerMove = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enabled)
        {
            _enemySFX.PlaySoundEnemyWalk();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _enemySFX.StopAudioSource3();
    }
}
