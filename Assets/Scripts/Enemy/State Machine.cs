using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Enemy _chaseState;
    public Enemy _patrolState;
    public Enemy _attackState;
    public Enemy _alertState;
    public Enemy _initialState;
    internal Enemy _currentState;
    public Enemy lastState;
    
    void Start()
    {
        _currentState = _initialState;
        ChangeState(_currentState);
    }


    public void ChangeState(Enemy newState)
    {
        _currentState.enabled = false;
        lastState = _currentState;
        _currentState = newState;
        _currentState.enabled = true;
    }

}
