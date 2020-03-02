using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLandscape : MonoBehaviour
{
    [SerializeField] private GameObject _parentLandscape;
    [SerializeField] private DestroyerLandscape _destroyerLandscape;
    [SerializeField] private GameObject _templateCoin;
    [SerializeField] private GameObject _templateBarrier;
    private FactoryLandscapeTile _factoryLandscape;
    private DrawTile _drawerTile;

    private Vector3 _currentPosition = new Vector3(0, 0, 0);
    private const int MaxHeightLandscape = 2;
    private const int MinHeightLandscape = -2;
    private const int OffcetHeight = 1;
    private const int DepthLandscape = 5;
    private bool _isGround;

    private void Start()
    {
        _factoryLandscape = FindObjectOfType<FactoryLandscapeTile>();
        _drawerTile = GetComponent<DrawTile>();

        DrawPrimaryLandscape();
    }

    private void DrawPrimaryLandscape()
    {
        int intWidthLandscape = 20;
        for (int i = 0; i < intWidthLandscape; i++)
        {
            DrawNextTiles();
        }
    }

    private void DrawNextTiles()
    {
        List<Tile> tiles = new List<Tile>();
        _isGround = false;
        tiles = GetNextTiles(_currentPosition);
        float underTileHeight = 0;
        for (int i = 1; i < tiles.Count; i++)
        {
            if (tiles[i].Position.y < tiles[i - 1].Position.y)
            {
                underTileHeight = tiles[i].Position.y;
            }
        }
        for (int i = 0; i < tiles.Count; i++)
        {
            _drawerTile.Draw(tiles[i], _parentLandscape);
            if (tiles[i].Position.y != underTileHeight)
            {
                if (_isGround)
                {
                    DrawBonus();
                }
                UpdatePositionLandscape(tiles[i]);
            }
            else
            {
                DrawRecursesGroundDown(tiles[i]);
            }
        }
    }

    private void DrawBonus()
    {
        int percentChanceCoin = 50;
        int percentChanceBarrier = 30;
        if (GetPercentBool(percentChanceCoin))
        {
            Tile coin = new Tile(null, new Vector3(_currentPosition.x, _currentPosition.y + OffcetHeight, 0), _templateCoin);
            _drawerTile.Draw(coin, _parentLandscape);
        }
        else if (GetPercentBool(percentChanceBarrier)) 
        {
            Tile barrier = new Tile(null, new Vector3(_currentPosition.x, _currentPosition.y + OffcetHeight, 0), _templateBarrier);
            _drawerTile.Draw(barrier, _parentLandscape);
        }
    }

    private void UpdatePositionLandscape(Tile tile)
    {
        _currentPosition = tile.Position;
        _currentPosition.x++;
    }

    private void OnTileDestroyed()
    {
        DrawNextTiles();
    }
    
    private void OnDisable()
    {
        _destroyerLandscape.LandscapeDestroyed -= OnTileDestroyed;
    }

    private void OnEnable()
    {
        _destroyerLandscape.LandscapeDestroyed += OnTileDestroyed;
    }
    
    private void DrawRecursesGroundDown(Tile tile)
    {
        int offsetDown = 1;
        if (tile.Position.y - offsetDown > -DepthLandscape)
        {
            tile.Position = new Vector3(tile.Position.x, tile.Position.y - offsetDown, 0);
            _drawerTile.Draw(tile, _parentLandscape);
            DrawRecursesGroundDown(tile);
        }
    }

    private List<Tile> GetNextTiles(Vector3 position)
    {
        if (position.y == MaxHeightLandscape)
        {
            return GetNextUpperRandomTiles(position);
        }
        if (position.y == MinHeightLandscape)
        {
            return GetNextLowerRandomTiles(position);
        }
        return GetNextMidleRandomTiles(position);
    }

    private List<Tile> GetNextUpperRandomTiles(Vector3 position)
    {
        int percentChance = 50;
        List<Tile> tiles = new List<Tile>();
        if (GetPercentBool(percentChance))
        {
            tiles = _factoryLandscape.GetGroundDownTiles(position, OffcetHeight);
        }
        else
        {
            _isGround = true;
            tiles = _factoryLandscape.GetGroundSurfaceTiles(position);
        }
        tiles.AddRange(AddBottomTiles(tiles));
        return tiles;
    }

    private List<Tile> GetNextLowerRandomTiles(Vector3 position)
    {
        int percentChance = 50;
        List<Tile> tiles = new List<Tile>();
        if (GetPercentBool(percentChance))
        {
            tiles = _factoryLandscape.GetGroundUpTiles(position, OffcetHeight);
        }
        else
        {
            _isGround = true;
            tiles = _factoryLandscape.GetGroundSurfaceTiles(position);
        }
        tiles.AddRange(AddBottomTiles(tiles));
        return tiles;
    }

    private List<Tile> AddBottomTiles(List<Tile> tiles)
    {
        Vector3 positionBottom = tiles[0].Position;
        for (int i = 1; i < tiles.Count; i++)
        {
            if (positionBottom.y > tiles[i].Position.y)
            {
                positionBottom.y = tiles[i].Position.y;
            }
        }
        return _factoryLandscape.GetGroundBottomTiles(new Vector3(positionBottom.x, positionBottom.y - OffcetHeight, positionBottom.z));
    }

    private List<Tile> GetNextMidleRandomTiles(Vector3 position)
    {
        int _chanceLandscape = Random.Range(1, 10);
        List<Tile> tiles = new List<Tile>();
        Vector3 bottomPosition = new Vector3(position.x, position.y - OffcetHeight, position.z);
        switch (_chanceLandscape)
        {
            case 1:
                tiles = _factoryLandscape.GetHoleSurfaceTiles(position);
                tiles.AddRange(_factoryLandscape.GetHoleBottomTiles(bottomPosition));
                break;
            case 2:
                tiles = _factoryLandscape.GetWaterSurfaceTiles(position);
                tiles.AddRange(_factoryLandscape.GetWaterBottomTiles(bottomPosition));
                break;
            case 3:
            case 4:
                tiles = _factoryLandscape.GetGroundDownTiles(position, OffcetHeight);
                tiles.AddRange(AddBottomTiles(tiles));
                break;
            case 5:
            case 6:
                tiles = _factoryLandscape.GetGroundUpTiles(position, OffcetHeight);
                tiles.AddRange(AddBottomTiles(tiles));
                break;
            default:
                _isGround = true;
                tiles = _factoryLandscape.GetGroundSurfaceTiles(position);
                tiles.AddRange(AddBottomTiles(tiles));
                break;
        }
        return tiles;
    }

    private bool GetPercentBool(int percent)
    {
        int randomChance = Random.Range(0, 100);
        return percent > randomChance ? true : false;
    }
}
