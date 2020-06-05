using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firepref : MonoBehaviour
{
    public float projetcSpeed;
    private Rigidbody2D rd;
    public float speedDisable; 
    GameControl _gameControl;
    public float _xPerson;
    void OnEnable()
    {
        if (rd != null)
        {
            SpeedFire();

            }
        Invoke("Disable", speedDisable);
    }
    private void Update()
    {
        _xPerson = _gameControl._movePerson.transform.position.x;
    }

    // Update is called once per frame
    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        rd = GetComponent<Rigidbody2D>();
        SpeedFire();
    }
    void SpeedFire()
    {
        rd.AddForce(new Vector2(_gameControl._movePerson.transform.localScale.x * projetcSpeed, 0), ForceMode2D.Impulse);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
