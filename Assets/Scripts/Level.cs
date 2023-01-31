
using System;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    [Serializable] public struct truthEntry
    {
        public List<bool> input;
        public List<bool> expectedOutput;
    }
    public List<ConnectionPoint> inputs = new List<ConnectionPoint>();
    public List<ConnectionPoint> outputs = new List<ConnectionPoint>();
    public List<truthEntry> truthTable = new List<truthEntry>();

    public bool Test()
    {
        // for each element to test in the truth table
        foreach (var truthElement in truthTable)
        {
            // setup the input nodes
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].SetState(truthElement.input[i]);
            }
            // update the connections instantly
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].UpdateConnected(0);
            }
            // check if output is correct
            for (int i = 0; i < outputs.Count; i++)
            {
                // if any mismatch detected, return false
                if (truthElement.expectedOutput[i] != outputs[i].on)
                {
                    return false;
                }
            }
        }
        // no mismatches found, must be true.
        return true;
    }
}
