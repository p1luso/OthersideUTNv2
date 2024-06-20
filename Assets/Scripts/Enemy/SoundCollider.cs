using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundCollider : MonoBehaviour
{
    public StateMachine stateMachine;
    public Movement _playerMove;

    void Start()
    {
        _playerMove = GameObject.Find("Player").GetComponent<Movement>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        float counter = 0;
        counter += Time.deltaTime;
        // Verificar si el objeto que entra en el Collider tiene la etiqueta "Enemy"
        if (other.CompareTag("Player"))
        {
            if (counter < 3f && stateMachine._currentState== stateMachine._patrolState && _playerMove._isMoving)
            {
                stateMachine.ChangeState(stateMachine._alertState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.lastState);
            }
        }
    }
}