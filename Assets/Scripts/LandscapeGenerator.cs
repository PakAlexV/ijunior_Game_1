using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    [SerializeField] private EnvironmentPalite _ground;
    [SerializeField] private EnvironmentPalite _hole;
    [SerializeField] private EnvironmentPalite _water;

    [SerializeField] private GameObject _prefabBackground;
    [SerializeField] private GameObject _prefabBackgroundBottom;
    [SerializeField] private GameObject _prefabHole;
    [SerializeField] private GameObject _prefabBarrier;
    [SerializeField] private GameObject _prefabCoin;

    [SerializeField][Range(3, 6)] private int _whatTileDown = 5;
 
    private int _initLocationMin = -8;
    private int _initLocationMax = 10;

    private int _nextY;
    private int _currentX;
    private int _currentY = 0;
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
        _currentSprite = _ground.TopSprite;
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
        _landTile.Add(_ground.TopSprite.name);
        _landTile.Add(_ground.LeftSprite.name);
        _landTile.Add(_ground.RigthSprite.name);
        _landTile.Add(_hole.LeftSprite.name);
        _landTile.Add(_hole.RigthSprite.name);
        _landTile.Add(_water.LeftSprite.name);
        _landTile.Add(_water.RigthSprite.name);
    }

    private void NonNextRandomInit()
    {
        _nonNextRandom.Add(_hole.TopSprite.name);
        _nonNextRandom.Add(_hole.LeftSprite.name);
        _nonNextRandom.Add(_water.TopSprite.name);
        _nonNextRandom.Add(_water.LeftSprite.name);
        _nonNextRandom.Add(_ground.AngelRigthSprite.name);
    }

    private void RendTile(Sprite sprite = null)
    {
        if (_nonNextRandom.Contains(_currentSprite.name) == false) { NextRandomTile(_currentY); }
        if (_currentSprite.name == _ground.AngelRigthSprite.name) { _currentSprite = _ground.TopSprite; }

        InitTile(_currentX, _currentY, _currentSprite);

        CanBonusDraw();
        
        CanDrawTileDown(_currentX, _currentY - 1);
        NeedAddTileDraw(_currentSprite.name);

        _currentY = _nextY;
    }

    private void CanBonusDraw()
    {
        if (_currentSprite.name == _ground.TopSprite.name)
        {
            int _r = Random.Range(1, 10);
            if (_r > 5)
            {
                Instantiate(_prefabCoin, new Vector3(_currentX, _currentY + 1, 0), Quaternion.identity);
            }
            if ((_r < 3) && (_lastBarrierX + 1 != _currentX))
            {
                Instantiate(_prefabBarrier, new Vector3(_currentX, _currentY + 1, 0), Quaternion.identity);
                _lastBarrierX = _currentX;
                _isBarrier = true;
            }
        }
    }

    private void NeedAddTileDraw(string name)
    {
        if (name == _ground.AngelLeftSprite.name) { InitTile(_currentX, _currentY + 1, _ground.LeftSprite); }
        else if (name == _hole.RigthSprite.name) { _currentSprite = _hole.TopSprite; }
        else if (name == _hole.TopSprite.name) { _currentSprite = _hole.LeftSprite; }
        else if (name == _water.RigthSprite.name) { _currentSprite = _water.TopSprite; }
        else if (name == _water.TopSprite.name) { _currentSprite = _water.LeftSprite; }
        else if (name == _ground.RigthSprite.name)
        {
            _currentSprite = _ground.AngelRigthSprite;
            InitTile(_currentX, _currentY - 1, _currentSprite);
        }
        else if ((name == _hole.LeftSprite.name) ||
            (name == _water.LeftSprite.name) ||
            (name == _ground.AngelRigthSprite.name)) { _currentSprite = _ground.TopSprite; } 
    }

    private void DrawTileDown(int y, Sprite sprite)
    {
        _currentSprite = sprite;
        InitTile(_currentX, y, sprite);
        CanDrawTileDown(_currentX, y -1);
    }

    private void CanDrawTileDown(int x, int y)
    {
        if ((_currentSprite.name == _ground.TopSprite.name) ||
            (_currentSprite.name == _ground.AngelLeftSprite.name)) { InitTile(x, y, _ground.BottomSprite); }
        if (_currentSprite.name == _ground.RigthSprite.name) { InitTile(x, y - 1, _ground.BottomSprite); }
        if (_currentSprite.name == _hole.TopSprite.name) { InitTile(x, y, _hole.BottomSprite); }
        if (_currentSprite.name == _hole.LeftSprite.name) { InitTile(x, y, _hole.BottomLeftSprite); }
        if (_currentSprite.name == _hole.RigthSprite.name) { InitTile(x, y, _hole.BottomRigthSprite); }
        if (_currentSprite.name == _water.TopSprite.name) { InitTile(x, y, _water.BottomSprite); }
        if (_currentSprite.name == _water.LeftSprite.name) { InitTile(x, y, _water.BottomLeftSprite); }
        if (_currentSprite.name == _water.RigthSprite.name) { InitTile(x, y, _water.BottomRigthSprite); }

        if (y - 1 > -_whatTileDown)
        {
            CanDrawTileDown(x, y - 1);
        }
    }

    private void InitTile(int x, int y, Sprite sprite)
    {
        GameObject _prefab = _prefabBackgroundBottom;
        if ((sprite == _hole.TopSprite) || (sprite == _water.TopSprite))
        {
            _prefab = _prefabHole;
        }
        if (_landTile.Contains(sprite.name) == true)
        {
            _prefab = _prefabBackground;
        }
        _prefab.GetComponent<SpriteRenderer>().sprite = sprite;
        Instantiate(_prefab, new Vector3(x, y, 0), Quaternion.identity);
    }

    private void NextRandomTile(int y)
    {
        List<PersentInSprite> persentInSpritesList = new List<PersentInSprite>();
        PersentInSprite nextTile = new PersentInSprite(50, _ground.TopSprite, y);

        if (_isBarrier)
        {
            _isBarrier = false;
        }

        if (y == 2)
        {
            persentInSpritesList.Clear();
            persentInSpritesList.Add(new PersentInSprite(50, _ground.TopSprite, y));
            persentInSpritesList.Add(new PersentInSprite(50, _ground.RigthSprite, y - 1));
            nextTile = RandomSprite(persentInSpritesList);
        }

        if (y == -2)
        {
            persentInSpritesList.Clear();
            persentInSpritesList.Add(new PersentInSprite(50, _ground.TopSprite, y));
            persentInSpritesList.Add(new PersentInSprite(50, _ground.AngelLeftSprite, y + 1));
            nextTile = RandomSprite(persentInSpritesList);
        }
        if ((y == 1) || (y == -1))
        {
            persentInSpritesList.Clear();
            persentInSpritesList.Add(new PersentInSprite(60, _ground.TopSprite, y));
            persentInSpritesList.Add(new PersentInSprite(20, _ground.RigthSprite, y - 1));
            persentInSpritesList.Add(new PersentInSprite(20, _ground.AngelLeftSprite, y + 1));
            nextTile = RandomSprite(persentInSpritesList);
        }   
        if (y == 0)
        {
            persentInSpritesList.Clear();
            persentInSpritesList.Add(new PersentInSprite(60, _ground.TopSprite, y));
            persentInSpritesList.Add(new PersentInSprite(10, _ground.RigthSprite, y - 1));
            persentInSpritesList.Add(new PersentInSprite(10, _ground.AngelLeftSprite, y + 1));
            persentInSpritesList.Add(new PersentInSprite(10, _hole.RigthSprite, y));
            persentInSpritesList.Add(new PersentInSprite(10, _water.RigthSprite, y));
            nextTile = RandomSprite(persentInSpritesList);
        }
        _currentSprite = nextTile.Sprite;
        _nextY = nextTile.NextY;
    }
    
    private PersentInSprite RandomSprite(List<PersentInSprite> items)
    {
        PersentInSprite item = items[0];
        int _r = Random.Range(1, 100);
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Persent;
            if (sum > _r)
            {
                item = items[i];
                break;
            }
        }
        return item;
    }
}

public class PersentInSprite
{
    private int persent;
    private Sprite sprite;
    private int nextY;

    public PersentInSprite(int _persent, Sprite _sprite, int _nextY)
    {
        persent = _persent;
        sprite = _sprite;
        nextY = _nextY;
    }

    public int Persent { get => persent; }
    public Sprite Sprite { get => sprite; }
    public int NextY { get => nextY; }
}