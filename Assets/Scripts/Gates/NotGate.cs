using UnityEngine;

public class NotGate : MonoBehaviour, IGate
{
    public ConnectionPoint IN;
    public ConnectionPoint OUT;

    public void UpdateState()
    {
        OUT.SetState(!IN.on);
    }
}