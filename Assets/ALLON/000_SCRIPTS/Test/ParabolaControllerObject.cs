using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaControllerObject : MonoBehaviour
{
    public ParabolaController Controller;

    private void OnEnable()
    {
        ParabolaController.ParabolaFinished += OnParabolaFinished;
    }
    
    private void OnDisable()
    {
        ParabolaController.ParabolaFinished -= OnParabolaFinished;
    }

    private void OnParabolaFinished()
    {
        Controller.FollowParabola();
    }

    private void Update()
    {
        
    }
}
