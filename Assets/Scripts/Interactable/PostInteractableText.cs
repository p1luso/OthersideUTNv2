using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostInteractableText : MonoBehaviour
{
    public PlayerInteraction interaction;
    private int interactablesRemaining;
    private string postInteractionText;
    private GameObject postInteractionTextObject;
    public GameObject postInteractionCanvas;
    public TextMeshProUGUI textMeshPro;
    public float textFadeSpeed = 1f; // Velocidad a la que el texto aparece
    private float _currentAlpha = 0.0f; // Alfa actual del texto
    public bool postInteractionCheck;




    // Start is called before the first frame update
    void Start()
    {
        postInteractionCheck = false;
        interaction = GameObject.Find("Player").GetComponent<PlayerInteraction>();
                
        interactablesRemaining = 5;

        postInteractionCanvas.SetActive(false);
        postInteractionTextObject = GameObject.Find("PostInteractionText");
    }

    // Update is called once per frame
    void Update()
    {

        if (postInteractionCheck == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HideText();
                
                postInteractionCheck = false;
            }

        }



    }


    public void ShowText()
    {
        interactablesRemaining = interactablesRemaining -= 1;

        SetPostInteractionText();

        StartCoroutine(FadeInText());

        


       



    }

    private IEnumerator FadeInText()
    {
        // Asegúrate de que el texto esté completamente transparente al principio
        Color textColor = textMeshPro.color;
        textColor.a = 0.0f;
        textMeshPro.color = textColor;

        // Muestra el canvas y el texto
        postInteractionCanvas.SetActive(true);
        textMeshPro.text = postInteractionText;

        // Aumenta gradualmente el alfa del texto hasta que sea completamente opaco
        while (textColor.a < 1.0f)
        {
            _currentAlpha += textFadeSpeed * Time.deltaTime;
            textColor.a = _currentAlpha;
            textMeshPro.color = textColor;
            yield return null;
        }
    }

    public void HideText()
    {
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        // Obtiene el color actual del texto
        Color textColor = textMeshPro.color;

        // Disminuye gradualmente el alfa del texto hasta que sea completamente transparente
        while (textColor.a > 0.1f)
        {
            _currentAlpha = textFadeSpeed * Time.deltaTime;
            textColor.a -= _currentAlpha;
            textMeshPro.color = textColor;
            yield return new WaitForEndOfFrame();
        }

        // Oculta el canvas
        postInteractionCanvas.SetActive(false);
        yield return null;
    }


    public void SetPostInteractionText()
    {
        

        if (interactablesRemaining > 0)
        {
            postInteractionText = interactablesRemaining + " lost souls remaining. Press E to continue...";
            return;

        }
        else if (interactablesRemaining <= 0)
        {
            postInteractionText = "Find the exit at the central pillar.";
            return;
        }


    }

}
