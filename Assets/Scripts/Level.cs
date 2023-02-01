
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Level : MonoBehaviour
{
    [Serializable] public struct truthEntry
    {
        public List<bool> input;
        public List<bool> expectedOutput;
    }
    public List<ConnectionPoint> inputs = new();
    public List<ConnectionPoint> outputs = new();
    public List<truthEntry> truthTable = new();
    public GameObject textGameObject;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = textGameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
    }

    public bool Test()
    {
        text.text = "";
        bool correct = true;
        // for each element to test in the truth table
        foreach (var truthElement in truthTable)
        {
            text.text += "Input: ";
            // setup the input nodes
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].SetState(truthElement.input[i]);
                text.text += truthElement.input[i] ? "1" : "0";
            }
            
            // update the connections instantly
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].UpdateConnected(0, 100);
            }
            // check if output is correct
            for (int i = 0; i < outputs.Count; i++)
            {
                // // if any mismatch detected, return false
                if (truthElement.expectedOutput[i] != outputs[i].on)
                {
                    correct = false;
                }

                text.text += " | Expected: ";
                foreach (var expectedBit in truthElement.expectedOutput)
                {
                    text.text += expectedBit ? "1" : "0";
                }

                text.text += " | Actual: ";
                foreach (var expectedBit in outputs)
                {
                    text.text += expectedBit.on ? "1" : "0";
                }

                text.text += "\n";
            }
        }

        text.text += correct ? "Level Complete!" : "Uh oh, something's wrong";
        // no mismatches found, must be true.
        return correct;
    }
}
