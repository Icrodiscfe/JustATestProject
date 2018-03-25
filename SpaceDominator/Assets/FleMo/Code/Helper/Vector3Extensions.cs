using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 GetDirection(this Vector3 myV3, Vector3 target)
    {
        return (target - myV3).normalized;
    }
	
}
