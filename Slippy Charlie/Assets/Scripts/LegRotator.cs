using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRotator : ArmRotator
{
    public LegRotator()
    {

    }

    public override Quaternion GetRotationTarget()
    {
        Quaternion rotationTarget; 
        
        if(isLeftLimb)
        {
            rotationTarget = Quaternion.Euler(Mathf.PingPong(Time.time * currentRotateSpeed, pingPongAngleAmount * 2) - pingPongAngleAmount, startingRotation.x, startingRotation.z);
        }
        else
        {
            rotationTarget = Quaternion.Euler(-Mathf.PingPong(Time.time * currentRotateSpeed, pingPongAngleAmount * 2) - pingPongAngleAmount, startingRotation.x, startingRotation.z);
        }

        return rotationTarget;
    }
}
