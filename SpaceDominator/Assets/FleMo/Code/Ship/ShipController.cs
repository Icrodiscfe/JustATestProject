using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IShipMotor))]
public class ShipController : MonoBehaviour, IShipController
{
    private IShipMotor shipMotor;
    private Vector3 rotateShipTowards;

    void Start()
    {
        shipMotor = GetComponent<IShipMotor>();
    }

    void FixedUpdate()
    {
        shipMotor.RotateTowards(rotateShipTowards);
        Debug.Log(rotateShipTowards);
    }

    /// <summary>
    /// Calls the ship motor to move the ship to given direction. Must be called constantinous. If no movement is wished pass null
    /// </summary>
    /// <param name="direction">Normalized direction the ship should move to</param>
	public void Move(Vector3 direction)
    {
        rotateShipTowards = direction;
        shipMotor.Move(direction);
    }

    public void SlowDown()
    {
        shipMotor.SlowDown();
    }
}
