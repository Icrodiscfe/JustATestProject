using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IShipMotor))]
public class ShipController : MonoBehaviour, IShipController
{
    private IShipMotor shipMotor;
    private Vector3 moveDirectionInput;

    void Start()
    {
        shipMotor = GetComponent<IShipMotor>();
    }

    void FixedUpdate()
    {
        if (moveDirectionInput == Vector3.zero)
        {
            shipMotor.SlowDown();
        }
    }

    /// <summary>
    /// Calls the ship motor to move the ship to given direction. Must be called constantinous. If no movement is wished pass Vector3.zero
    /// </summary>
    /// <param name="direction">Normalized direction the ship should move to</param>
	public void Move(Vector3 direction)
    {
        moveDirectionInput = direction;
        shipMotor.Move(direction);
    }
}
