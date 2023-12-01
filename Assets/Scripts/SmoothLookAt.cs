using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SmoothLookAt : MonoBehaviour
{
    public Transform lookAtTarget;
    public float rotateSmoothTime = 0.1f;
    private float angularVelocity = 0.0f;
    public bool tracking = false;

    void LateUpdate()
    {
        if (tracking)
        {
            var angleToTarget = Quaternion.LookRotation(lookAtTarget.position - transform.position);
            var rotationDifference = Quaternion.Angle(transform.rotation, angleToTarget);
            //if (rotationDifference != 0)
            //{
             //rotationDifference = 0.001f;
            //}
            if (rotationDifference != 0)
            {
                var t = Mathf.SmoothDampAngle(rotationDifference,0.0f,ref angularVelocity,rotateSmoothTime);
                t = 1.0f - t/rotationDifference;
                transform.rotation = Quaternion.Slerp(transform.rotation, angleToTarget, t);
            }
        }
        
    }
}
