using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SldScripty : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider _mainSlider;
    GameControl _gameControl;
    public bool sliderItem;
    float _timeLerp;
    public float _valueLerp;
    public float _valeuItem;
    bool _itemCheck;
    bool _recharge;
    float _valueTempSlider;
    bool _pauseSlider;
    public RectTransform[] _item;
    public bool _checkLife;



    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        _item[1].gameObject.SetActive(false);
        _mainSlider = GetComponent<Slider>();
        if (!sliderItem)
        {
            ValueMaxFire(_gameControl._life);
        }


    }

    void Update()
    {
        if (sliderItem)
        {
            SliderItem();
        }
      
    }

    public void ValueMaxFire(int value)
    {
        _mainSlider.maxValue = value;
        _mainSlider.value = value;


    }
    public void FireSum(bool sum)
    {
        if (!sliderItem)
        {
            if (sum)
            {
                _mainSlider.value++;

            }
            else
            {
                _mainSlider.value--;

            }
            _gameControl.LifeFire(false);
        }
    }

    public void MoveItem(Vector2 pos)//metodo de ativar o item e chamar corroutine
    {
        _item[1].gameObject.SetActive(true);
        StartCoroutine(MoveTimeitem(pos));

    }
    IEnumerator MoveTimeitem(Vector2 pos)//corroutine de animação e movimento do item até p slider
    {

        _item[1].transform.position = new Vector2(pos.x, pos.y); // atribuir posição nova
        _item[1].transform.DOScale(3f, 0.25f);
        yield return new WaitForSeconds(.25f);
        _item[1].DOAnchorPos(_item[0].localPosition, .25f, false);
        _item[1].transform.DOScale(1f, 0.25f);
        yield return new WaitForSeconds(.25f);
        _item[1].gameObject.SetActive(false);
        _item[0].transform.DOScale(2f, 0.15f);
        yield return new WaitForSeconds(.15f);
        _item[0].transform.DOScale(1f, 0.15f);
        FireSum(true);
    }

    public void PauseSliderON(bool on)
    {
        _pauseSlider = on;
    }
    public void ItemCheckON(bool on)
    {
        _itemCheck = on;
    }

    void SliderItem()
    {
        if (!_pauseSlider)
        {
            if (!_recharge)
            {
                if (_itemCheck)//pegou o item para aumentar o + tempo
                {

                    _itemCheck = false;
                    _valueTempSlider = _mainSlider.value + _valeuItem;// valor temporario que o tempo ira crescer
                    _recharge = true;
                }
                else// tempo decrescente, ou diminuindo 
                {
                    _timeLerp += _valueLerp * Time.fixedDeltaTime;
                    _mainSlider.value = _mainSlider.maxValue - _timeLerp;
                }
            }
            else// aumento de tempo
            {
                _timeLerp += (-_valeuItem * 10) * Time.fixedDeltaTime;
                _mainSlider.value = _mainSlider.maxValue - _timeLerp;
                if (_timeLerp <= 0)// se o tempo recarregar mais que o limite
                {
                    _timeLerp = 0;
                    _recharge = false;
                }
                if (_mainSlider.value > _valueTempSlider)// encerra o aumento do tempo
                {
                    _recharge = false;
                }
            }
            if (_mainSlider.value == 0 && !_checkLife)
            {
                _checkLife = true;
                _gameControl.StopEnemeys(true);
                _gameControl._hudControl.GameOverON(_gameControl._gameOver2);

            }

        }

        // Update is called once per frame
    }

    public void RestaureSlider()
    {        
            _checkLife = true;
           // _gameControl.StopEnemeys(true);
           // _gameControl.HitPlayer(_gameControl._movePerson.transform);
            do
            {
                _valueTempSlider = _mainSlider.value + _valeuItem;// valor temporario que o tempo ira crescer
                _timeLerp += (-_valeuItem * 10) * Time.fixedDeltaTime;
                _mainSlider.value = _mainSlider.maxValue - _timeLerp;
                if (_timeLerp <= 0)// se o tempo recarregar mais que o limite
                {
                    _timeLerp = 0;
                    _recharge = false;
                }
            } while (_mainSlider.value < _mainSlider.maxValue - 1f);
            _checkLife = false;
        
    }
}
