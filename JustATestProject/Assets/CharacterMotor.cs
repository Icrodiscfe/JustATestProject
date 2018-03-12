using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMotor : MonoBehaviour {
    [SerializeField]
    private float _MoveSpeed = 3;
    [SerializeField]
    private float _JumpForce = 250;

    private Rigidbody2D myRigidbody;
    private Vector2 velocity = new Vector2();
    private Collider2D collider = null;

    public bool IsOnGround
    {
        get
        {
            return Physics2D.Raycast(myRigidbody.position, Vector2.down, collider.bounds.size.y + 0.1f);
        }
    }

	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
	}

    public void Move(float multiplier)
    {
        //prevents falling :(
        Vector2 speed = Vector2.right * multiplier * Time.deltaTime * _MoveSpeed;

        this.myRigidbody.MovePosition(myRigidbody.position + speed);
    }

    public void Jump(float multiplier)
    {
        this.myRigidbody.AddForce(Vector2.up * _JumpForce, ForceMode2D.Impulse);
    }
}
