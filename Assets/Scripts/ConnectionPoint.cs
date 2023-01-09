using System;
using UnityEngine;
// a point that can be connected to a line or something else
public class ConnectionPoint : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;
    public bool is_output;
    public ConnectionLine[] connections = {};
    public bool on { get; private set; } = false;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        SetState(false);
    }

    void UpdateConnectedLines()
    {
        if (is_output)
        {
            foreach (var line in connections)
            {
                line.UpdateState();
            }
        }
    }
    void Off()
    {
        _renderer.sprite = offSprite;
        on = false;
        UpdateConnectedLines();
    }

    void On()
    {
        _renderer.sprite = onSprite;
        on = true;
        UpdateConnectedLines();
    }

    public void SetState(bool state)
    {
        if (state)
        {
            On();
        }
        else
        {
            Off();
        }
    }
}