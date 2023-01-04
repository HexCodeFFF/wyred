using System;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;
    public bool InitialState = false;
    public bool on { get; private set; } = false;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        SetState(InitialState);
    }

    void Off()
    {
        _renderer.sprite = offSprite;
        on = false;
    }

    void On()
    {
        _renderer.sprite = onSprite;
        on = true;
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