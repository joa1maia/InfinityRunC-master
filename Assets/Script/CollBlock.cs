using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollBlock : MonoBehaviour
{
    // Start is called before the first frame update
    MovePerson _movePerson;
    Rigidbody2D rigbloc;
    public float _dist;
    void Start()
    {
        _movePerson = Camera.main.GetComponent<GameControl>()._movePerson;
        rigbloc = GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(9, 10, true);
    }

    // Update is called once per frame
    void Update()
    {
        _dist = Vector3.Distance(_movePerson.transform.localPosition, transform.position);
          if (_movePerson.enabled && _movePerson._rig.velocity.x > 0.1f && _dist> 6)
        {        
            rigbloc.velocity = new Vector2(_movePerson._rig.velocity.x, 0);
        }
        else
        {
            rigbloc.velocity = new Vector2(0, 0);
       
        }

    }   
}
