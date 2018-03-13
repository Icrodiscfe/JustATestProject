using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipMotor {
    /// <summary>
    /// Moves the rigidbody to the direction. Must be called constantinous. If no movement is wished pass Vector3.zero.
    /// </summary>
    /// <param name="direction">The normalized direction the ship should move to.</param>
    void Move(Vector3 direction);

    /// <summary>
    /// Slows down the ship by brake force.
    /// </summary>
    void SlowDown();
}
