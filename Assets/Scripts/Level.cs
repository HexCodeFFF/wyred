using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public bool autoLabelPoints = true;
    public List<ConnectionPoint> inputs = new();
    public List<ConnectionPoint> outputs = new();
    public List<truthEntry> truthTable = new();
    public GameObject textGameObject;
    private TextMeshProUGUI _text;
    public bool persistentText = false;
    private float _textTimeout;
    public float updateSpeed = -1;
    private int _cycleState = 0;
    public bool complete = false;


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
        _text = textGameObject.GetComponent<TextMeshProUGUI>();
        _text.text = $"Level {levelNumber}: {levelName}";
        PlayerPrefs.SetInt("Level", levelNumber);
        _textTimeout = 5f;
        if (updateSpeed > 0)
        {
            InvokeRepeating(nameof(UpdateInputs), updateSpeed, updateSpeed);
        }


        for (int i = 0; i < inputs.Count; i++)
        {
            if (autoLabelPoints)
            {
                inputs[i].SetText($"Input {i + 1}");
            }

            inputs[i].isOutput = true;
        }

        for (int i = 0; i < outputs.Count; i++)
        {
            if (autoLabelPoints)
            {
                outputs[i].SetText($"Output {i + 1}");
            }

            outputs[i].isOutput = false;
        }
    }

    private void UpdateInputs()
    {
        //TODO: suspend updates while testing truth table?

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
        if (_textTimeout <= 0 && !persistentText)
        {
            _text.text = "";
        }
    }

    public bool Test()
    {
        if (complete)
        {
            return true;
        }

        _text.text = "";
        bool correct = true;
        // for each element to test in the truth table
        foreach (var truthElement in truthTable)
        {
            _text.text += "Input: ";
            // setup the input nodes
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].SetState(truthElement.input[i]);
                _text.text += truthElement.input[i] ? "1" : "0";
            }

            // update the connections instantly
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].UpdateConnected(0, 100);
            }

            // check if output is correct
            bool thisoneiscorrect = true;
            for (int i = 0; i < outputs.Count; i++)
            {
                if (truthElement.expectedOutput[i] != outputs[i].on)
                {
                    thisoneiscorrect = false;
                    break;
                }
            }

            _text.text += " | ";
            if (!thisoneiscorrect)
            {
                correct = false;
                _text.text += "<color=red>";
            }

            _text.text += "Expected: ";
            foreach (var expectedBit in truthElement.expectedOutput)
            {
                _text.text += expectedBit ? "1" : "0";
            }

            _text.text += " | Actual: ";

            foreach (var expectedBit in outputs)
            {
                _text.text += expectedBit.on ? "1" : "0";
            }

            if (!thisoneiscorrect)
            {
                _text.text += "</color>";
            }

            _text.text += "\n";
        }

        _text.text += correct ? "Level Complete!" : "Check your wiring.";
        _textTimeout = 5f;
        if (correct)
        {
            Invoke(nameof(NextLevel), 3);
            complete = true;
        }

        return correct;
    }

    public void NextLevel()
    {
        persistentText = true;
        _text.text = "Loading...";
        if (levelNumber + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelNumber + 1);
        }
        else
        {
            _text.text = "You Win!";
        }
    }

    public void ButtonState(bool state)
    {
    }
}