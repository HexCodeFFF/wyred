using UnityEngine;

// a Line between a point and the player, a connection that the Player is currently making.
public class ConnectingLine : MonoBehaviour, ILine
{
    public ConnectionPoint point;
    public BoxCollider2D playerCollider;
    private LineRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<LineRenderer>();
        _renderer.startColor = Color.black;
        _renderer.endColor = Color.black;
    }

    private void Update()
    {
        _renderer.SetPositions(new[] { playerCollider.bounds.center, point.transform.position });
    }
}