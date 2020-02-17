using UnityEngine;

public class GeneratorLandscape : MonoBehaviour
{
    [SerializeField] private EnvironmentPalite _ground;
    [SerializeField] private EnvironmentPalite _hole;
    [SerializeField] private EnvironmentPalite _water;

    [SerializeField] private GameObject _prefabBackground;
    [SerializeField] private GameObject _prefabBackgroundBottom;
    [SerializeField] private GameObject _prefabHole;
    [SerializeField] private GameObject _prefabBarrier;
    [SerializeField] private GameObject _prefabCoin;

    [SerializeField] [Range(3, 6)] private int _whatTileDown = 5;

    private int _initLocationMin = -8;
    private int _initLocationMax = 10;

    private int _currentX;
    private int _currentY = 0;
    private int _lastBarrierX = 0;       
    private Sprite _currentSprite;
    
    public void OnDestroyTile()
    {
        DrawTile();
    }

    private void Start()
    {
        DrawPrimaryLandscape();
    }

    private void DrawPrimaryLandscape()
    {
        _currentSprite = _ground.TopSprite;
        _currentX = _initLocationMin;
        while (_currentX <= _initLocationMax)
        {
            DrawTile();
        }
    }

    private void InstantiateTile(int x, int y, Sprite sprite, GameObject prefab)
    {
        prefab.GetComponent<SpriteRenderer>().sprite = sprite;
        Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
    }

    private void DrawTile()
    {
        NextRandomTile();        
        _currentX++;
    }
    
    private void NextRandomTile()
    {
        if (_currentY == 2)
        {
            MaxHeightGround();
        }
        else if (_currentY == -2)
        {
            MinHeightGround();
        }
        else
        {
            MidleHeightGround();
        }
    }

    private void MidleHeightGround()
    {
        int _r = Random.Range(1, 8);
        switch (_r)
        {
            case 1:
                DrawHole();
                break;
            case 2:
                DrawWater();
                break;
            case 3:
            case 4:
                DrawDescentGround();
                break;
            case 5:
            case 6:
                DrawRiseGround();
                break;
            default:
                DrawGround();
                break;
        }
    }

    private void MaxHeightGround()
    {
        int _r = Random.Range(0, 2);
        if (_r == 0)
        {
            DrawGround();
        } 
        else
        {
            DrawDescentGround();
        }
    }

    private void MinHeightGround()
    {
        int _r = Random.Range(0, 2);
        if (_r == 0)
        {
            DrawGround();
        }
        else
        {
            DrawRiseGround();
        }
    }
        
    private void DrawRiseGround()
    {
        InstantiateTile(_currentX, _currentY, _ground.AngelLeftSprite, _prefabBackgroundBottom);
        DrawGroundDown(_currentX, _currentY - 1, _ground.BottomSprite, _prefabBackgroundBottom);
        _currentY++;
        InstantiateTile(_currentX, _currentY, _ground.LeftSprite, _prefabBackground);
    }

    private void DrawDescentGround()
    {
        InstantiateTile(_currentX, _currentY, _ground.RigthSprite, _prefabBackground);
        _currentY--;
        InstantiateTile(_currentX, _currentY, _ground.AngelRigthSprite, _prefabBackgroundBottom);
        DrawGroundDown(_currentX, _currentY - 1, _ground.BottomSprite, _prefabBackgroundBottom);
    }

    private void DrawHole()
    {
        InstantiateTile(_currentX, _currentY, _hole.RigthSprite, _prefabBackground);
        DrawGroundDown(_currentX, _currentY - 1, _hole.BottomRigthSprite, _prefabBackgroundBottom);
        _currentX++;
        InstantiateTile(_currentX, _currentY, _hole.TopSprite, _prefabHole);
        DrawGroundDown(_currentX, _currentY - 1, _hole.BottomSprite, _prefabBackgroundBottom);
        _currentX++;
        InstantiateTile(_currentX, _currentY, _hole.LeftSprite, _prefabBackground);
        DrawGroundDown(_currentX, _currentY - 1, _hole.BottomLeftSprite, _prefabBackgroundBottom);
    }

    private void DrawWater()
    {
        InstantiateTile(_currentX, _currentY, _water.RigthSprite, _prefabBackground);
        DrawGroundDown(_currentX, _currentY - 1, _water.BottomRigthSprite, _prefabBackgroundBottom);
        _currentX++;
        InstantiateTile(_currentX, _currentY, _water.TopSprite, _prefabHole);
        DrawGroundDown(_currentX, _currentY - 1, _water.BottomSprite, _prefabBackgroundBottom);
        _currentX++;
        InstantiateTile(_currentX, _currentY, _water.LeftSprite, _prefabBackground);
        DrawGroundDown(_currentX, _currentY - 1, _water.BottomLeftSprite, _prefabBackgroundBottom);
    }

    private void DrawGround()
    {
        InstantiateTile(_currentX, _currentY, _ground.TopSprite, _prefabBackground);
        DrawGroundDown(_currentX, _currentY - 1, _ground.BottomSprite, _prefabBackgroundBottom);
        DrawBonus();
    }

    private void DrawGroundDown(int x, int y, Sprite sprite, GameObject prefab)
    {
        InstantiateTile(x, y, sprite, prefab);
        if (y - 1 > -_whatTileDown)
        {
            DrawGroundDown(x, y - 1, sprite, prefab);
        }
    }

    private void DrawBonus()
    {
        int _r = Random.Range(1, 10);
        if (_r > 5)
        {
            Instantiate(_prefabCoin, new Vector3(_currentX, _currentY + 1, 0), Quaternion.identity);
        }
        if ((_r < 3) && (_lastBarrierX != _currentX - 1))
        {
            Instantiate(_prefabBarrier, new Vector3(_currentX, _currentY + 1, 0), Quaternion.identity);
            _lastBarrierX = _currentX;
        }
    }
}