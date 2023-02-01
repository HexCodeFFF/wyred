using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Level : MonoBehaviour
{
    [Serializable]
    public struct truthEntry
    {
        public List<bool> input;
        public List<bool> expectedOutput;
    }

    public String levelName;
    public int levelNumber;
    public List<ConnectionPoint> inputs = new();
    public List<ConnectionPoint> outputs = new();
    public List<truthEntry> truthTable = new();
    public GameObject textGameObject;
    private TextMeshProUGUI text;
    private float _textTimeout;
    public float updateSpeed = -1;
    private int _cycleState = 0;

    private int TwoToThe(int the)
    {
        int res = 1;
        for (int i = 0; i < the; i++)
        {
            res *= 2;
        }

        return res;
    }
    private void Start()
    {
        text = textGameObject.GetComponent<TextMeshProUGUI>();
        text.text = $"Level {levelNumber}: {levelName}";
        Debug.Log(text);
        _textTimeout = 5f;
        if (updateSpeed > 0)
        {
            InvokeRepeating("UpdateInputs", updateSpeed, updateSpeed);
        }
    }

    private void UpdateInputs()
    {
        // cycle through all possible iterations of the inputs via binary
        _cycleState++;
        if (_cycleState >= TwoToThe(inputs.Count))
        {
            _cycleState = 0;
        }

        for (int i = 0; i < inputs.Count; i++)
        {
            // for each input, check if its corresponding binary digit is 1 on the cyclestate
            inputs[i].SetState((TwoToThe(i) & _cycleState) > 0);
            inputs[i].UpdateConnected();
        }
        
    }
    private void Update()
    {
        _textTimeout -= Time.deltaTime;
        if (_textTimeout <= 0)
        {
            text.text = "";
        }
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
                bool thisoneiscorrect = truthElement.expectedOutput[i] == outputs[i].on;
                // // if any mismatch detected, return false
                text.text += " | ";
                if (!thisoneiscorrect)
                {
                    correct = false;
                    text.text += "<color=red>";
                }

                text.text += "Expected: ";
                foreach (var expectedBit in truthElement.expectedOutput)
                {
                    text.text += expectedBit ? "1" : "0";
                }

                text.text += " | Actual: ";

                foreach (var expectedBit in outputs)
                {
                    text.text += expectedBit.on ? "1" : "0";
                }

                if (!thisoneiscorrect)
                {
                    text.text += "</color>";
                }

                text.text += "\n";
            }
        }

        text.text += correct ? "Level Complete!" : "Uh oh, something's wrong";
        // no mismatches found, must be true.
        _textTimeout = 5f;
        return correct;
    }
}