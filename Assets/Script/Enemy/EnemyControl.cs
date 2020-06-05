using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D _rigEnemy;
    GameControl _gameControl;
    public float _speed;
    float _speedL;
    public Collider2D _c1;
    public Collider2D _c2;
    BoxCollider2D _boxCollider;
    CircleCollider2D _circleCollider;
    SpriteRenderer _img;
    public bool _enemeyJump;
    public bool _enemeyFly;
    public float _valueFly;
    public float _forcePulo;
    Animator _animJump;
    public bool _ground;
    public float _timeStart;
    public float _timeStarFly;
    float _timeTemp;
    float _timeFly;
    public bool _checkDeath;
    public Vector2 posStart;
    public bool _restoreLife;
    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        _gameControl._enemyControlList.Add(this.gameObject.GetComponent<EnemyControl>());
        _rigEnemy = GetComponent<Rigidbody2D>();
        posStart = new Vector2(transform.localPosition.x, transform.localPosition.y);
        _speedL = _speed;
        if (GetComponent<BoxCollider2D>() != null)
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }
        if (GetComponent<CircleCollider2D>() != null)
        {
            _circleCollider = GetComponent<CircleCollider2D>();
        }
        _img = GetComponent<SpriteRenderer>();
        _animJump = GetComponent<Animator>();
        _rigEnemy.velocity = new Vector2(_speed, 0);
        _timeTemp=_timeStart; 
        _timeFly = _timeStarFly;



}
    private void Update()
    {
        if (!_checkDeath)
        {
            if (!_enemeyJump)
            {
                if (_enemeyFly) 
                {
                   
                    _timeFly -= Time.deltaTime;
                  //  _rigEnemy.velocity = new Vector2(_speed, _rigEnemy.velocity.y);
                    if (_timeFly < 0)
                    {
                        _valueFly = _valueFly * -1;                    
                        _timeFly = _timeStarFly;
                    }
                    _rigEnemy.velocity = new Vector2(_rigEnemy.velocity.x, _valueFly);
                }
               
                    _rigEnemy.velocity = new Vector2(_speed, _rigEnemy.velocity.y);

                

            }
            else
            {
               
                _animJump.SetFloat("SpeedV", _rigEnemy.velocity.y);
                _animJump.SetBool("CheckGround", _ground);
                _timeTemp -= Time.deltaTime;
                if (_timeTemp < 0 && _ground)
                {

                    _ground = false;
                    _rigEnemy.AddForce(transform.up * _forcePulo * 100);
                    _timeTemp = _timeStart;
                }
            }
        }
        else// morte
        {
            if (_boxCollider != null)
            {
                _boxCollider.enabled = false;
            }
            if (_circleCollider != null)
            {
                _circleCollider.enabled = false;
            }
            _rigEnemy.velocity = new Vector2(0, -10);
            _animJump.SetBool("Death", true);

        }
        if (_restoreLife)// restaurar posição inimigo na plataforma
        {
            if (_boxCollider != null)
            {
                _boxCollider.enabled = true;
            }
            if (_circleCollider != null)
            {
                _circleCollider.enabled = true;
            }
            _checkDeath = false;
            transform.localPosition = new Vector2(posStart.x, posStart.y);
            _animJump.SetBool("Death", false);
            _restoreLife = false;

        }
       
    }
    public void Stop( bool check)
    {
   
        if (check)
        {
            _speed = 0;
            _circleCollider.enabled = false;
            _rigEnemy.isKinematic = true;
        }

        else
        {
            _speed = _speedL;
            _circleCollider.enabled = true;
            _rigEnemy.isKinematic = false;

        }
    }

    void Turn()
    {
        _speed = _speed * -1;
        _img.flipX = !_img.flipX;
        if (_speed > 0)
        {
            _img.flipX = true;
        }
        else
        {
            _img.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!_enemeyJump &&( collision == _c1 || collision == _c2))
        {
            Turn();

        }
       
   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _ground = true;
           
        }
        if (collision.gameObject.CompareTag("Enemy") && !_enemeyJump)
        {
            Turn();
            Debug.Log("2");
        }
    }

}
