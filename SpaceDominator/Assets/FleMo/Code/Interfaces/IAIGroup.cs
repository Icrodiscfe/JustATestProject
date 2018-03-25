using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIGroup
{
    /// <summary>
    /// Returns the speed of the group if they are not in fight
    /// </summary>
    /// <returns></returns>
    float GetGroupSpeed();

    /// <summary>
    /// Return alert state of the group
    /// </summary>
    /// <returns>The actual aler state of the group</returns>
    IAIGroupStatics.AlertStates GetAlertState();
}


public static class IAIGroupStatics
{
    public enum AlertStates { None, Alarm }
}