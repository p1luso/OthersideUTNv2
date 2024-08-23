using Cinemachine;
using UnityEngine;

public class CandleLight : MonoBehaviour
{
    public Transform _candle;
    public Light _light;
    public bool lightOn;
    public float zoomInDistance = 15f;  // Valor del Far Clip Plane al acercar
    public CinemachineVirtualCamera _camera;

    private float originalFarClipPlane;
    private bool originalFog;
    private Color originalFogColor;
    private float originalFogDensity;
    public Color fogColor = Color.gray;
    public float fogDensity = 0.05f;

    void Start()
    {
        // Almacena el valor original del Far Clip Plane
        originalFarClipPlane = _camera.m_Lens.FarClipPlane;

        // Almacena los valores originales de la niebla
        originalFog = RenderSettings.fog;
        originalFogColor = RenderSettings.fogColor;
        originalFogDensity = RenderSettings.fogDensity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            lightOn = !lightOn; // Cambia el estado de la luz
            _light.enabled = !_light.enabled;
            _candle.gameObject.SetActive(!_candle.gameObject.activeSelf);
        }

        if (enabled)
        {
            if (lightOn)
            {
                _camera.m_Lens.FarClipPlane = zoomInDistance;

                // Configura la niebla
                RenderSettings.fog = true;
                RenderSettings.fogColor = fogColor;
                RenderSettings.fogDensity = fogDensity;
            }
            else
            {
                _camera.m_Lens.FarClipPlane = originalFarClipPlane;

                // Restaura los valores originales de la niebla
                RenderSettings.fog = originalFog;
                RenderSettings.fogColor = originalFogColor;
                RenderSettings.fogDensity = originalFogDensity;
            }
        }
    }
}