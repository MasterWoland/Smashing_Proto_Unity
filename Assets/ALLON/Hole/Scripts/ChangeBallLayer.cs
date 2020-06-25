using UnityEngine;
using System.Collections;

public class ChangeBallLayer : MonoBehaviour {

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable
    private readonly string _ballTag = "Ball";
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(_ballTag))
        {
            Debug.Log("___ change layer! ____");
            other.gameObject.layer = LayerOnEnter;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // if(other.gameObject.CompareTag(_ballTag))
        // {
        //     Debug.Log("___ reset layer ____");
        //     other.gameObject.layer = LayerOnExit;
        // }
    }
}