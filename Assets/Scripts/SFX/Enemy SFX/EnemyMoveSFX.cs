using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSFX : MonoBehaviour
{
    public EnemySFX _enemySFX;
    public StateMachine stateMachine;

    private void Start()
    {
        // Busca el StateMachine en los padres del GameObject actual
        stateMachine = GetComponentInParent<StateMachine>();

        if (stateMachine == null)
        {
            Debug.LogError("StateMachine no encontrado en los padres del GameObject " + gameObject.name);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enabled)
        {
            if (stateMachine == null)
            {
                Debug.LogError("StateMachine es null al intentar acceder en OnTriggerEnter");
                return;
            }

            if (stateMachine._currentState == stateMachine._patrolState)
            {
                _enemySFX.PlaySoundEnemyWalk();
                _enemySFX.PlaySoundBreathPatrol();
            }
            else if (stateMachine._currentState == stateMachine._chaseState ||
                     stateMachine._currentState == stateMachine._attackState)
            {
                _enemySFX.PlaySoundEnemyRun();
                _enemySFX.PlaySoundBreathChase();

                if (stateMachine._currentState == stateMachine._attackState)
                {
                    _enemySFX.PlaySoundAttack();
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _enemySFX.StopAudioSource1();
        _enemySFX.StopAudioSource2();
        _enemySFX.StopAudioSource3();
    }
}