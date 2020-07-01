using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
   private Vector3 _prevPosition;
   private Vector3 _currentPosition;
   private Rigidbody _rb;
   
   private void Start()
   {
      _prevPosition = _currentPosition = transform.position;
      _rb = GetComponent<Rigidbody>();
   }

   private void OnCollisionEnter(Collision other)
   {
      // Debug.Log("[Player] Collided with: "+other.gameObject.tag);

      if (other.gameObject.CompareTag("Ball"))
      {
         // Time.timeScale = 0;
         Debug.Log("[Player] Contact count: "+other.contactCount);         
         Debug.Log("[Player] Contact normal: "+other.GetContact(0).normal);

         Vector3 direction = _rb.position - _prevPosition;
         Debug.Log("[Player] Move direction: "+direction);
         Debug.Log("[Player] Player velocity: "+_rb.velocity);
         
         Vector3 point = other.GetContact(0).point;
         Vector3 normal = other.GetContact(0).normal;
         
         // normal direction contact point sphere
         Debug.DrawLine(point, point + (normal * 10f), Color.magenta, 20f);
         
         // player move direction
         Debug.DrawLine(_rb.position, _rb.position + (direction * 10f), Color.green, 20f);

         other.gameObject.GetComponent<ParabolaController>().StopFollow();
         other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
         other.gameObject.GetComponent<Rigidbody>().useGravity = true;
         
         // GetComponent<Rigidbody>().isKinematic = true;
      }
   }

   private void FixedUpdate()
   {
      _prevPosition = _currentPosition;
      _currentPosition = _rb.position;
   }
}
