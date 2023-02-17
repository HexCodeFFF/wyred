using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private static readonly int AnimationPropertyWalkSpeed = Animator.StringToHash("WalkSpeed");
    private static readonly int AnimationPropertyGrounded = Animator.StringToHash("Grounded");
    private static readonly int AnimationPropertyFaceLeft = Animator.StringToHash("FaceLeft");
    private static readonly int AnimationPropertyMoving = Animator.StringToHash("Moving");
    public float speed = 100f;
    public BoxCollider2D groundCollider;
    public float jumpStrength = 35f;
    public float jumpDelay = 0.1f;

    public ConnectingLine connectingPrefab;
    public ConnectionLine connectionPrefab;

    public float animationMovementThreshold = 0.1f;
    public float animationMovementSpeedDivisor = 10f;
    public List<AudioClip> steps;
    public AudioClip unplug;
    public AudioClip plug;
    public AudioClip buzz;

    public float footstepFrequency;

    private Animator _animator;

    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider2D;
    private bool _connect;
    private ConnectingLine _connectingLine;
    private bool _facingLeft;

    private bool _grounded;
    private float _horizontal;
    private float _jump;

    private Rigidbody2D _rigidbody2d;
    private float _timeSinceLastJump;
    private float _vertical;
    private bool _wasGrounded;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        InvokeRepeating(nameof(FootStep), 0, footstepFrequency);
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _jump = Input.GetAxis("Jump");
        _timeSinceLastJump += Time.deltaTime;

        HandleConnection();

        HandleAnimation();
    }


    private void FixedUpdate()
    {
        _grounded = groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && _timeSinceLastJump >= jumpDelay;
        if (_grounded && !_wasGrounded) _audioSource.PlayOneShot(steps[Random.Range(0, steps.Count - 1)], 1f);
        _wasGrounded = _grounded;

        if (_grounded && (_jump > 0 || _vertical > 0))
        {
            // cancel Y velocity
            var vel = _rigidbody2d.velocity;
            vel.y = 0;
            _rigidbody2d.velocity = vel;
            // Impulse force up
            _rigidbody2d.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            _grounded = false;
            _timeSinceLastJump = 0;
        }

        _rigidbody2d.AddForce(new Vector2(_horizontal * speed, 0));
    }

    private ConnectionPoint GetTouchingPoint()
    {
        var touching = new List<Collider2D>();
        var filter = new ContactFilter2D();
        filter.NoFilter();
        // this shit dosent work? huh
        // filter.SetLayerMask(LayerMask.GetMask("Connection Points"));
        // filter.useLayerMask = true;
        _boxCollider2D.OverlapCollider(filter, touching);
        foreach (var coll in touching)
            if (coll.gameObject.layer == LayerMask.NameToLayer("Connection Points"))
                return coll.gameObject.GetComponent<ConnectionPoint>();

        return null;
    }

    private void HandleConnection()
    {
        _connect = Input.GetButtonDown("Fire1");
        if (_connect) // user pushed connect button
        {
            // we're touching a point
            if (_rigidbody2d.IsTouchingLayers(LayerMask.GetMask("Connection Points")))
            {
                var point = GetTouchingPoint();
                if (point)
                {
                    if (_connectingLine) // we're holding a line
                    {
                        if (point.isOutput ^ _connectingLine.point.isOutput) // if only one is output
                        {
                            var otherPoint = _connectingLine.point;
                            Destroy(_connectingLine.gameObject);
                            _connectingLine = null;
                            var line = Instantiate(connectionPrefab);
                            var inpoint = !point.isOutput ? point : otherPoint;
                            var outpoint = point.isOutput ? point : otherPoint;
                            line.input = outpoint;
                            line.output = inpoint;
                            if (inpoint.input)
                            {
                                Destroy(inpoint.input.gameObject);
                                _audioSource.PlayOneShot(unplug);
                            }

                            _audioSource.PlayOneShot(plug);
                            _audioSource.PlayOneShot(buzz);

                            inpoint.input = line;
                            outpoint.Outputs.Add(line);
                            line.UpdateState();
                        }
                    }
                    else // make a new line
                    {
                        _connectingLine = Instantiate(connectingPrefab);
                        _connectingLine.playerCollider = _boxCollider2D;
                        _connectingLine.point = point;
                        _audioSource.PlayOneShot(plug);
                        if (!point.isOutput && point.input)
                        {
                            Destroy(point.input.gameObject);
                            point.input = null;
                            point.SetState(false);
                            _audioSource.PlayOneShot(unplug);
                        }
                    }
                }
            }
            else // no point found
            {
                // destroy held line
                if (_connectingLine)
                {
                    Destroy(_connectingLine.gameObject);
                    _connectingLine = null;
                    _audioSource.PlayOneShot(unplug);
                }
            }
        }
    }

    private void HandleAnimation()
    {
        // facingleft is NOT updated when motion is 0 so we can remember the last state
        if (_horizontal > 0) _facingLeft = false;

        if (_horizontal < 0) _facingLeft = true;

        _animator.SetBool(AnimationPropertyMoving,
            !(Math.Abs(_horizontal) < animationMovementThreshold
              && Math.Abs(_rigidbody2d.velocity.x) < animationMovementThreshold)
        );
        _animator.SetBool(AnimationPropertyGrounded, _grounded);
        _animator.SetBool(AnimationPropertyFaceLeft, _facingLeft);
        _animator.SetFloat(AnimationPropertyWalkSpeed,
            Math.Min(1, Math.Max(Math.Abs(_horizontal),
                Math.Abs(_rigidbody2d.velocity.x / animationMovementSpeedDivisor)))
        );
    }

    private void FootStep()
    {
        if (!(Math.Abs(_horizontal) < animationMovementThreshold
              && Math.Abs(_rigidbody2d.velocity.x) < animationMovementThreshold) && _grounded)
            _audioSource.PlayOneShot(steps[Random.Range(0, steps.Count - 1)], 0.5f);
    }
    // private bool IsGrounded()
    // {
    //     Physics2D.OverlapBox(_rigidbody2d.position - _rigidbody2d.)
    // }
}