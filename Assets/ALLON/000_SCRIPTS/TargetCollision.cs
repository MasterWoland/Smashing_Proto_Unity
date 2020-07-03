using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
   public static event Action<int> TargetHit;
   public int Value = default;

   private void OnCollisionEnter(Collision other)
   {
      // in Physics setup this should only be able to collide with ball
      // if statement should be redundant
      if (other.gameObject.CompareTag("Ball"))
      {
          TargetHit?.Invoke(Value);
          Debug.Log("[Target] "+Value);
      } 
   }
}
