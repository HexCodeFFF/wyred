using UnityEngine;

public class NotGate : MonoBehaviour, IGate
{
    public ConnectionPoint IN;
    public ConnectionPoint OUT;
    
    void Start()
    {
        IN.outputs.Add(this);
        UpdateState(0f, 100);
    }
    public void UpdateState(float updateDelay, int depth=100)
    {
        if (depth <= 0)
        {
            return;
        }
        OUT.SetState(!IN.on);
        OUT.UpdateConnected(updateDelay, depth-1);
    }
}