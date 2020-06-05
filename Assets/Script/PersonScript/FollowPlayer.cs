using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Transform _player;
    void Start()
    {
        _player = Camera.main.GetComponent<GameControl>()._movePerson.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(_player.transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }
}
