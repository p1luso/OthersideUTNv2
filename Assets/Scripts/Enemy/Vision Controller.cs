using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public Transform _playerEyes;
    public Transform _playerCrouch;
    public Vector3 _offset = new Vector3(0f, 0.0f, 0f);
    private NavMeshController _navMeshController;
    public int _viewAngleL = -60;
    public int _viewAngleR = 60;
    [SerializeField] float _viewDistance;
    [SerializeField] float viewDistanceLightOn = 45f;
    [SerializeField] float viewDistanceLightOff = 20f;

    private Movement _playerMovement;
    [SerializeField] CandleLight candleLight;

    void Awake()
    {
        _navMeshController = GetComponent<NavMeshController>();
        _playerMovement = FindObjectOfType<Movement>();
        candleLight = FindObjectOfType<CandleLight>();
        
    }

    void Update()
    {
        if (candleLight != null)
        {
            if (candleLight.lightOn)
            {
                _viewDistance = viewDistanceLightOff;
            }
            else
            {
                _viewDistance = viewDistanceLightOn;
            }
        }
    }

    public bool CanSeePlayer(out RaycastHit hit, bool _lookForPlayer = false)
    {
        Vector3 _origin;
        Vector3 _direction;

        if (_playerMovement.crouch)
        {
            _origin = _playerCrouch.position;
        }
        else
        {
            _origin = _playerEyes.position;
        }

        bool playerSeen = false;
        hit = new RaycastHit();

        for (int i = _viewAngleL; i <= _viewAngleR; i++)
        {
            if (_lookForPlayer)
            {
                _direction = (_navMeshController._currentTarget.position + _offset) - _origin;
            }
            else
            {
                _direction = _playerMovement.crouch ? _playerCrouch.forward : _playerEyes.forward;
            }

            // Rota el vector de dirección en i grados alrededor del eje y
            _direction = Quaternion.Euler(0, i, 0) * _direction;

            if (Physics.Raycast(_origin, _direction, out hit, _viewDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerSeen = true;
                    Debug.DrawRay(_origin, _direction * _viewDistance, Color.green); // Dibuja un raycast verde si el jugador es visto
                    break;
                }
                else
                {
                    Debug.DrawRay(_origin, _direction * _viewDistance, Color.red); // Dibuja un raycast rojo si el jugador no es visto
                }
            }
        }

        return playerSeen;
    }
}
