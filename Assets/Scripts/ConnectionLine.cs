using System.Collections;
using UnityEngine;

// a line actively connecting two ConnectionPoints
public class ConnectionLine : MonoBehaviour, Line, IUpdatable
{
    public ConnectionPoint input;
    public ConnectionPoint output;
    private LineRenderer _renderer;
    public float updateDelay = 0.0f;
    private bool _waitingToUpdate = false;

    private void Start()
    {
        _renderer = GetComponent<LineRenderer>();
        _renderer.SetPositions(new[] { input.transform.position, output.transform.position });
        _renderer.startColor =  Color.black;
        _renderer.endColor = Color.black;
    }

    public void UpdateState()
    {
        _renderer.startColor = input.on ? Color.yellow : Color.black;
        if (!_waitingToUpdate)
        {
            
            _waitingToUpdate = true;
            StartCoroutine(ReallyUpdate());
        }
    }
    public IEnumerator ReallyUpdate()
    {
        // small intentional delay to allow loops to not break the game
        yield return new WaitForSeconds(updateDelay);
        _waitingToUpdate = false;
        output.SetState(input.on);
        _renderer.startColor = input.on ? Color.yellow : Color.black;
        _renderer.endColor = _renderer.startColor;
    }
}