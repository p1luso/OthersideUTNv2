using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
   [Range(0.001f, 0.01f)]
   public float Amount = 0.002f;
   [Range(1f, 30f)]
   public float Frequency = 10.0f;
   [Range(0f,15f)]
   public float Smooth = 10.0f;
   public float SpeedFactor = 1.2f;
   Vector3 StartPos;

   private Movement _movement;
   void Start()
   {
      StartPos = transform.localPosition;
      _movement = GetComponentInParent<Movement>();
   }
   void Update()
   {
      CheckForHeadbobTrigger();
      StopHeadbob();
   }
   private void CheckForHeadbobTrigger()
   {
      float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
      if (inputMagnitude > 0)
      {
         StartHeadBob();
      }
   }
   private Vector3 StartHeadBob()
   {
      Vector3 pos = Vector3.zero;
       float speed = _movement._speed;
       float frequency = Frequency + speed * SpeedFactor;
       
       pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
       pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
       transform.localPosition += pos;
       return pos;

   }
   private void StopHeadbob()
   {
      if (transform.localPosition == StartPos) return;
      transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
   }

}
