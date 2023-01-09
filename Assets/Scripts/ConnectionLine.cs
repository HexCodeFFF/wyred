using UnityEngine;

// a line actively connecting two ConnectionPoints
public class ConnectionLine : MonoBehaviour, Line
{
    public ConnectionPoint input;
    public ConnectionPoint output;
    private LineRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<LineRenderer>();
        _renderer.SetPositions(new[] { input.transform.position, output.transform.position });
        _renderer.startColor = _renderer.endColor = Color.black;
    }


    public void UpdateState()
    {
        output.SetState(input.on);
        _renderer.startColor = _renderer.endColor = input.on ? Color.yellow : Color.black;
    }
}