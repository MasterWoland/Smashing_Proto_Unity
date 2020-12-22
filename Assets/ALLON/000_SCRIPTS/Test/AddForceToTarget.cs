using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;
 
    [SerializeField]
    float initialAngle;

    [SerializeField] private Rigidbody _rb;
    private Vector3 finalVelocity;

    void Start()
    {
        var rigid = _rb;//GetComponent<Rigidbody>();

        Vector3 p = target.position;

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) *
                                Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
        finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        Invoke("Shoot", 2f);
        // Fire!
        // rigid.velocity = finalVelocity;

        // Alternative way:
    }

    void Shoot()
    {
        _rb.AddForce(finalVelocity * _rb.mass, ForceMode.Impulse);
    }
}
