using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace nl.allon.addforce
{
    public class PhysicsBallController : MonoBehaviour
    {
        [SerializeField] private Transform _targetTF;
        [SerializeField] private GameObject _ball;
        [SerializeField] private float _launchSpeed;
        [SerializeField] private float _launchAngle;
        private Rigidbody _ballRB;
        private Transform _ballTF;
        private float _gravity;
        private Vector3 _upVector;

        // for testing purposes
        [SerializeField] private Transform _startTF;

        private void Start()
        {
            _gravity = Physics.gravity.magnitude;

            _ballTF = _ball.transform;
            _ballRB = _ball.GetComponent<Rigidbody>();
            ResetBall();
        }

        private void ResetBall()
        {
            _ballRB.velocity = Vector3.zero;
            _ballRB.angularVelocity = Vector3.zero;

            Vector3 startPos = _startTF.position;
            startPos.y += _ball.transform.localScale.y * 0.5f;
            _ball.transform.position = startPos;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("__ launch __");
                
                ResetBall();
                SetTargetWithSpeed(_targetTF.position, _launchSpeed, true);
                // LaunchBall();
            }
        }

        private void LaunchBall()
        {
            ResetBall();

            _gravity = Physics.gravity.magnitude;
            Debug.Log("[Gravity] "+_gravity);
            
            float angleInRadians = Mathf.Deg2Rad * _launchAngle;

            Vector3 ballPosition = _ballTF.position;
            Vector3 targetPosition = _targetTF.position;

            // Positions of the ball and the target on the same plane
            Vector3 planarPositionBall = new Vector3(ballPosition.x, 0, ballPosition.z);
            Vector3 planarPositionTarget = new Vector3(targetPosition.x, 0, targetPosition.z);

            // Planar distance between objects
            float distance = Vector3.Distance(planarPositionTarget, planarPositionBall);
           
            // Distance along the y axis between objects
            float yOffset = ballPosition.y - targetPosition.y;

            // calculate start velocity
            float initialVelocity = (1 / Mathf.Cos(angleInRadians)) *
                                    Mathf.Sqrt((0.5f * _gravity * Mathf.Pow(distance, 2)) /
                                               (distance * Mathf.Tan(angleInRadians) + yOffset));
            
            Vector3 velocity = new Vector3(
                0,
                initialVelocity * Mathf.Sin(angleInRadians),
                initialVelocity * Mathf.Cos(angleInRadians));

            // Rotate our velocity to match the direction between the two objects
            float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarPositionTarget - planarPositionBall);
            Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
            
            _ballRB.velocity = finalVelocity;
            
            // _ballRB.AddForce(finalVelocity * _ballRB.mass, ForceMode.Impulse);

        }

        public void SetTargetWithSpeed(Vector3 point, float speed, bool useLowAngle)
        {
            _launchSpeed = speed;

            Vector3 ballPosition = _ballTF.position;
            Vector3 targetPosition = _targetTF.position;
            
            // Positions of the ball and the target on the same plane
            Vector3 planarPositionBall = new Vector3(ballPosition.x, 0, ballPosition.z);
            Vector3 planarPositionTarget = new Vector3(targetPosition.x, 0, targetPosition.z);

            // Planar distance between objects
            // float distance = Vector3.Distance(planarPositionTarget, planarPositionBall);
           
            // Distance along the y axis between objects
            // float yOffset = ballPosition.y - targetPosition.y;



            Vector3 direction = ballPosition - targetPosition;
            float yOffset = direction.y;
            direction = MathHelper.ProjectVectorOnPlane(Vector3.up, direction);
            float distance = direction.magnitude;     

            float angle0, angle1;
            bool targetInRange = ProjectileMath.LaunchAngle(
                _launchSpeed, 
                distance, 
                yOffset, 
                Physics.gravity.magnitude, 
                out angle0, 
                out angle1);

            if (targetInRange)
                _launchAngle = useLowAngle ? angle1 : angle0;

            Debug.Log("target in range: "+targetInRange);
            Debug.Log("ANGLE: "+_launchAngle);

            // projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
            // SetTurret(direction, currentAngle * Mathf.Rad2Deg);
            //
            // currentTimeOfFlight = ProjectileMath.TimeOfFlight(currentSpeed, currentAngle, -yOffset, Physics.gravity.magnitude);
        }
    }

    public class MathHelper
    {
        //Projects a vector onto a plane. The output is not normalized.
        public static Vector3 ProjectVectorOnPlane(Vector3 planeNormal, Vector3 vector)
        {
            return vector - (Vector3.Dot(vector, planeNormal) * planeNormal);
        }
    }
}