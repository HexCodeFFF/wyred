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
        _renderer.startColor =  Color.black;
        _renderer.endColor = Color.black;
    }


    public void UpdateState()
    {
        output.SetState(input.on);//TODO: there's gonna be a recursion issue here that will totally freeze the game
        _renderer.startColor = input.on ? Color.yellow : Color.black;
        _renderer.endColor = _renderer.startColor;
    }
}