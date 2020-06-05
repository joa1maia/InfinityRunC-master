using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameControl _gameControl;
    Collider2D _collider2D;
    public Camera _targetCamera;
    public bool _fireCheck;
    Vector3 _vecPos;
    AudioSource _audioSource;
    // Collider2D _collider;
    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _gameControl = Camera.main.GetComponent<GameControl>();
        _targetCamera = Camera.main.GetComponent<Camera>();
        _gameControl._itemList.Add(gameObject);
        _vecPos = transform.localPosition;
        _audioSource = GetComponent<AudioSource>();
    }
    public void RestartItem()
    {
        transform.localPosition = _vecPos;
        _collider2D.enabled = true;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_fireCheck)
        {         
            _gameControl._sldScripty.ItemCheckON(true);
            _collider2D.enabled = false;
            Vector2 screenPosition = _targetCamera.WorldToScreenPoint(_gameControl._movePerson.transform.position);
            transform.localPosition = screenPosition + _gameControl.offset;
            _gameControl._sldScripty.MoveItem(transform.localPosition);
            _gameControl._sldScripty.RestaureSlider();
            _gameControl._hudControl.Point();
            _audioSource.Play();
        }
        if (collision.gameObject.CompareTag("Player") && _fireCheck)
        {
            _gameControl._sldScriptyFire.ItemCheckON(true);
            _collider2D.enabled = false;
            Vector2 screenPosition = _targetCamera.WorldToScreenPoint(_gameControl._movePerson.transform.position);
            transform.localPosition = screenPosition + _gameControl.offset;
            _gameControl._sldScriptyFire.MoveItem(transform.localPosition);
            _audioSource.Play();
        }

    }

}
