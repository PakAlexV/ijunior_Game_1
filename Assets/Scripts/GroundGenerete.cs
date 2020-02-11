using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerete : MonoBehaviour
{
    [SerializeField] private Sprite _groundButtom = default, _groundTop = default, _groundLeft = default, _groundRigth = default, _groundAngelLeft = default, _groundAngelRigth = default,
                                    _holeTop = default, _holeButtom = default, _holeLeft = default, _holeRigth = default, _holeButLeft = default, _holeButRigth = default,
                                    _waterTop = default, _waterButtom = default, _waterLeft = default, _waterRigth = default, _waterButLeft = default, _waterButRigth = default;

    [SerializeField] private GameObject _prefBg = default, _prefHole = default, _prefBarrier = default, _prefCoin = default;
    [SerializeField][Range(3, 6)] private int _whatTileDown = 5;
 
    private int _initLocationMin = -8;
    private int _initLocationMax = 10;

    private int _nextY;
    private int _currentX, _currentY = 0;
    private int _lastBarrierX = 0;
    private Sprite _currentSprite;
    private bool _isBarrier = false;

    private List<string> _nonNextRandom = new List<string>();
    private List<string> _landTile = new List<string>();

    public void OnDestroyTile()
    {
        RendTile();
        _currentX++;
    }

    private void Start()
    {
        _currentSprite = _groundTop;
        NonNextRandomInit();
        LendTilesInit();
        _currentX = _initLocationMin;
        while (_currentX <= _initLocationMax)
        {
            RendTile();
            _currentX++;
        }
    }

    private void LendTilesInit()
    {
        _landTile.Add(_groundTop.name);
        _landTile.Add(_groundLeft.name);
        _landTile.Add(_groundRigth.name);
        _landTile.Add(_holeLeft.name);
        _landTile.Add(_holeRigth.name);
        _landTile.Add(_waterLeft.name);
        _landTile.Add(_waterRigth.name);
    }

    private void NonNextRandomInit()
    {
        _nonNextRandom.Add(_holeTop.name);
        _nonNextRandom.Add(_holeLeft.name);
        _nonNextRandom.Add(_waterTop.name);
        _nonNextRandom.Add(_waterLeft.name);
        _nonNextRandom.Add(_groundAngelRigth.name);
    }

    private void RendTile(Sprite sprite = null)
    {
        if (_nonNextRandom.Contains(_currentSprite.name) == false) { NextRandomTile(_currentY); }
        if (_currentSprite.name == _groundAngelRigth.name) { _currentSprite = _groundTop; }

        InitTile(_currentX, _currentY, _currentSprite);

        CanBonusDraw();
        
        CanDrawTileDown(_currentX, _currentY - 1);
        NeedAddTileDraw(_currentSprite.name);

        _currentY = _nextY;
    }

    private void CanBonusDraw()
    {
        if (_currentSprite.name == _groundTop.name)
        {
            int _r = Random.Range(1, 10);
            if (_r > 5)
            {
                Instantiate(_prefCoin, new Vector3(_currentX, _currentY + 1, 0), Quaternion.identity);
            }
            if ((_r < 3) && (_lastBarrierX + 1 != _currentX))
            {
                Instantiate(_prefBarrier, new Vector3(_currentX, _currentY + 1, 0), Quaternion.identity);
                _lastBarrierX = _currentX;
                _isBarrier = true;
            }
        }
    }

    private void NeedAddTileDraw(string name)
    {
        if (name == _groundAngelLeft.name) { InitTile(_currentX, _currentY + 1, _groundLeft); }
        else if (name == _holeRigth.name) { _currentSprite = _holeTop; }
        else if (name == _holeTop.name) { _currentSprite = _holeLeft; }
        else if (name == _waterRigth.name) { _currentSprite = _waterTop; }
        else if (name == _waterTop.name) { _currentSprite = _waterLeft; }
        else if (name == _groundRigth.name)
        {
            _currentSprite = _groundAngelRigth;
            InitTile(_currentX, _currentY - 1, _currentSprite);
        }
        else if ((name == _holeLeft.name) ||
            (name == _waterLeft.name) ||
            (name == _groundAngelRigth.name)) { _currentSprite = _groundTop; } 
    }

    private void DrawTileDown(int y, Sprite sprite)
    {
        _currentSprite = sprite;
        InitTile(_currentX, y, sprite);
        CanDrawTileDown(_currentX, y -1);
    }

    private void CanDrawTileDown(int x, int y)
    {
        if ((_currentSprite.name == _groundTop.name) ||
            (_currentSprite.name == _groundAngelLeft.name)) { InitTile(x, y, _groundButtom); }
        if (_currentSprite.name == _groundRigth.name) { InitTile(x, y - 1, _groundButtom); }
        if (_currentSprite.name == _holeTop.name) { InitTile(x, y, _holeButtom); }
        if (_currentSprite.name == _holeLeft.name) { InitTile(x, y, _holeButLeft); }
        if (_currentSprite.name == _holeRigth.name) { InitTile(x, y, _holeButRigth); }
        if (_currentSprite.name == _waterTop.name) { InitTile(x, y, _waterButtom); }
        if (_currentSprite.name == _waterLeft.name) { InitTile(x, y, _waterButLeft); }
        if (_currentSprite.name == _waterRigth.name) { InitTile(x, y, _waterButRigth); }

        if (y - 1 > -_whatTileDown)
        {
            CanDrawTileDown(x, y - 1);
        }
    }

    private void InitTile(int x, int y, Sprite sprite)
    {
        GameObject _pref = _prefBg;
        _pref.tag = "empty";
        if ((sprite == _holeTop) || (sprite == _waterTop))
        {
            _pref = _prefHole;
            _pref.tag = "hole";
        }
        if (_landTile.Contains(sprite.name) == true)
        {
            _pref.tag = "land";
        }
        _pref.GetComponent<SpriteRenderer>().sprite = sprite;
        Instantiate(_pref, new Vector3(x, y, 0), Quaternion.identity);
    }

    private void NextRandomTile(int y)
    {
        int _r = Random.Range(1, 10);
        if (_isBarrier)
        {
            _r = 0;
            _isBarrier = false;
        }
        if (y == 2)
        {
            _currentSprite = _r < 5 ? _groundTop : _groundRigth;  // 50x50 прямо-вниз
            _nextY = _r < 5 ? y : y - 1; 
        }
        if (y == -2)
        {
            _currentSprite = _r < 5 ? _groundTop : _groundAngelLeft; 
            _nextY = _r < 5 ? y : y + 1; // 50x50 прямо-вверх
        }
        if ((y == 1) || (y == -1))
        {
            _currentSprite = _r < 6 ? _groundTop : _r > 8 ? _groundRigth : _groundAngelLeft; // 60x20x20 прямо-вниз-вверх 
            _nextY = _r < 6 ? y : _r > 8 ? y - 1 : y + 1;
        }   
        if (y == 0)
        {
            _currentSprite = _r < 6 ? _groundTop : _r == 7 ? _groundRigth : _r == 8 ? _groundAngelLeft : _r == 9 ? _holeRigth : _waterRigth; // 60x10x10x10x10 прямо-вниз-вверх-внизяма-внизвода
            _nextY = _r < 6 ? y : _r == 7 ? y - 1 : _r == 8 ? y +1 : y;
        }
    }
}