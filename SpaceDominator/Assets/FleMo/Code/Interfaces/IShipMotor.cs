using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipMotor {
    /// <summary>
    /// Moves the rigidbody to the direction.
    /// </summary>
    /// <param name="direction">The normalized direction the ship should move to.</param>
    void Move(Vector3 direction);

    /// <summary>
    /// Slows down the ship by brake force.
    /// </summary>
    void SlowDown();

    /// <summary>
    /// Returns a object ob IShipMotorSettings to get the values of the ship
    /// </summary>
    /// <returns>Actual IShipMotorSettings</returns>
    ShipMotorSettings GetMotorSettings();

    /// <summary>
    /// Returns the actual speed of the ship
    /// </summary>
    /// <returns>Speed as float</returns>
    float GetSpeed();

    /// <summary>
    /// Rotates the ship to given direction
    /// </summary>
    /// <param name="direction">Direction the ship rotates to</param>
    void RotateTowards(Vector3 direction);
}
