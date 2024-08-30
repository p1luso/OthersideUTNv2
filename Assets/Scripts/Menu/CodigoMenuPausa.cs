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
    public AudioMixer audioMixer; // Referencia al Audio Mixer
    private bool Pausa = false;
    private float originalVolume;

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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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

        // Restaurar el volumen original del Audio Mixer
        audioMixer.SetFloat("Volume", originalVolume);
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

        // Bajar el volumen del Audio Mixer para "pausar" el sonido
        audioMixer.GetFloat("Volume", out originalVolume);
        audioMixer.SetFloat("Volume", -80f); // Baja el volumen, -80 dB es pr�cticamente silencioso
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
