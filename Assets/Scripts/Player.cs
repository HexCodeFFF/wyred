using System;
using Unity.Mathematics;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public BoxCollider2D ground_collider;
    public float jump_strength = 3f;
    public float jump_delay = 0.2f;

    private Rigidbody2D _rigidbody2d;
    private float _horizontal;
    private float _vertical;
    private float _jump;
    private float time_since_last_jump = 0;

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
        time_since_last_jump += Time.deltaTime;
    }


    private void FixedUpdate()
    {
        _grounded = ground_collider.IsTouchingLayers(LayerMask.GetMask("Ground")) && time_since_last_jump >= jump_delay;
        if (_grounded && (_jump > 0 || _vertical > 0))
        {
            _rigidbody2d.AddForce(new Vector2(0, jump_strength), ForceMode2D.Impulse);
            _grounded = false;
            time_since_last_jump = 0;
        }

        _rigidbody2d.AddForce(new Vector2((_horizontal) * speed, 0));
    }

    // private bool IsGrounded()
    // {
    //     Physics2D.OverlapBox(_rigidbody2d.position - _rigidbody2d.)
    // }
}