using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CodigoMenuPausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public PlayerInteraction playerInteraction;
    public InteractableText _interactableText;
    public Movement playerMovement;
    private bool Pausa = false;
    private AudioSource[] _audioSources;

    // Start is called before the first frame update
    void Start()
    {
        ObjetoMenuPausa.SetActive(false);
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        _audioSources = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!_interactableText.isShowing)
            {
                if (Pausa == false)
                {
                    PauseMenu();
                    

                }
                else if (Pausa == true)
                {
                    Resumir();
                }
            }
            else if (_interactableText.isShowing)
            {
                _interactableText.HideText();
                Resumir();
            }
        }

    }
    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;
        playerInteraction.enabled = true;
        playerMovement.enabled = true;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.UnPause();
        }
    }

    public void PauseMenu()
    {
        ObjetoMenuPausa.SetActive(true);
        Pausa = true;
        playerInteraction.enabled = false;
        playerMovement.enabled = false;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // Pausar todos los AudioSources
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.Pause();
        }
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}


