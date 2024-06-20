using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Transform camTransform;
    public float shakeDuration = 2f;
    public float shakeAmount = 0.2f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    public void Shake()
    {
        originalPos = camTransform.localPosition;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", shakeDuration);
    }

    void DoShake()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 3;
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        camTransform.localPosition = new Vector3(0f,0.63f,-0.42f);
    }
}