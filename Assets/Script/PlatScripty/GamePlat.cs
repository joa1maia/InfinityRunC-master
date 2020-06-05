using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded;

public class GamePlat : MonoBehaviour
{
    GameControl _gameControl;
    public List<GameObject> _platStartL = new List<GameObject>();
    public List<GameObject> _platL1 = new List<GameObject>();
    public List<GameObject> _platL2 = new List<GameObject>();
    public List<GameObject> _platL3 = new List<GameObject>();
    public List<GameObject> _platTempL = new List<GameObject>();


    public Transform _platParent;

    public int _numbPlat;
    int _OrdNumbplat;
    float _platdistance;

    GameObject _prefbPlat;
    GameObject _pref_i_Plat;
    public GameObject _prefbPlatLevel;

    public int _levelgame;
    bool _passlevel;

    void Awake()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        DistancePlatM();
        InstPlats(_numbPlat);
    }

    // Update is called once per frame

    void DistancePlatM()// calcula distanacia entre a primeira e a segunda plataforma
    {
        _platdistance = _platStartL[0].transform.position.x - _platStartL[1].transform.position.x;
        if (_platdistance < 0)// checa se o valor é negativo, caso seja, transforma em positivo
        {
            _platdistance = _platdistance * -1;
        }
    }
    void InstNewPlats()//Intanciar novas plataformas para na passagem de level
    {
        GameObject _clone = Instantiate(_prefbPlat, _prefbPlat.transform.position, _prefbPlat.transform.rotation);// instanciar plataformas
        _platTempL.Add(_clone); //add na lista
        _clone.transform.SetParent(_platParent);// colocar como filho de outro gameobject
        _clone.transform.position = new Vector2(0, 0);// posicionar 

    }

    void PassLevel()
    {
      
        switch (_levelgame)
        {
            case 0:
                _prefbPlat = _platL1[_OrdNumbplat];
                break;
            case 1:
                _prefbPlat = _platL2[_OrdNumbplat];
                break;
            case 2:
                _prefbPlat = _platL3[_OrdNumbplat];
                break;
            default:
                break;
        }
    }

    void InstPlats(int _numbL)// gera plataformas pela primeira vez
    {
        _platTempL = _platTempL.Shuffle();
        for (int i = 0; i < _numbL; i++)
        {
            PassLevel();
            GameObject _clone = Instantiate(_prefbPlat, _prefbPlat.transform.position, _prefbPlat.transform.rotation);// instanciar plataformas
            _platTempL.Add(_clone); //add na lista
            _clone.transform.SetParent(_platParent);// colocar como filho de outro gameobject
            _clone.transform.position = new Vector2(0, 0);// posicionar 

            if (i == 0)
            {// se for a primeira pegar posição da plataformas iniciais
                _clone.transform.position = new Vector2(_platStartL[1].transform.position.x + _platdistance, _platStartL[0].transform.position.y);
            }
            else
            {
                _clone.transform.position = new Vector2(_platTempL[i - 1].transform.position.x + _platdistance, _platStartL[0].transform.position.y);
                if (i == _numbL - 1)// se for colocado a ultima plataforma, coloca plataforma level
                {
                    _pref_i_Plat = Instantiate(_prefbPlatLevel, _prefbPlat.transform.position, _prefbPlat.transform.rotation);// instanciar intervalo de plataformas
                    _pref_i_Plat.transform.SetParent(_platParent);// colocar como filho de outro gameobject  
                    _pref_i_Plat.transform.position = new Vector2(0, 0);// posicionar 0
                    _pref_i_Plat.transform.position = new Vector2(_platTempL[i].transform.position.x + _platdistance, _platStartL[0].transform.position.y);

                }
            }

            _OrdNumbplat++;
            if (_OrdNumbplat == _platL1.Count)
            {
                // Debug.Log(_numbplatordem +" "+ _plataformasL1.Count);
                _OrdNumbplat = 0;
            }
        }

    }

    public void LoopRepitPlat()
    {
       //------ Passou de level
        _levelgame++;
        StartCoroutine(_gameControl._hudControl.LevelOn(_levelgame));
        if (!_passlevel)
        {
            _passlevel = true;
            _gameControl.LevelSlideTime();
        }
        else
        {
            _passlevel = false;
        }
        if (_levelgame == 1)
        {
            PassLevel();

            for (int i = 0; i < _platL2.Count; i++)
            {
                InstNewPlats();
            }
        }
        else if (_levelgame == 2)
        {
            PassLevel();

            for (int i = 0; i < _platL3.Count; i++)
            {
                InstNewPlats();
            }
        }
        else
        {
            int nn = _platTempL.Count;
            Debug.Log(nn);
            for (int m = 0; m < nn; m++)
            {
                GameObject _clone = Instantiate(_platTempL[m], _platTempL[m].transform.position, _platTempL[m].transform.rotation);// instanciar plataformas
                _platTempL.Add(_clone); //add na lista
                _clone.transform.SetParent(_platParent);// colocar como filho de outro gameobject
                _clone.transform.position = new Vector2(0, 0);// posicionar 

            }
        }

        _platTempL = _platTempL.Shuffle();
        for (int i = 0; i < _platTempL.Count; i++)
        {
            if (i == 0)
            {// se for a primeira pegar posição da plataforma intervalo e repetir na posição a frente
                _platTempL[i].transform.position = new Vector2(_pref_i_Plat.transform.position.x + _platdistance, _platStartL[0].transform.position.y);
            }
            else
            {
                _platTempL[i].transform.position = new Vector2(_platTempL[i - 1].transform.position.x + _platdistance, _platStartL[0].transform.position.y);
            }
            _OrdNumbplat++;
            if (_OrdNumbplat == _platL1.Count)
            {
                _OrdNumbplat = 0;
            }
        }
        _gameControl.ResetPlat();
    }
    public void LoopPlatlevel()
    {
        _pref_i_Plat.transform.position = new Vector2(_platTempL[_platTempL.Count - 1].transform.position.x + _platdistance, _platStartL[0].transform.position.y);
    }
}
