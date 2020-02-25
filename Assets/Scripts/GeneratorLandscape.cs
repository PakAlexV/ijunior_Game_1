using UnityEngine;

[RequireComponent(typeof(ReceiveSprites))]
[RequireComponent(typeof(DrawTile))]
public class GeneratorLandscape : MonoBehaviour
{
    [SerializeField] private GameObject _landscapeParent;
    [SerializeField] private DestroyerLandscape _destroerLandscape;

    [SerializeField] [Range(3, 6)] private int _depthLandscape = 5;
    [SerializeField] [Range(10, 25)] private int _initWidthLandscape = 22;

    [SerializeField] private GameObject _prefabSurfase;
    [SerializeField] private GameObject _prefabBottom;
    [SerializeField] private GameObject _prefabHole;
    [SerializeField] private GameObject _prefabBarrier;
    [SerializeField] private GameObject _prefabCoin;

    private ReceiveSprites _receiveSprites;
    private DrawTile _drawTile;
    private Vector2 _currentPositionLandscepe = new Vector2(0, 0);
    private Sprite nextSprite;
    private bool _canDrawBarriers = false;

    private void OnTileDestroyed()
    {
        DrawLandscape();
        UpdatePositionLandscape(nextSprite);
    }

    private void OnDisable()
    {
        _destroerLandscape.LandscapeDestroyed -= OnTileDestroyed;
    }

    private void OnEnable()
    {
        _destroerLandscape.LandscapeDestroyed += OnTileDestroyed;
    }

    private void Start()
    {
        _receiveSprites = FindObjectOfType<ReceiveSprites>();
        _drawTile = FindObjectOfType<DrawTile>();
        DrawPrimaryLandscape();
    }

    private void UpdatePositionLandscape(Sprite sprite)
    {
        if (_receiveSprites.GetUpLandscape(sprite))
        {
            _currentPositionLandscepe.y++;
        }
        else if (_receiveSprites.GetDownLandscape(sprite))
        {
            _currentPositionLandscepe.y--;
        }
        else
        {
            _currentPositionLandscepe.x++;
        }
    }

    private void DrawPrimaryLandscape()
    {
        while (_currentPositionLandscepe.x < _initWidthLandscape)
        {
            DrawLandscape();
            UpdatePositionLandscape(nextSprite);
        }
    }

    private void DrawBonus()
    {
        int offsetOverSurfase = 1;
        if (GetPercentBool(30))
        {
            DrawTile(new Vector3(_currentPositionLandscepe.x, _currentPositionLandscepe.y + offsetOverSurfase, 0), _prefabCoin);
        }
        else if (GetPercentBool(30) && CanDrawBarrier())
        {
            DrawTile(new Vector3(_currentPositionLandscepe.x, _currentPositionLandscepe.y + offsetOverSurfase, 0), _prefabBarrier);
        }
    }

    private bool CanDrawBarrier()
    {
        if (_canDrawBarriers)
        {
            _canDrawBarriers = false;
        }
        else
            _canDrawBarriers = true;
        return _canDrawBarriers;
    }

    private void DrawLandscape()
    {
        DrawGenereteTile(nextSprite);
        DrawBottomTiles();
        if (_receiveSprites.IsSurface(nextSprite))
        {
            DrawBonus();
        }
    }

    private void DrawTile(Vector3 position, GameObject prefab, Sprite sprite = null)
    {
        Tile tile = new Tile(sprite, position, prefab, _landscapeParent);
        _drawTile.Draw(tile);
    }

    private void DrawGenereteTile(Sprite sprite)
    {
        nextSprite = _receiveSprites.GetNextSprite(sprite, _currentPositionLandscepe.y);
        Vector3 nextPosition = new Vector3(_currentPositionLandscepe.x, _currentPositionLandscepe.y, 0);
        GameObject prefab = _prefabSurfase;
        if (_receiveSprites.IsGap(nextSprite))
        {
            prefab = _prefabHole;
        }
        DrawTile(nextPosition, prefab, nextSprite);
    }

    private void DrawBottomTiles()
    {
        if (_receiveSprites.CanBottomSprite(nextSprite))
        {
            int offsetDown = 1;
            Sprite bottomSprite = _receiveSprites.GetBottomSprite(nextSprite);
            Vector3 position = new Vector3(_currentPositionLandscepe.x, _currentPositionLandscepe.y - offsetDown, 0);
            Tile tile = new Tile(bottomSprite, position, _prefabBottom, _landscapeParent);
            DrawRecursesGroundDown(tile);
        }
    }

    private void DrawRecursesGroundDown(Tile tile)
    {
        int offsetDown = 1;
        _drawTile.Draw(tile);
        if (tile.Position.y - offsetDown > -_depthLandscape)
        {
            tile.Position = new Vector3(tile.Position.x, tile.Position.y - offsetDown, 0);
            DrawRecursesGroundDown(tile);
        }
    }

    private bool GetPercentBool(int percent)
    {
        int randomChance = Random.Range(0, 100);
        return percent > randomChance ? true : false;
    }
}
