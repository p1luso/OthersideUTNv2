using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CodigoMenuEndGame : MonoBehaviour
{
    public GameObject opciones;

    public void IrAlMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }



}
