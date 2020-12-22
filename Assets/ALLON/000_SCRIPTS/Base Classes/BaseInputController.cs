using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public abstract class 
    BaseInputController : MonoBehaviour
{
    public virtual void InitInputController(InputDevice device) {}
    public virtual void DisconnectInputController(InputDevice device) {}
}
