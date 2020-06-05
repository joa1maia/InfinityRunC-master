using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePerson : MonoBehaviour
{
    public Vector2 _movement;
    Rigidbody2D _rig;
    public float _speed;
    public SpriteRenderer _imgPerson;
    public float _jumpForce;
    Animator _anim;
    public bool _ground;
    public bool _jumpDoubleCheck;
    public Vector2 _vPosRestart;
    Vector2 _posRestart;
    public  bool _stopPlayer;
    public AudioClip _soundJump;
    SoundObj _soundObj;

    public Collider2D _collider2D;
    public float _extraH;
    public LayerMask _layerMask;
    public Transform _posStartRay;

    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _soundObj = GetComponent<SoundObj>();
        _rig.isKinematic = false;
        _collider2D = GetComponent<Collider2D>();
      //  Physics2D.IgnoreLayerCollision(0,8,true);

    }
    void FixedUpdate()
    {
        MovePersonHorixontal();
        JumpM();
        
    }

    public void VectorTempPos(Transform posTemp)//recebe valores enviados do restarrpoint
    {
      
        _rig.isKinematic = true;
        _rig.velocity = new Vector2(0, 0);
        _stopPlayer = true;
        _posRestart = new Vector2(posTemp.position.x, posTemp.position.y);
    }
    public void RestartPos()//botão para continuar o game de acordo com a posição recebida
    {
        _rig.isKinematic = false;
        transform.position = _posRestart;
    }


    void MovePersonHorixontal()
    {
       _ground= IsGroundCheck();
        if (_jumpDoubleCheck == true)
        {
            Invoke("JumpDoubleFalse", 1f);
        }
        if (!_stopPlayer)// se true. p player fica imovel
        {
            if (_ground)
            {
                _movement = new Vector2(Input.GetAxis("Horizontal"), _rig.velocity.y);// retorna valor direto do teclado
                //_movement = new Vector2(1, _rig.velocity.y);// retorna valor direto do teclado
                _rig.velocity = new Vector2(_movement.x * _speed, _rig.velocity.y);
            }
          

            if (_movement.x > 0.001f)// condição para ativa o fliper do sprite do personagem
            {
               // _imgPerson.flipX = false;
               transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if (_movement.x < -0.001f)
            {
               // _imgPerson.flipX = true;
               transform.localScale = new Vector2(-1, transform.localScale.y);
            }
     

            _anim.SetFloat("runSpeed", Mathf.Abs(_movement.x));// envia o valor para maquina de estado de animação, sendo que a variavel _movement, sempre retorna positivo

        }
        else
        {
            _rig.velocity = new Vector2(0, 0);
            _anim.SetFloat("runSpeed",0);// envia o valor para maquina de estado de animação, sendo que a variavel _movement, sempre retorna positivo
        }

    }
    void JumpM() {

        if (_ground && !_jumpDoubleCheck && Input.GetButtonDown("Jump"))//checa se apertou do teclado e se esta tocando no chão para pular.
        {
            _rig.AddForce(transform.up * _jumpForce * 10);
            _soundObj.StartSound(_soundJump);
            Invoke("JumpDouble", .4f);
            
          
        }
        if ( !_ground && _jumpDoubleCheck && Input.GetButtonDown("Jump"))
        {
            _jumpDoubleCheck = false;

            if (_rig.velocity.y < 0){
       
                _rig.AddForce(transform.up * _jumpForce * 2 * -(_rig.velocity.y));
            }
            else
            {
                _rig.AddForce(transform.up * _jumpForce * 6);
            }
            _soundObj.StartSound(_soundJump);
             _jumpDoubleCheck = false;
            CancelInvoke();
           // CancelInvoke();
        }
        _anim.SetFloat("jumpSpeed", _movement.y);
        _anim.SetBool("Ground", _ground);

    }

    private bool IsGroundCheck()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(_posStartRay.transform.position, Vector2.left,(_posStartRay.position.x + _extraH), _layerMask);
        Color rayColor;
  
        if (raycastHit2D.collider !=null)
        {
            rayColor = Color.green;
            _ground = false;
        }
        else
        {
            rayColor = Color.red;
            _ground = true;
           // _jumpDoubleCheck = false;
        }

        Debug.DrawRay(_posStartRay.transform.position, Vector2.left * (_posStartRay.position.x + _extraH), rayColor);
       // Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
    void JumpDouble()
    {       
        _jumpDoubleCheck = true;
 
    }
    void JumpDoubleFalse()
    {
        _jumpDoubleCheck = false;
        CancelInvoke("umpDoubleFalse");

    }
}
