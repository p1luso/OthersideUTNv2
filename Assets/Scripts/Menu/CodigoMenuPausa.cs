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
    public AudioMixer audioMixer;
    private float _originalVolume;

    // Start is called before the first frame update
    void Start()
    {
        ObjetoMenuPausa.SetActive(false);
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
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

        audioMixer.SetFloat("Volume", _originalVolume);        
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
        audioMixer.GetFloat("Volume", out _originalVolume);
        audioMixer.SetFloat("Volume", -80f); // Ajusta el volumen a 0 dB o el valor que desees
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}


