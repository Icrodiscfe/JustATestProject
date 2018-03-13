using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipController {
    /// <summary>
    /// Calls the ship motor to move the ship to given direction
    /// </summary>
    /// <param name="direction">Normalized direction the ship should move to</param>
    void Move(Vector3 direction);
}
