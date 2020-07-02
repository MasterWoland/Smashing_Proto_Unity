using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
   [SerializeField] private float _multiplier = 1f;

   public float GetMultiplier()
   {
      return _multiplier;
   }
}
