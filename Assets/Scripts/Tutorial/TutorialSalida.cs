using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSalida : MonoBehaviour
{
    private GameObject _tutorialObject;
    public Movement _playerMovement;
    public GameObject canvasExit;
    public GameObject canvasTutorial;
    private AudioSource[] _audioSources;

    void Start()
    {
        _tutorialObject = GameObject.Find("Tutorial");
        _playerMovement = FindObjectOfType<Movement>();
        canvasExit.SetActive(false);
        _audioSources = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _tutorialObject.SetActive(false);
            _playerMovement.enabled = true;
            foreach (AudioSource audioSource in _audioSources)
            {
                audioSource.UnPause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            canvasExit.SetActive(false);
            canvasTutorial.SetActive(true);
            _playerMovement.enabled = true;
            foreach (AudioSource audioSource in _audioSources)
            {
                audioSource.UnPause();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _playerMovement.enabled = false;
        canvasTutorial.SetActive(false);
        canvasExit.SetActive(true);
        // Pausar todos los AudioSources
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.Pause();
        }
    }
}
