using System;
using System.Collections;
using UnityEngine;

// a line actively connecting two ConnectionPoints
public class ConnectionLine : MonoBehaviour, ILine, IUpdatable
{
    public ConnectionPoint input;
    public ConnectionPoint output;
    private LineRenderer _renderer;
    public float defaultUpdateDelay = 0.0f;
    private bool _waitingToUpdate = false;

    private void Start()
    {
        _renderer = GetComponent<LineRenderer>();
        _renderer.SetPositions(new[] { input.transform.position, output.transform.position });
        _renderer.startColor =  Color.black;
        _renderer.endColor = Color.black;
        UpdateState();
    }

    public void UpdateState(float updateDelay=0.1f, int depth=100)
    {
        if (depth <= 0)
        {
            return;
        }
        _renderer.startColor = input.on ? Color.yellow : Color.black;
        if (!_waitingToUpdate)
        {
            _waitingToUpdate = true;
            if (updateDelay > 0)
            {
                StartCoroutine(WaitToUpdate(updateDelay, depth));
            }
            else
            {
                ReallyUpdate(updateDelay, depth);
            }
            
        }
    }
    IEnumerator WaitToUpdate(float updateDelay, int depth=100)
    {
        // small intentional delay to allow loops to not break the game
        yield return new WaitForSeconds(updateDelay);
        ReallyUpdate(updateDelay, depth);
    }

    void ReallyUpdate(float updateDelay, int depth = 100)
    {
        _waitingToUpdate = false;
        output.SetState(input.on);
        output.UpdateConnected(updateDelay, depth-1);
        // _renderer.startColor = input.on ? Color.yellow : Color.black;
        _renderer.endColor = _renderer.startColor;
    }

    public void OnDestroy()
    {
        input.outputs.Remove(this);
    }
}