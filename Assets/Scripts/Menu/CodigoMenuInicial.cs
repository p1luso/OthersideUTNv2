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
        menuPrincipal.SetActive(true);
        menuOpciones.SetActive(false);
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
        Debug.Log("Clickeadas opciones");
        animator.SetTrigger("Ir Menu Opciones");
        menuPrincipal.SetActive(false);
        StartCoroutine(ActivarConDemora(menuOpciones));
        //menuOpciones.SetActive(true);

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


