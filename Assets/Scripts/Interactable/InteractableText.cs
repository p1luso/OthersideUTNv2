using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableText : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI textMeshPro;
    public PlayerInteraction playerInteraction;
    public Movement playerMovement;
    public Pause _pause;
    private Enemy _enemy;


    public PostInteractableText postInteractableText;

    [TextArea(3, 10)]
    public List<string> texts;
    private Interactable interactable;
    public float textFadeSpeed = 1f; // Velocidad a la que el texto aparece
    private float currentAlpha = 0.0f; // Alfa actual del texto
    [HideInInspector]
    public bool isShowing = false;
    void Start()
    {
        interactable = GetComponent<Interactable>();
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        canvas.SetActive(false);

    }

    public void ShowText()
    {
        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        // Asegúrate de que el texto esté completamente transparente al principio
        Color textColor = textMeshPro.color;
        textColor.a = 0.0f;
        textMeshPro.color = textColor;

        // Muestra el canvas y el texto
        canvas.SetActive(true);
        textMeshPro.text = texts[playerInteraction.interactionCount - 1];

        // Aumenta gradualmente el alfa del texto hasta que sea completamente opaco
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.enabled = false;


        while (textColor.a < 1.0f)
        {
            currentAlpha += textFadeSpeed * Time.deltaTime;
            textColor.a = currentAlpha;
            textMeshPro.color = textColor;
            yield return null;
        }

        // Pausa el juego
        
        isShowing = true;
        _pause.InteractableTextShowing();
        
    }

    public void HideText()
    {
        StartCoroutine(FadeOutText());
    }
    private IEnumerator FadeOutText()
    {
        isShowing = false;
        // Obtiene el color actual del texto
        Color textColor = textMeshPro.color;

        // Disminuye gradualmente el alfa del texto hasta que sea completamente transparente
        while (textColor.a > 0.1f)
        {
            currentAlpha = textFadeSpeed * Time.deltaTime;
            textColor.a -= currentAlpha;
            textMeshPro.color = textColor;
            yield return new WaitForEndOfFrame();
        }

        // Oculta el canvas
        canvas.SetActive(false);

        // Reanuda el juego
        Cursor.visible = false;
        //Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        //_enemy.Pause = false;
        _pause.InteractableTextNotShowing();


        postInteractableText.postInteractionCheck = true;

        postInteractableText.ShowText();

        yield return null;
    }

}
