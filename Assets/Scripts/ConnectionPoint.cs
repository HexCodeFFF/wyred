using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

// a point that can be connected to a line or something else
public class ConnectionPoint : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;
    [FormerlySerializedAs("is_output")] public bool isOutput;
    // connections
    public List<IUpdatable> Outputs = new List<IUpdatable>();
    public ConnectionLine input;
    public bool on { get; private set; } = false;
    private SpriteRenderer _renderer;
    [FormerlySerializedAs("Text")] [FormerlySerializedAs("_text")] public TextMeshProUGUI text;
    public String setText;
    private bool _initialized = false;
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        if (setText != null && text != null)
        {
            text.SetText(setText);
        }
        SetState(false);
        UpdateConnected(0f);
        _initialized = true;
    }

    public void SetText(String txt)
    {
        if (_initialized && text)
        {
            text.SetText(txt);
        }
        else
        {
            setText = txt;
        }
    }

    public void UpdateConnected(float updateDelay = 0.1f, int depth=-1)
    {
        if (depth == 0)
        {
            return;
        }
        foreach (var line in Outputs)
        {
            line.UpdateState(updateDelay, depth-1);
        }
    }

    void Off()
    {
        _renderer.sprite = offSprite;
        on = false;
        if (text)
        {
            text.color = Color.white;
        }
    }

    void On()
    {
        _renderer.sprite = onSprite;
        on = true;
        if (text)
        {
            text.color = Color.black;
        }
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

    public void Toggle()
    {
        SetState(!on);
    }
}