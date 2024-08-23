using System.Collections;
using UnityEngine;

public class SoundCollider : MonoBehaviour
{
    public StateMachine stateMachine;
    public Movement _playerMove;
    private float counter;
    private bool isPlayerDetected;

    void Start()
    {
        _playerMove = GameObject.Find("Player").GetComponent<Movement>();
        counter = 0;
        isPlayerDetected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stateMachine._currentState == stateMachine._patrolState)
        {
            if (_playerMove._isMoving && !_playerMove.crouch)
            {
                stateMachine.ChangeState(stateMachine._alertState);
                isPlayerDetected = true;
                counter = 0;
                StartCoroutine(CheckPlayerLost());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }

    private IEnumerator CheckPlayerLost()
    {
        while (isPlayerDetected)
        {
            counter += Time.deltaTime;

            if (counter >= 3f)
            {
                stateMachine.ChangeState(stateMachine.lastState);
                isPlayerDetected = false;
                yield break;
            }

            yield return null;
        }

        // Reset counter if player is detected again within 3 seconds
        counter = 0;
    }

    void Update()
    {
        // In case the player is in the collider but not detected
        if (isPlayerDetected && counter < 3f)
        {
            counter += Time.deltaTime;
        }
    }
}