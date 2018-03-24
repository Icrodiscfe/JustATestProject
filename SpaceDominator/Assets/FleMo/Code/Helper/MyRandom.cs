using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyRandom
{
    public static Vector3 RandomVector3InCircle(float radius)
    {
        float angle = Random.Range(0, 360);
        float range = Random.Range(0, radius);

        return new Vector3(
            radius * Mathf.Cos(angle),
            0,
            radius * Mathf.Sin(angle));
    }

    public static Vector3 RandomVector3InCircle(float minRadius, float maxRadius)
    {
        float angle = Random.Range(0, 360);
        float range = Random.Range(minRadius, maxRadius);

        return new Vector3(
            range * Mathf.Cos(angle),
            0,
            range * Mathf.Sin(angle));
    }
}
