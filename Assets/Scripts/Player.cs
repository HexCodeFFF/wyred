using System;
using Unity.Mathematics;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public BoxCollider2D ground_collider;
    public float jump_strength = 3;

    private Rigidbody2D _rigidbody2d;
    private float _horizontal;
    private float _vertical;
    private float _jump;

    private bool _grounded = false;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _jump = Input.GetAxis("Jump");
    }


    private void FixedUpdate()
    {
        // get existing velocity

        // if already fast for some reason, dont decrease speed
        // otherwise, move
        // if (_horizontal < 0)
        // {
        //     vel.x = Math.Min(vel.x, _horizontal * speed);
        // }
        // else
        // {
        //     vel.x = Math.Max(vel.x, _horizontal * speed);
        // }
        // _rigidbody2d.AddForce(new Vector2(_horizontal * speed, 0));
        //
        Vector2 vel = _rigidbody2d.velocity;
        _grounded = ground_collider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (_grounded && (_jump > 0 || _vertical > 0))
        {
            vel.y = jump_strength;
        }

        if (_horizontal != 0)
        {
            vel.x = (_horizontal) * speed;
        }

        _rigidbody2d.velocity = vel;
    }

    // private bool IsGrounded()
    // {
    //     Physics2D.OverlapBox(_rigidbody2d.position - _rigidbody2d.)
    // }
}