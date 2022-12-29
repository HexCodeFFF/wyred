using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    private float _horizontal; 
    private float _vertical;
    public float speed = 3.0f;
    private bool _grounded = false;
    
    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    
    private void FixedUpdate()
    {
        Vector2 position = _rigidbody2d.position;
        position.x += speed * _horizontal * Time.deltaTime;
        position.y += speed * _vertical * Time.deltaTime;

        _rigidbody2d.MovePosition(position);
    }
}
