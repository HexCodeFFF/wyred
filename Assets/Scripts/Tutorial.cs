using System;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float fadeTime = 0.5f;
    private CanvasGroup _canvasGroup;
    private bool _show = false;
    private float _alpha = 0;


    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (_show)
        {
            _alpha = Math.Min(_alpha + Time.deltaTime / fadeTime, 1);
        }
        else
        {
            _alpha = Math.Max(_alpha - Time.deltaTime / fadeTime, 0);
        }

        _canvasGroup.alpha = _alpha;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player)
        {
            _show = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player)
        {
            _show = false;
        }
    }
}