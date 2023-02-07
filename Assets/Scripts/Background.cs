using System;
using Cinemachine;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float parallax = 0.1f;
    public Transform camera;
    private Transform _transform;
    private Vector3 _startpos;

    public CinemachineBrain cineBrain;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startpos = _transform.position;
    }

    private void Update()
    {
        Vector3 transformpos = camera.position * parallax + _startpos;
        transformpos.z = _startpos.z;
        _transform.position = transformpos;
        cineBrain.ManualUpdate();
    }
}