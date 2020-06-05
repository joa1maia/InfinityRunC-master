using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    // Start is called before the first frame update

    GameControl _gameControl;
    public AudioClip _soundFire;
    SoundObj _soundObj;
    bool _fireCheck;
    float timeT;
    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        _soundObj = GetComponent<SoundObj>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !_fireCheck && _gameControl._sldScriptyFire._mainSlider.value>0)
        {
            _fireCheck = true;
            _soundObj.StartSound(_soundFire);
            _gameControl._sldScriptyFire.FireSum(false);
           _gameControl.LifeFire(false);
            for (int i = 0; i < 5; i++)
            {
                Invoke("Fire", timeT);
                timeT = timeT + .06f;
            }
            Invoke("FireTime", 1f);
        }
    }
    public void Fire() {
        GameObject obj = ObjectPooler.current.GetPooledObject();
        if (obj != null)
        {
           // Debug.Log(obj.gameObject.name + " hhh");
            obj.transform.position = _gameControl._posFire.position;
            obj.transform.rotation = _gameControl._posFire.rotation;
            obj.SetActive(true);
            //_manageCenario2.somTiro.Play();
        }
    }
    void FireTime()
    {
        timeT = .06f;
        _fireCheck = false;
    }
}
