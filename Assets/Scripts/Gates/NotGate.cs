using UnityEngine;

public class NotGate : MonoBehaviour, IGate
{
    public ConnectionPoint IN;
    public ConnectionPoint OUT;
    
    void Start()
    {
        IN.outputs.Add(this);
        UpdateState();
    }
    public void UpdateState()
    {
        OUT.SetState(!IN.on);
    }
}