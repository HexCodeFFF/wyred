using System.Collections;
using UnityEngine;

// a line actively connecting two ConnectionPoints
public class ConnectionLine : MonoBehaviour, ILine, IUpdatable
{
    public ConnectionPoint input;
    public ConnectionPoint output;
    public float defaultUpdateDelay = 0.0f;
    private LineRenderer _renderer;
    private bool _waitingToUpdate = false;

    private void Start()
    {
        _renderer = GetComponent<LineRenderer>();
        _renderer.SetPositions(new[] { input.transform.position, output.transform.position });
        _renderer.startColor = Color.black;
        _renderer.endColor = Color.black;
        UpdateState();
    }

    public void OnDestroy()
    {
        input.Outputs.Remove(this);
    }

    public void UpdateState(float updateDelay = 0.1f, int depth = -1)
    {
        if (depth == 0)
        {
            return;
        }

        if (Level.Testing && updateDelay > 0) return;
        _renderer.startColor = input.on ? Color.yellow : Color.black;
        if (updateDelay > 0)
        {
            if (!_waitingToUpdate)
            {
                _waitingToUpdate = true;
                StartCoroutine(WaitToUpdate(input.on, updateDelay, depth));
            }
        }
        else
        {
            ReallyUpdate(input.on, updateDelay, depth);
        }
    }

    IEnumerator WaitToUpdate(bool state, float updateDelay, int depth = 100)
    {
        // small intentional delay to allow loops to not break the game
        yield return new WaitForSeconds(updateDelay);
        ReallyUpdate(state, updateDelay, depth);
    }

    void ReallyUpdate(bool state, float updateDelay, int depth = 100)
    {
        _waitingToUpdate = false;
        if (Level.Testing && updateDelay > 0) return;

        output.SetState(state);
        output.UpdateConnected(updateDelay, depth - 1);
        // _renderer.startColor = input.on ? Color.yellow : Color.black;
        _renderer.endColor = _renderer.startColor;
    }
}