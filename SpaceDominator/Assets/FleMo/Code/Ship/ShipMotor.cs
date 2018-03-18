using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
    private Direction IsOfMe(Transform source, Vector3 target)
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
    /// Returns the rotation who is left based on the direction
    /// </summary>
    /// <param name="actualRotaion">The actual rotaion or the source rotation</param>
    /// <param name="targetRotation">The target rotation</param>
    /// <param name="direction">The direction where the rotation should go. If passed Direction.None 0 will be returned</param>
    /// <returns>The rotation left</returns>
    private float RotationLeft(float actualRotaion, float targetRotation, Direction direction)
    {
        if(direction == Direction.None)
        {
            return 0;
        }

        float rotation = 0;

        if(direction == Direction.Left)
        {
            if(targetRotation > actualRotaion)
            {
                rotation = actualRotaion + 360 - targetRotation;
            }
            else
            {
                rotation = actualRotaion - targetRotation;
            }
        }
        else if(direction == Direction.Right)
        {
            if(actualRotaion > targetRotation)
            {
                rotation = 360 - actualRotaion + targetRotation;
            }
            else
            {
                rotation = targetRotation - actualRotaion;
            }
        }

        return rotation;
    }

    /// <summary>
    /// Rotates the ship to the given direction based on physics
    /// </summary>
    /// <param name="direction">The normalized direction the ship should face to</param>
    public void RotateTowards(Vector3 direction)
    {
        Vector3 target = this.transform.position + direction;
        float rotationAcceleration = _MotorSettings.RotationSpeed;

        Direction dir = IsOfMe(this.transform, target);

        switch(dir)
        {
            case Direction.Left:
                rotationAcceleration *= -1;
                break;
            case Direction.Right:

                break;
            case Direction.None:
                return;
        }

        #region physic rotation
        //myRigidbody.AddTorque(Vector3.up * rotationAcceleration);

        //if(myRigidbody.angularVelocity.y > _MotorSettings.MaxRotationSpeed)
        //{
        //    myRigidbody.angularVelocity = new Vector3(0, _MotorSettings.MaxRotationSpeed);
        //}
        //else if(myRigidbody.angularVelocity.y < -_MotorSettings.MaxRotationSpeed)
        //{
        //    myRigidbody.angularVelocity = new Vector3(0, -_MotorSettings.MaxRotationSpeed);
        //}

        //GameObject go = new GameObject();
        //GameObject go2 = new GameObject();
        //go2.transform.position = direction;

        //go.transform.LookAt(go2.transform);


        //float targetRotation = go.transform.eulerAngles.y;
        //float rotationLeft = RotationLeft(myRigidbody.transform.rotation.eulerAngles.y, targetRotation, dir);
        //DestroyImmediate(go);
        //DestroyImmediate(go2);
        //Debug.Log(rotationLeft);
        #endregion

        Quaternion rotation = Quaternion.Euler(0, rotationAcceleration * Time.deltaTime, 0);
        myRigidbody.MoveRotation(myRigidbody.rotation * rotation);
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