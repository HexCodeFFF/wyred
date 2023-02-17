using UnityEngine;

public class TestButton : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public AudioClip buttonDown;
    public AudioClip buttonUp;
    private AudioSource _audioSource;
    private Level _level;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = upSprite;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "player")
        {
            _renderer.sprite = downSprite;
            Debug.Log(_level.Test());
            _audioSource.PlayOneShot(buttonDown);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "player")
        {
            _renderer.sprite = upSprite;
            _audioSource.PlayOneShot(buttonUp);
        }
    }
}