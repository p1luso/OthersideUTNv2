using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialCanvas;
    private GameObject _tutorial;
    public TextMeshProUGUI textMeshPro;
    public Transform player;
    private Collider _collider;
    [TextArea(3, 10)]
    public List<string> texts;
    [HideInInspector]
    public int _text = 0;
    public int lastText = 3;
    public float textFadeSpeed = 1f; // Velocidad a la que el texto aparece
    private float _currentAlpha = 0.0f; // Alfa actual del texto

    // Start is called before the first frame update
    void Start()
    {
        tutorialCanvas.SetActive(false);
        _collider = GetComponent<Collider>();
        _tutorial = GameObject.Find("Tutorial");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_text >= lastText-1) // Si excede la cantidad de textos disponibles, ocultar el texto
            {
                HideText();
                _tutorial.SetActive(false);
            }
            else // Si hay más textos, mostrar el siguiente
            {
                StartCoroutine(ChangeText());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _collider.enabled = false;
            ShowText();
        }
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
        tutorialCanvas.SetActive(true);
        textMeshPro.text = texts[_text];

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
        tutorialCanvas.SetActive(false);
        yield return null;
    }
    private IEnumerator ChangeText()
    {
        yield return StartCoroutine(FadeOutText()); // Esperar a que el texto actual se desvanezca completamente
        _currentAlpha = 0.0f; // Reiniciar el valor de alpha
        _text++;
        yield return StartCoroutine(FadeInText()); // Mostrar el siguiente texto
    }
}

