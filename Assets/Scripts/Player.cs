using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    public float speed = 100f;
    public BoxCollider2D groundCollider;
    public float jumpStrength = 35f;
    public float jumpDelay = 0.1f;

    public ConnectingLine connectingPrefab;
    public ConnectionLine connectionPrefab;

    private Rigidbody2D _rigidbody2d;
    private BoxCollider2D _boxCollider2D;
    private float _horizontal;
    private float _vertical;
    private float _jump;
    private bool _connect;
    private ConnectingLine _connectingLine = null;
    private float _timeSinceLastJump = 0;

    private bool _grounded = false;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private ConnectionPoint GetTouchingPoint()
    {
        List<Collider2D> touching = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        // this shit dosent work? huh
        // filter.SetLayerMask(LayerMask.GetMask("Connection Points"));
        // filter.useLayerMask = true;
        _boxCollider2D.OverlapCollider(filter, touching);
        foreach (var coll in touching)
        {
            if (coll.gameObject.layer == LayerMask.NameToLayer("Connection Points"))
            {
                return coll.GetComponentInParent<ConnectionPoint>();
            }
        }

        return null;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _jump = Input.GetAxis("Jump");
        _timeSinceLastJump += Time.deltaTime;

        _connect = Input.GetButtonDown("Fire1");
        if (_connect // user pushed connect button
            && _rigidbody2d.IsTouchingLayers(LayerMask.GetMask("Connection Points")) // we're touching a point
           )
        {
            ConnectionPoint point = GetTouchingPoint();
            if (point)
            {
                if (_connectingLine) // we're holding a line
                {
                    if (point.isOutput ^ _connectingLine.point.isOutput) // if only one is output
                    {
                        ConnectionPoint otherPoint = _connectingLine.point;
                        Destroy(_connectingLine.gameObject);
                        _connectingLine = null;
                        ConnectionLine line = Instantiate(connectionPrefab);
                        ConnectionPoint inpoint = !point.isOutput ? point : otherPoint;
                        ConnectionPoint outpoint = point.isOutput ? point : otherPoint;
                        line.input = inpoint;
                        line.output = outpoint;
                        if (inpoint.input)
                        {
                            Destroy(inpoint.input.gameObject);
                            inpoint.input = line;
                        }
                        outpoint.outputs.Add(line);
                        line.UpdateState();
                    }
                }
                else // make a new line
                {
                    _connectingLine = Instantiate(connectingPrefab);
                    _connectingLine.player = this;
                    _connectingLine.point = point;
                }
            }
        }
    }


    private void FixedUpdate()
    {
        _grounded = groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && _timeSinceLastJump >= jumpDelay;
        if (_grounded && (_jump > 0 || _vertical > 0))
        {
            _rigidbody2d.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            _grounded = false;
            _timeSinceLastJump = 0;
        }

        _rigidbody2d.AddForce(new Vector2((_horizontal) * speed, 0));
    }

    // private bool IsGrounded()
    // {
    //     Physics2D.OverlapBox(_rigidbody2d.position - _rigidbody2d.)
    // }
}