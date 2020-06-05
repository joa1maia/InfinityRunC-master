using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolGround : MonoBehaviour
{
    // Start is called before the first frame update
    GameControl _gameControl;
    [SerializeField]
    LayerMask platlayerMask;
    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
    }

    // Update is called once per frame


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _gameControl._movePerson._ground = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _gameControl._movePerson._ground = false;
        }
    }
}
