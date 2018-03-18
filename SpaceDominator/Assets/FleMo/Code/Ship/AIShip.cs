using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IShipController))]
public class AIShip : MonoBehaviour {

    [SerializeField]
    AISettings _AiSetting;

    GameObject player;
    IShipController shipController;

	void Start ()
    {
        shipController = GetComponent<IShipController>();
        player = GameObject.FindGameObjectWithTag(Tags.Player);
	}

    void Update()
    {
        CheckIfPlayerInRange();
    }

    private void CheckIfPlayerInRange()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance <= _AiSetting.Sight && distance > _AiSetting.MinApproachDistance)
        {
            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            shipController.Move(direction);
        }
        else
        {
            shipController.SlowDown();
        }
    }
}
