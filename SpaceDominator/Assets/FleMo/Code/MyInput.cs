using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IShipController))]
public class MyInput : MonoBehaviour
{
    private IShipController shipController;

    void Start()
    {
        shipController = GetComponent<IShipController>();
    }

	void Update ()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        shipController.Move(movement);
    }
}
