using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMotor : MonoBehaviour {
    [SerializeField]
    private float _Acceleration = 3;
    [SerializeField]
    private float _MaxSpeed = 2;
    [SerializeField]
    private float _JumpForce = 250;
    [SerializeField]
    private float _CharacterHeight = 2;

    private Rigidbody2D myRigidbody;
    private Vector2 velocity = new Vector2();
    private Collider2D myCollider = null;

    public bool IsOnGround
    {
        get
        {
            return Physics2D.Raycast(myRigidbody.position, Vector2.down, _CharacterHeight / 2 + 0.1f);
        }
    }

	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
	}

    public void Move(float multiplier)
    {
        Vector2 speed = Vector2.right * multiplier * Time.deltaTime * _Acceleration * 10000;

        myRigidbody.AddForce(myRigidbody.position + speed);

        if(myRigidbody.velocity.x > _MaxSpeed)
        {
            myRigidbody.velocity = new Vector2(_MaxSpeed, myRigidbody.velocity.y);
        }
        else if (myRigidbody.velocity.x < -_MaxSpeed)
        {
            myRigidbody.velocity = new Vector2(-_MaxSpeed, myRigidbody.velocity.y);

        }
    }

    public void Jump(float multiplier)
    {
        myRigidbody.AddForce(Vector2.up * _JumpForce, ForceMode2D.Impulse);
    }
}
