using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100; // Asegúrate de establecer esto al valor máximo de salud que puede tener el jugador

    public float health = 100.0f;
    private float healTime = 10.0f; // Tiempo en segundos para curar completamente

    public float _healCD = 5f;
    internal float _healCDcounter = 0f;
    private bool _gotDMG = false;

    public GameObject _canvas;
    private StateMachine _stateMachine;
    private SFX _sfx;
    private DamageEffect damageEffect;
    private bool _isTakingDamage = false; // Añade esta variable
    private float _dmgCounter; // Añade esta variable
    public CameraShaker cameraShake;
    public bool _isPlayerDead;

    public void Start()
    {
        Time.timeScale = 1;
        _canvas.SetActive(false);
        GameObject _player = GameObject.Find("Player");
        _sfx = _player.GetComponent<SFX>();
        damageEffect = FindObjectOfType<DamageEffect>(); // Encuentra el script DamageEffect en la escena
        _stateMachine = GetComponent<StateMachine>();
        cameraShake = GetComponent<CameraShaker>();
        _healCDcounter = 0;
        _dmgCounter = 0;
    }
    void Update()
    {
        Heal();
        Debug.Log(_healCDcounter);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        damageEffect.TriggerDamageEffect();
        _gotDMG = true;
        _isTakingDamage = true; // Establece _isTakingDamage en true cuando el jugador recibe daño
        _healCDcounter = 0f;
        _sfx.PlaySoundHit();
        cameraShake.Shake();
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(ResetTakingDamage()); // Inicia una corrutina para restablecer _isTakingDamage después de un cierto período de tiempo
        }
    }

    public void Heal()
    {
        if (_dmgCounter > 2)
        {
            _dmgCounter = 0;
            _isTakingDamage = false;
        }

        if (health != maxHealth && !_isTakingDamage)
        {
            if (_gotDMG == true)
            {
                _healCDcounter += Time.deltaTime;
                if (_healCDcounter > _healCD)
                {

                    StartCoroutine(HealOverTime());
                    _healCDcounter = 0f;
                }
            }
        }
    }

    private IEnumerator HealOverTime()
    {
        float startTime = Time.time;

        while (Time.time - startTime <= healTime && !_isTakingDamage)
        {
            health += Time.deltaTime * (maxHealth / healTime);
            health = Mathf.Min(health, maxHealth); // Asegúrate de no exceder la salud máxima
            yield return null;
        }

        _gotDMG = false; // Establece _gotDMG en false después de que se complete la curación
        _isTakingDamage = false; // Establece _isTakingDamage en false después de que se complete la curación
    }
    private IEnumerator ResetTakingDamage()
    {
        yield return new WaitForSeconds(2); // Espera 2 segundos
        _isTakingDamage = false; // Restablece _isTakingDamage en false
    }
    private void Die()
    {
        _sfx.PlaySoundDeath();
        Time.timeScale = 0;
        _canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}