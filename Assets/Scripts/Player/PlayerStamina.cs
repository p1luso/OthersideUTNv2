using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f; // La máxima stamina que el jugador puede tener.
    public float currentStamina; // La stamina actual del jugador.
    public float staminaDecreasePerSecond = 10f; // Cuánto disminuye la stamina por segundo cuando el jugador corre.
    public float staminaIncreasePerSecond; // Cuánto aumenta la stamina por segundo cuando el jugador descansa.
    public bool _canRun = false;
    public float _timeToRecover = 3f;
    private float _recoverCounter;
    private SFX _sfx;
    private Movement _playerMovement;
    private bool isAgitatedSoundPlaying = false;

    // Inicializar la stamina del jugador al máximo al comenzar.
    void Start()
    {
        currentStamina = maxStamina;
        _recoverCounter = 3;
        GameObject _player = GameObject.Find("Player");
        _sfx = _player.GetComponent<SFX>();
        staminaIncreasePerSecond = maxStamina / _timeToRecover;
        _playerMovement = FindObjectOfType<Movement>();
    }

    // Actualizar la stamina del jugador cada frame.
    void Update()
    {
        // Si el jugador está corriendo, disminuir la stamina.
        if (_playerMovement._isMoving && Input.GetKey(KeyCode.LeftShift) && _canRun == true)
        {
            DecreaseStamina(staminaDecreasePerSecond * Time.deltaTime);
            _recoverCounter = 0;
        }
        // Si el jugador no está corriendo, aumentar la stamina.
        else
        {
            IncreaseStamina(staminaIncreasePerSecond * Time.deltaTime);
            _recoverCounter += Time.deltaTime;
        }

        // Asegurarse de que la stamina no exceda los límites.
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina <= 0)
        {
            StartCoroutine(PlayAgitatedSound());
            _canRun = false;
        }
        else if (_recoverCounter >= _timeToRecover && currentStamina >= maxStamina)
        {
            _canRun = true;
        }
    }

    // Método para disminuir la stamina.
    void DecreaseStamina(float amount)
    {
        currentStamina -= amount;
    }

    // Método para aumentar la stamina.
    void IncreaseStamina(float amount)
    {
        currentStamina += amount;
    }

    IEnumerator PlayAgitatedSound()
    {
        isAgitatedSoundPlaying = true;
        _sfx.PlaySoundAgitated();
        yield return new WaitForSeconds(_timeToRecover); // Espera la duración del sonido agitado antes de comenzar a desvanecerlo
        _sfx.FadeOutAgitatedSound(_sfx._audioSourceAgitated);
        isAgitatedSoundPlaying = false;
    }
}