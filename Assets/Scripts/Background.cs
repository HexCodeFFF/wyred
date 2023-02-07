using System;
using Cinemachine;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float parallax = 0.1f;
    public Camera camera;
    private Transform _cameraTransform;
    private CinemachineBrain _cameraBrain;
    private Transform _transform;
    private Vector3 _startpos;

    

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _cameraTransform = camera.GetComponent<Transform>();
        _cameraBrain = camera.GetComponent<CinemachineBrain>();
        _startpos = _transform.position;
    }

    private void Update()
    {
        Vector3 transformpos = _cameraTransform.position * parallax + _startpos;
        transformpos.z = _startpos.z;
        _transform.position = transformpos;
        // need to tell camera when to update otherwise it goes jittery and silly and goofy
        _cameraBrain.ManualUpdate();
    }
}