using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// a point that can be connected to a line or something else
public class ConnectionPoint : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;
    [FormerlySerializedAs("is_output")] public bool isOutput;
    // connections
    public List<ConnectionLine> outputs = new List<ConnectionLine>();
    public ConnectionLine input;
    public bool on { get; private set; } = false;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        SetState(false);
    }

    public void UpdateConnectedLines()
    {
        foreach (var line in outputs)
        {
            line.UpdateState();
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