using System;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    private Level _level;
    public Sprite upSprite;
    public Sprite downSprite;
    private SpriteRenderer _renderer;
    private void Start()
    {   
        _level = GetComponentInParent<Level>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = upSprite;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _renderer.sprite = downSprite;
            Debug.Log(_level.Test());
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _renderer.sprite = upSprite;
        }
    }
}