
using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Collider2D _collider;
    private GameObject _canvas;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _canvas = GetComponentInChildren<Canvas>().gameObject;
        // _canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player)
        {
            _canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player)
        {
            _canvas.SetActive(false);
        }
    }
}
