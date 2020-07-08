using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnAwake : MonoBehaviour
{
    public bool Deactivate = true;

    private void Awake()
    {
        if(Deactivate) this.gameObject.SetActive(false);
    }
}
