using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IShipController)),
    RequireComponent(typeof(Rigidbody))]
public class AIShip : MonoBehaviour, IAIShip
{
    [SerializeField]
    AISettings _AiSetting;

    Vector3? flyToVector3 = null;
    Rigidbody flyToRigidbody = null;
    IShipController shipController;
    Rigidbody myRigidbody;

    void Start()
    {
        shipController = GetComponent<IShipController>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        FlyTo();
    }

    private void FlyTo()
    {
        if (!flyToVector3.HasValue && flyToRigidbody == null)
        {
            shipController.SlowDown();
            return;
        }

        Vector3? direction = null;

        if (flyToVector3.HasValue)
        {
            direction = (flyToVector3.Value - this.transform.position).normalized;
        }
        else if(flyToRigidbody != null)
        {
            Vector3 vr = flyToRigidbody.velocity - myRigidbody.velocity;
            Vector3 sr = flyToRigidbody.position - myRigidbody.position;
            float tc = sr.magnitude / vr.magnitude;
            Vector3 st = flyToRigidbody.position + (flyToRigidbody.velocity * tc);
            direction = (st - this.transform.position).normalized;
        }
        
        if (direction.HasValue)
        {
            shipController.Move(direction.Value);
        }
    }

    public void SetTarget(Vector3? target)
    {
        flyToRigidbody = null;
        flyToVector3 = target;
    }

    public void SetTarget(Rigidbody targetRigidbody)
    {
        flyToVector3 = null;
        flyToRigidbody = targetRigidbody;
    }

    public AISettings GetAISettings()
    {
        return _AiSetting;
    }
}