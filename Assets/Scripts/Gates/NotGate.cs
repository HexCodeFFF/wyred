using UnityEngine;

public class NotGate : MonoBehaviour, IGate
{
    public ConnectionPoint IN;
    public ConnectionPoint OUT;

    void Start()
    {
        IN.Outputs.Add(this);
        UpdateState();
    }

    public void UpdateState(float updateDelay = 0.1f, int depth = 100)
    {
        if (depth == 0)
        {
            return;
        }

        if (Level.Testing && updateDelay > 0) return;

        OUT.SetState(!IN.on);
        OUT.UpdateConnected(updateDelay, depth - 1);
    }
}