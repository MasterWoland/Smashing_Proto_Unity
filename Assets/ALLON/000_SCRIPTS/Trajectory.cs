using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Trajectory contains the Spawn and Target location for the ball
/// </summary>
public class Trajectory : MonoBehaviour
{
   [SerializeField] private Transform SpawnTransform;
   [SerializeField] private Transform TargetTransform;
   
   public Vector3 SpawnPosition => SpawnTransform.position;
   public Vector3 TargetPosition => TargetTransform.position;
}
