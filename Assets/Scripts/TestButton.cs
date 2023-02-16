using UnityEngine;

public class TestButton : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    private Level _level;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = upSprite;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "player")
        {
            _renderer.sprite = downSprite;
            Debug.Log(_level.Test());
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "player") _renderer.sprite = upSprite;
    }
}