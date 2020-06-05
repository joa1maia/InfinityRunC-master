using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolPlatLevel : MonoBehaviour
{
    public bool _platIntv;
    GamePlat _gamePlat;
   // GameControl _gameControl;
    // Start is called before the first frame update
    void Start()
    {
        _gamePlat = Camera.main.GetComponent<GamePlat>();
      //  _gameControl = Camera.main.GetComponent<GameControl>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (_platIntv && collision.gameObject.CompareTag("Player"))
        {
            _gamePlat.LoopRepitPlat();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_platIntv && collision.gameObject.CompareTag("Player"))
        {
            _gamePlat.LoopPlatlevel();
        }

    }
}
