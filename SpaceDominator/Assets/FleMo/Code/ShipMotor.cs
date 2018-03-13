using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Motor of the ship. 
/// </summary>
public class ShipMotor : MonoBehaviour, IShipMotor
{
    private Rigidbody myRigidbody;

    [SerializeField]
    private float _Thrust = 10000;
    [SerializeField]
    private float _MaxSpeed = 100;
    [SerializeField]
    private float _RotationSpeed = 10000;
    [SerializeField]
    private float _Brake = 10000;
    [SerializeField]
    private GameObject Test;

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
        myRigidbody.AddForce(direction * _Thrust);

        if(myRigidbody.velocity.magnitude > _MaxSpeed)
        {
            myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, _MaxSpeed);
        }


        RotateTowards(direction);
    }

    /// <summary>
    /// Slows down the ship by brake force.
    /// </summary>
    public void SlowDown()
    {
        Vector3 br = myRigidbody.velocity.normalized * -1 * _Brake;
        myRigidbody.AddForce(br);
    }

    /// <summary>
    /// Rotates the ship to the given direction based on physics
    /// </summary>
    /// <param name="direction">The normalized direction the ship should face to</param>
    private void RotateTowards(Vector3 direction)
    {
        Vector3 target = this.transform.position + direction;
        Test.transform.position = target;
        Quaternion rotateTo = Quaternion.FromToRotation(this.transform.position, target);
        transform.LookAt(target);  
    }
}
