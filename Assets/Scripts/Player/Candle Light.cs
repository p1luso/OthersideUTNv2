using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour
{
    public Transform _candle;
    public Light _light;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            _light.enabled = !_light.enabled;
            _candle.gameObject.SetActive(!_candle.gameObject.activeSelf);
        }
    }
}
