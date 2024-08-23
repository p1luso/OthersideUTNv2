using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
   public float fadeDuration = 1.0f; // Duración del fade out
private Image damageImage;
private Color originalColor;
private float currentAlpha = 0f;
private Player _player;
private bool isTakingDamage = false;

void Start()
{
    damageImage = GetComponent<Image>();
    originalColor = damageImage.color;
    damageImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        _player = player.GetComponent<Player>();
    }
}

void Update()
{
    if (_player != null)
    {
        float damageReceived = 1 - (_player.health / _player.maxHealth);
        float alphaTarget = damageReceived * 0.2f; // Ajusta este valor para cambiar el alpha máximo

        if (isTakingDamage)
        {
            currentAlpha = Mathf.Min(currentAlpha + Time.deltaTime / fadeDuration, alphaTarget);
        }
        else
        {
            currentAlpha = Mathf.Max(currentAlpha - Time.deltaTime / fadeDuration, 0);
        }
        damageImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha);
    }
}
public void TriggerDamageEffect()
{
    isTakingDamage = true;
}

public void StopDamageEffect()
{
    isTakingDamage = false;
}
}


