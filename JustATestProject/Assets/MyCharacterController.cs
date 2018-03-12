using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class MyCharacterController : MonoBehaviour {
    [SerializeField]
    private int _JumpsInAir = 0;
    [SerializeField]
    private bool _CanOnlyMoveOnGround = false;

    private CharacterMotor characterMotor;
    private int jumpInAirDone;

    void Start()
    {
        characterMotor = GetComponent<CharacterMotor>();
    }

	public void Move(float density)
    {
        //add colllectable speed multiplier here
        if (characterMotor.IsOnGround || _CanOnlyMoveOnGround == false)
        {
            characterMotor.Move(density);
        }
    }

    void Update()
    {
        if(characterMotor.IsOnGround)
        {
            jumpInAirDone = 0;
        }
    }

    public void Jump()
    {
        //add colllectable jump multiplier here
        if (characterMotor.IsOnGround)
        {
            characterMotor.Jump(1);
        }
        else if(jumpInAirDone < _JumpsInAir)
        {
            jumpInAirDone++;
            characterMotor.Jump(1);
        }
    }
}
