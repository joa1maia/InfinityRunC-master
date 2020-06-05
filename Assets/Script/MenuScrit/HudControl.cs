using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControl : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> _panelStartGame = new List<Transform>();
    public List<Transform> _panelBackGame = new List<Transform>();
    public List<Transform> _panelGameOver = new List<Transform>();
    public Transform _panelLevel;
    public Text _textLevel;
    public Text _textBackGame;
    public List<Transform> _panelTop = new List<Transform>();
    public Text _topTextPoint;
    public Transform _backGround;
    public CanvasGroup _canvasGroup;
    public Text _textLife;
    GameControl _gameControl;
    SoundObj _soundObj;

    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
      //  _gameControl.LifeFire(true);
        _textLife.text = "x" + _gameControl._life;
    }

    public void StarGameOff()
    {
        _canvasGroup.interactable = false;
        StartCoroutine(StarGameOffTime(.25f));
    }

    IEnumerator StarGameOffTime(float waitTime)
    {
        _backGround.GetComponent<Image>().DOFade(0, waitTime);
        for (int i = 0; i < _panelStartGame.Count; i++)
        {
            yield return new WaitForSeconds(waitTime);
            _panelStartGame[i].DOScale(1.2f, waitTime);
            yield return new WaitForSeconds(waitTime);
            _panelStartGame[i].DOScale(0f, waitTime);
        }
        _panelStartGame[0].gameObject.SetActive(false);

        for (int i = 0; i < _panelTop.Count; i++)
        {
            _panelTop[i].transform.localScale = new Vector2(0, 0);
        }       

        for (int i = 0; i < _panelTop.Count; i++)
        {
            _panelTop[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            _panelTop[i].DOScale(1.2f, waitTime);
            yield return new WaitForSeconds(waitTime);
            _panelTop[i].DOScale(1f, waitTime);
        }
        _gameControl.MovePersonON(true);//liberar movimento player

        _gameControl._sldScripty.PauseSliderON(false);
        StopAllCoroutines();

    }
    public void BackGameOn(bool on)
    {
        StartCoroutine(BackGameOnTime(.25f,on));
    }
    public void TextRestart()
    {
        if (_gameControl._life != 1)
        {
            _textBackGame.text = "Resta " + _gameControl._life + " " + _gameControl._doses;
        }
        else
        {
            _textBackGame.text = "Resta " + _gameControl._life + " " + _gameControl._dose;
        }

    }
    IEnumerator BackGameOnTime(float waitTime, bool on)
    {
        if (on)
        {
            _gameControl._sldScripty.PauseSliderON(true);
            _backGround.GetComponent<Image>().DOFade(.75f, waitTime);

            _panelBackGame[0].gameObject.SetActive(true);

            for (int i = 0; i < _panelStartGame.Count; i++)
            {
                _panelBackGame[i].transform.localScale = new Vector2(0, 0);
            }

            for (int i = 0; i < _panelStartGame.Count; i++)
            {
                yield return new WaitForSeconds(waitTime);
                _panelBackGame[i].DOScale(1.2f, waitTime);
                yield return new WaitForSeconds(waitTime);
                _panelBackGame[i].DOScale(1f, waitTime);
            }
            _canvasGroup.interactable = true;
            StopAllCoroutines();
            Time.timeScale = 0;

        }
        else {
            Time.timeScale = 1;
            _canvasGroup.interactable = false;
            _backGround.GetComponent<Image>().DOFade(0, waitTime);
            for (int i = 0; i < _panelStartGame.Count; i++)
            {
                yield return new WaitForSeconds(waitTime);
                _panelBackGame[i].DOScale(1.2f, waitTime);
                yield return new WaitForSeconds(waitTime);
                _panelBackGame[i].DOScale(0f, waitTime);
            }
           
            _panelBackGame[0].gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            _gameControl._movePerson._stopPlayer = false;
            _gameControl._sldScripty.PauseSliderON(false);
            _gameControl._sldScripty._mainSlider.value = _gameControl._sldScripty._mainSlider.maxValue;
            StopAllCoroutines();
        }  
    }
    public void GameOverON(string textGameOver)
    {
        Time.timeScale = 1;
        _panelGameOver[2].GetComponent<Text>().text = "" + textGameOver;
        StartCoroutine(GameOverONTime(.25f));
        _gameControl._movePerson._stopPlayer = true;
        _canvasGroup.interactable = true;
    }
    IEnumerator GameOverONTime(float waitTime)
    {
        _backGround.GetComponent<Image>().DOFade(0, waitTime);
        _panelGameOver[0].gameObject.SetActive(true);
        _gameControl._sldScripty.PauseSliderON(true);

        for (int i = 0; i < _panelTop.Count; i++)
        {
            _panelGameOver[i].transform.localScale = new Vector2(0, 0);
        }

        for (int i = 0; i < _panelTop.Count; i++)
        {
            _panelGameOver[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            _panelGameOver[i].DOScale(1.2f, waitTime);
            yield return new WaitForSeconds(waitTime);
            _panelGameOver[i].DOScale(1f, waitTime);
        }
        StopAllCoroutines();

    }

    public IEnumerator LevelOn(int level)
    {
        _panelLevel.gameObject.SetActive(true);
         yield return new WaitForSeconds(.1f);
        _panelLevel.transform.DOScale(1, .3f);
        _textLevel.text = "Level " + level;
        yield return new WaitForSeconds(3);
        _panelLevel.transform.DOScale(0, .3f);
        yield return new WaitForSeconds(.3f);
        _panelLevel.gameObject.SetActive(false);
    }

    public void Point()
    {
        _gameControl._point++;
        _topTextPoint.text ="Pontos: "+ _gameControl._point.ToString("D3");

    }
}
