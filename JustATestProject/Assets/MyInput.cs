using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class MyInput : MonoBehaviour {
    private MyCharacterController characterController;

	void Start ()
    {
        characterController = GetComponent<MyCharacterController>();
	}
	
	void Update ()
    {
        if(!Mathf.Approximately(Input.GetAxis("Horizontal"), 0))
        {
            characterController.Move(Input.GetAxis("Horizontal"));
        }

        if(Input.GetButtonDown("Jump"))
        {
            characterController.Jump();
        }
	}
}
