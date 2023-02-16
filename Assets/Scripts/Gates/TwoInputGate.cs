using UnityEngine;

public class TwoInputGate : MonoBehaviour, IGate
{
    public enum GateType
    {
        AND,
        NAND,
        NOR,
        OR,
        XNOR,
        XOR
    }

    public GateType type = GateType.AND;

    public ConnectionPoint IN1;
    public ConnectionPoint IN2;
    public ConnectionPoint OUT;

    // Start is called before the first frame update
    private void Start()
    {
        IN1.Outputs.Add(this);
        IN2.Outputs.Add(this);
        UpdateState(0f, 100);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void UpdateState(float updateDelay, int depth = -1)
    {
        if (depth == 0) return;

        if (Level.Testing && updateDelay > 0) return;
        var in1 = IN1.on;
        var in2 = IN2.on;
        var result = false;
        switch (type)
        {
            case GateType.AND:
                result = in1 & in2;
                break;
            case GateType.NAND:
                result = !(in1 & in2);
                break;
            case GateType.NOR:
                result = !(in1 || in2);
                break;
            case GateType.OR:
                result = in1 || in2;
                break;
            case GateType.XNOR:
                result = !(in1 ^ in2);
                break;
            case GateType.XOR:
                result = in1 ^ in2;
                break;
        }

        OUT.SetState(result);
        OUT.UpdateConnected(updateDelay, depth - 1);
    }
}