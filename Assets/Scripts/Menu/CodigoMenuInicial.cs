using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CodigoMenuInicial : MonoBehaviour
{
    public Animator animator;
    public GameObject menuPrincipal;
    public GameObject menuOpciones;
    public float tiempoDeEsperaParaActivar;
    public AudioSource _menuMusic;


    public void Start()
    {
        Time.timeScale = 1;
        _menuMusic.volume = 1;

        menuPrincipal.SetActive(true);
        menuOpciones.SetActive(false);
        animator.Play("En reposo"); // Reemplaza con el nombre exacto del estado inicial.
        animator.ResetTrigger("Ir Menu Opciones");
        animator.ResetTrigger("Ir Menu Principal");
    }

    public void EmpezarNivel(string NombreNivel)
    {
        SceneManager.LoadScene("Game Scene");
    }  


    public void MenuMusic()
    {
        _menuMusic.volume = 1;
    }


    public void IrAMenudeOpciones()
    {
        Debug.Log("Trigger de animación activado: Ir Menu Opciones");
        animator.SetTrigger("Ir Menu Opciones");
        menuPrincipal.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void IrAMenuPrincipal()
    {
        animator.SetTrigger("Ir Menu Principal");
        menuOpciones.SetActive(false);
        menuPrincipal.SetActive(true);
        StartCoroutine(ActivarConDemora(menuPrincipal));
    }  


    private IEnumerator ActivarConDemora(GameObject ObjetoActivar)
    {
        yield return new WaitForSeconds(tiempoDeEsperaParaActivar);
        ObjetoActivar.SetActive(true);
    }

    private IEnumerator EsperarYActivarOpciones()
    {
        yield return new WaitForSeconds(tiempoDeEsperaParaActivar);
        menuPrincipal.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void EmpezarNivel()
    {
        SceneManager.LoadScene("Game Scene");
        Time.timeScale = 1;
    }


    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aqui se Cierra el juego");
    }
}


