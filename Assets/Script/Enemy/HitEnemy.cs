using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    GameControl _gameControl;
    public Transform _vRestart;
    EnemyControl _enemyControl;
    public bool Enemey;
    public Collider2D _collider2D;
    AudioSource _audioSource;

    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        _enemyControl = GetComponent<EnemyControl>();
        _collider2D = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _collider2D.enabled)//chamar menu de restart ou gameover quando player tocar na agua ou inimigo
        {
            _collider2D.enabled = false;
            _gameControl.HitPlayer(_vRestart);


            _enemyControl.Stop(true);
            _gameControl.StopEnemeys(true);
            _audioSource.Play();

        }    
    }  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))//chamar menu de restart ou gameover quando player tocar na agua ou inimigo
        {
            _gameControl.HitPlayer(_vRestart);
            _gameControl.StopEnemeys(true);
            _audioSource.Play();
        }
        else if (Enemey && collision.gameObject.CompareTag("Fire"))//Morte do inimigo quando tocar na bala
        {
            _enemyControl._checkDeath = true;
            _audioSource.Play();
        }
    }
}
