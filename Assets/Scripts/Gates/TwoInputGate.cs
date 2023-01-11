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
        XOR,
    }

    public GateType type = GateType.AND;

    public ConnectionPoint IN1;
    public ConnectionPoint IN2;
    public ConnectionPoint OUT;

    // Start is called before the first frame update
    void Start()
    {
        IN1.outputs.Add(this);
        IN2.outputs.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateState()
    {
        bool in1 = IN1.on;
        bool in2 = IN2.on;
        bool result = false;
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
    }
}
