using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Motor of the ship. 
/// </summary>
public class ShipMotor : MonoBehaviour, IShipMotor
{
    [SerializeField]
    ShipMotorSettings _MotorSettings;

    private Rigidbody myRigidbody;
    private enum Direction { Right, Left, None }


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves the rigidbody to the direction.
    /// </summary>
    /// <param name="direction">The normalized direction the ship should move to.</param>
    public void Move(Vector3 direction)
    {
        myRigidbody.AddForce(direction * _MotorSettings.Thrust);

        if(myRigidbody.velocity.magnitude > _MotorSettings.MaxSpeed)
        {
            myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, _MotorSettings.MaxSpeed);
        }
        
    }

    /// <summary>
    /// Slows down the ship by brake force.
    /// </summary>
    public void SlowDown()
    {
        Vector3 br = myRigidbody.velocity.normalized * -1 * _MotorSettings.Brake;
        myRigidbody.AddForce(br);
    }

    /// <summary>
    /// Calculates if target vector is right or left of source.
    /// </summary>
    /// <param name="source">The source transform</param>
    /// <param name="target">The target position vector3</param>
    /// <returns>Returns the direction.</returns>
    private Direction IsRightOfMe(Transform source, Vector3 target)
    {
        var relativePoint = source.InverseTransformPoint(target);
        if (relativePoint.x < 0.0)
        {
            //object is left
            return Direction.Left;
        }
        else if (relativePoint.x > 0.0)
        {
            //object is right
            return Direction.Right;
        }
        else
        {
            //object is direct ahead
            return Direction.None;
        }

    }

    /// <summary>
    /// Rotates the ship to the given direction based on physics
    /// </summary>
    /// <param name="direction">The normalized direction the ship should face to</param>
    public void RotateTowards(Vector3 direction)
    {
        Vector3 target = this.transform.position + direction;
        float rotationAcceleration = _MotorSettings.RotationAcceleration;

        if(!IsRightOfMe(this.transform, target))
        {
            rotationAcceleration *= -1;
        }

        myRigidbody.AddTorque(Vector3.up * rotationAcceleration);

        if(myRigidbody.angularVelocity.y > _MotorSettings.MaxRotationSpeed)
        {
            myRigidbody.angularVelocity = new Vector3(0, _MotorSettings.MaxRotationSpeed);
        }
        else if(myRigidbody.angularVelocity.y < -_MotorSettings.MaxRotationSpeed)
        {
            myRigidbody.angularVelocity = new Vector3(0, -_MotorSettings.MaxRotationSpeed);
        }


    }

    public ShipMotorSettings GetMotorSettings()
    {
        return _MotorSettings;
    }

    public float GetSpeed()
    {
        return myRigidbody.velocity.magnitude;
    }
}