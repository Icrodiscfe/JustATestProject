using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensions
{
    public static Vector3 GetInterceptionPoint(this Rigidbody myRigidbody, Rigidbody otherRigidbody, bool amIPredator)
    {
        Rigidbody predator;
        Rigidbody pray;

        if(amIPredator)
        {
            pray = otherRigidbody;
            predator = myRigidbody;
        }
        else
        {
            pray = myRigidbody;
            predator = otherRigidbody;
        }

        Vector3 vr = pray.velocity - predator.velocity;
        Vector3 sr = pray.position - predator.position;
        float tc = sr.magnitude / vr.magnitude;
        Vector3 st = pray.position + (pray.velocity * tc);
        Vector3 dir = (st - predator.position).normalized;

        return dir;
    }

    public static Vector3 GetCorrectedInterceptionPoint(this Rigidbody myRigidbody, Rigidbody otherRigidbody, bool amIPredator)
    {
        Rigidbody predator;
        Rigidbody pray;

        if (amIPredator)
        {
            pray = otherRigidbody;
            predator = myRigidbody;
        }
        else
        {
            pray = myRigidbody;
            predator = otherRigidbody;
        }

        Vector3 vr = pray.velocity - predator.velocity;
        Vector3 sr = pray.position - predator.position;
        float tc = sr.magnitude / vr.magnitude;
        Vector3 st = pray.position + (pray.velocity * tc);
        Vector3 dir = (st - predator.position).normalized;

        if(predator.transform.IsInFrontOfMe(dir + predator.transform.position) && !predator.transform.IsInFrontOfMe(pray.transform.position))
        {
            dir = predator.position.GetDirection(pray.position);
        }

        return dir;
    }
}
