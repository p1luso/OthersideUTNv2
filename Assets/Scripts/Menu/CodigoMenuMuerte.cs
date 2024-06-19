using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodigoMenuMuerte : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public Movement playerMovement;

    public Player player;

    public CodigoMenuPausa codigoMenuPausa;

    public void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        codigoMenuPausa = GameObject.FindObjectOfType<CodigoMenuPausa>().GetComponent<CodigoMenuPausa>();
    }

    public void Update()
    {
        if (player._isPlayerDead == true)
        {
            DeathScreen();
        }
    }

    public void DeathScreen()
    {
        codigoMenuPausa.enabled = false;
        playerInteraction.enabled = false;
        playerMovement.enabled = false;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Pausa");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("Game Scene");
        Time.timeScale = 1;

    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
