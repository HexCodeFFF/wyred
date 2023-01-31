
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
}
