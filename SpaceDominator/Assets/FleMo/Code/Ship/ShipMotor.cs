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

        RotateTowards(direction);
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
    /// Rotates the ship to the given direction based on physics
    /// </summary>
    /// <param name="direction">The normalized direction the ship should face to</param>
    private void RotateTowards(Vector3 direction)
    {
        Vector3 target = this.transform.position + direction;
        Quaternion actualRotation = myRigidbody.rotation;
        Quaternion rotateTo = Quaternion.FromToRotation(this.transform.position, target);
        transform.LookAt(target);
        Quaternion targetRotation = myRigidbody.rotation;
        myRigidbody.rotation = actualRotation;

        myRigidbody.AddTorque(Vector3.up * _MotorSettings.RotationSpeed);
        //myRigidbody.MoveRotation(targetRotation);
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
