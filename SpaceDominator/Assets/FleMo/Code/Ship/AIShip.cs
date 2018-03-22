using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IShipController))]
public class AIShip : MonoBehaviour, IAIShip
{
    [SerializeField]
    AISettings _AiSetting;

    Vector3? flyTo = null;
    IShipController shipController;

    void Start()
    {
        shipController = GetComponent<IShipController>();
    }

    void Update()
    {
        FlyTo();
    }

    private void FlyTo()
    {
        if (!flyTo.HasValue)
        {
            shipController.SlowDown();
            return;
        }

        Vector3 direction = (flyTo.Value - this.transform.position).normalized;
        shipController.Move(direction);
    }

    public void SetTarget(Vector3? target)
    {
        flyTo = target;
    }

    public AISettings GetAISettings()
    {
        return _AiSetting;
    }
}
