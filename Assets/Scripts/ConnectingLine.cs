using System;
using UnityEngine;

// a Line between a point and the player, a connection that the Player is currently making.
public class ConnectingLine : MonoBehaviour, ILine
{
    public ConnectionPoint point;
    public Player player;
    private LineRenderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<LineRenderer>();
        _renderer.startColor =  Color.black;
        _renderer.endColor = Color.black;
    }

    private void Update()
    {
        _renderer.SetPositions(new[] { player.transform.position, point.transform.position });
    }
}