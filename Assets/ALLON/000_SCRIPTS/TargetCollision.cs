using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
   public static event Action<int, Vector3> TargetHit;
   public int Value = default;
   [SerializeField] private float _offsetZ = 0; // used for score animation
   
   private void OnCollisionEnter(Collision other)
   {
      // in Physics setup this should only be able to collide with ball
      // if statement should be redundant
      if (other.gameObject.CompareTag("Ball"))
      {
          TargetHit?.Invoke(Value, transform.position);
          // Debug.Log("[Target] "+Value);
      } 
   }
}
