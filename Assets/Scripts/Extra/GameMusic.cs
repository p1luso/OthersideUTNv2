using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource _calmMusic;
    public AudioSource _alertMusic;
    public AudioSource _chaseMusic;

    private StateMachine[] _stateMachine;
    public float transitionTime = 0f; // El tiempo de transición entre los volúmenes
    private float _targetVolumeCalm;
    private float _targetVolumeAlert;
    private float _targetVolumeChase;
    public float _volumeMusicFromOptions;
    public Player _player;
    

    private void Start()
    {
        _stateMachine = FindObjectsOfType<StateMachine>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _volumeMusicFromOptions = 1f;
    }

    void Update()
    {
        bool isChase = false;
        bool isAlert = false;

        foreach (var stateMachine in _stateMachine)
        {
            if (stateMachine._currentState == stateMachine._chaseState || stateMachine._currentState == stateMachine._attackState)
            {
                isChase = true;
                break;
            }
            else if (stateMachine._currentState == stateMachine._alertState)
            {
                isAlert = true;
            }
        }
        if (_player.health <= 0)
        {
            isChase = false;
            isAlert = false;
        }

        if (isChase)
        {
            _targetVolumeCalm = 0;
            _targetVolumeAlert = 0;
            _targetVolumeChase = _volumeMusicFromOptions;
        }
        else if (isAlert)
        {
            _targetVolumeCalm = 0;
            _targetVolumeAlert = _volumeMusicFromOptions;
            _targetVolumeChase = 0;
        }
        else
        {
            _targetVolumeCalm = _volumeMusicFromOptions;
            _targetVolumeAlert = 0;
            _targetVolumeChase = 0;
        }

        _calmMusic.volume = Mathf.Lerp(_calmMusic.volume, _targetVolumeCalm, Time.deltaTime / transitionTime);
        _alertMusic.volume = Mathf.Lerp(_alertMusic.volume, _targetVolumeAlert, Time.deltaTime / transitionTime);
        _chaseMusic.volume = Mathf.Lerp(_chaseMusic.volume, _targetVolumeChase, Time.deltaTime / transitionTime);
    }
}