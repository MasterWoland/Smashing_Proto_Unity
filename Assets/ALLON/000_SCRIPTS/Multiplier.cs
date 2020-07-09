using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
   public static event Action<int> MultiplierHit;
   // [SerializeField] private float _multiplier = 1f;

   // public float GetMultiplier()
   // {
   //    return _multiplier;
   // }
   
   private void OnTriggerEnter(Collider other)
   {
      // Debug.Log("[TRIGGER] "+other.gameObject.name);

      if (other.CompareTag("Ball"))
      {
         Vector3 distanceToCenter = other.transform.position - this.transform.parent.position;
         
         float distance = distanceToCenter.magnitude;
         Debug.Log("[TRIGGER] Distance = "+distance);
         
         // MultiplierHit?.Invoke(distance);
         
         if (distance <= 0.5f) // size is 1m, so radius is 0.5
         {
            // Debug.Log("[Ball] hit multiplier: 5 ");
            MultiplierHit?.Invoke(5);
         }
         else if (distance <= 1.5f)
         {
            // Debug.Log("[Ball] hit multiplier: 3 ");
            MultiplierHit?.Invoke(3);
         }
         else
         {
            // Debug.Log("[Ball] hit multiplier: 2 ");
            MultiplierHit?.Invoke(2);
         }
      }
   }

}
