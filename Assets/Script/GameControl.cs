using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    public MovePerson _movePerson;
    public Transform _posFire;
    public HudControl _hudControl;
    public SldScripty _sldScripty;
    public SldScripty _sldScriptyFire;
    public int _life;
    public int _point;

    public List<EnemyControl> _enemyControlList = new List<EnemyControl>();
    public List<GameObject> _itemList = new List<GameObject>();

    public Vector2 offset;


    public String _gameOver1;
    public String _gameOver2;
    public String _dose;
    public String _doses;

    private void Awake()
    {
        MovePersonON(false);//liberar movimento player
        _sldScripty.PauseSliderON(true);
    }


    public void StopEnemeys(bool check)
    {
        for (int i = 0; i < _enemyControlList.Count; i++)
        {
            _enemyControlList[i].Stop(check);
        }
    }

    public void ResetPlat()
    {
        for (int i = 0; i < _enemyControlList.Count; i++)
        {
            _enemyControlList[i]._restoreLife = true;
        }
        for (int i = 0; i < _itemList.Count; i++)
        {
            _itemList[i].SetActive(true);
            _itemList[i].GetComponent<ItemScript>().RestartItem();
        }
    }
    public void MovePersonON(bool move)
    {
        _movePerson.enabled = move;
    }
    public void LifeFire(bool check)
    {
    /*  if (check) {
            _sldScriptyFire._mainSlider.value = _life;
            _life = Convert.ToInt32(_sldScriptyFire._mainSlider.value);
        }
        else
        {
            _life = Convert.ToInt32(_sldScriptyFire._mainSlider.value);
        
        _hudControl._textLife.text = "x" + _life;  */
    }

    public void HitPlayer(Transform _vRestart)
    {
        _life--;
        LifeFire(true);
        if (_life > 0)//restart
        {
            _hudControl.BackGameOn(true);
            _hudControl.TextRestart();
            _hudControl._textLife.text = "x" + _life;
            _movePerson.VectorTempPos(_vRestart);// enviar valores para restart de posição
        }
        else if (_life <= 0)
        {
            _movePerson.VectorTempPos(_vRestart);
            _hudControl.GameOverON(_gameOver1);
        }
    }
    public void LevelSlideTime (){
        _sldScriptyFire._mainSlider.maxValue = _sldScriptyFire._mainSlider.maxValue + 1;
        //_sldScriptyFire._mainSlider.value = _sldScriptyFire._mainSlider.value - 1;
    }


}
