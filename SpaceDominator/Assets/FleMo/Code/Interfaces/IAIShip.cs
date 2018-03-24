using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIShip
{
    void SetTarget(Vector3? target);
    void SetTarget(Rigidbody rigidbody);
    AISettings GetAISettings();
}
