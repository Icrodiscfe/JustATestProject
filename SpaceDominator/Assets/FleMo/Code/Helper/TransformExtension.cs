using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static bool IsInFrontOfMe(this Transform tr, Vector3 target)
    {
        Vector3 deltaPos = tr.position - target;
        var x = deltaPos.x * tr.forward.x;
        var y = deltaPos.y * tr.forward.y;
        var z = deltaPos.z * tr.forward.z;

        return x + z + y < 0;
    }
}
