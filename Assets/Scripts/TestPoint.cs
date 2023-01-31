
using System;
using UnityEngine;

public class TestPoint : MonoBehaviour
{
    public float delay = 1f;
    private ConnectionPoint _point;
    public void Start()
    {
        _point = GetComponent<ConnectionPoint>();
        InvokeRepeating("TogglePoint", 1, 1);    

    }


    private void TogglePoint()
    {
        _point.Toggle();
        _point.UpdateConnected();
    }
}
