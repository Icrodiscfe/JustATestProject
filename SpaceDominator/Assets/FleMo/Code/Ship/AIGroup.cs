using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIGroup : MonoBehaviour, IAIGroup {

    GameObject player;
    IAIShip[] iAIShips;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        iAIShips = transform.Cast<Transform>().Select(x => x.GetComponent<IAIShip>()).ToArray();
    }

    void Update()
    {
        bool isInRange = false;
        foreach(IAIShip ship in iAIShips)
        {
            isInRange = CheckIfPlayerInRange();
            
            if(isInRange)
            {
                break;
            }
        }

        if(isInRange)
        {
            SetTarget(player.transform.position);
        }
        else
        {
            SetTarget(null);
        }
    }

    public void SetTarget(Vector3? position)
    {
        foreach(IAIShip child in iAIShips)
        {
            child.SetTarget(position);
        }
    }

    private bool CheckIfPlayerInRange()
    {
        foreach (Transform shipTransform in transform)
        {
            IAIShip ship = shipTransform.GetComponent<IAIShip>();
            float distance = Vector3.Distance(shipTransform.position, player.transform.position);

            if (distance <= ship.GetAISettings().Sight && distance > ship.GetAISettings().MinApproachDistance)
            {
                return true;
            }
        }

        return false;
    }
}
