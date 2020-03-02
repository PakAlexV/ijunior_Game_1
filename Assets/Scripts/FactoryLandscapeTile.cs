using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TypeSection
{
    HoleSurfase,
    HoleBottom,
    WaterSurface,
    WaterBottom
}

public class FactoryLandscapeTile : MonoBehaviour
{
    [SerializeField] private EnvironmentPalite _waterPalite;
    [SerializeField] private EnvironmentPalite _groundPalite;
    [SerializeField] private EnvironmentPalite _holePalite;
    [SerializeField] private GameObject _templateBottom;
    private GapPlace _waterPlace;
    private GapPlace _holePlace;
    private GroundPlace _groundPlace;
    private Vector3 _offsetPosition = new Vector3(0, 1, 0);

    private void Awake()
    {
        _groundPlace = new GroundPlace(_groundPalite, _groundPalite.Template, _templateBottom);
        _waterPlace = new GapPlace(_waterPalite, _groundPalite.Template, _templateBottom, _waterPalite.Template);
        _holePlace = new GapPlace(_holePalite, _groundPalite.Template, _templateBottom, _holePalite.Template);        
    }

    public bool IsSurface(Tile tile)
    {
        return tile.Template == _groundPalite.Template ? true : false;
    }
    
    public List<Tile> GetWaterSurfaceTiles(Vector3 position)
    {
        return GetGapSection(position, TypeSection.WaterSurface);
    }

    public List<Tile> GetWaterBottomTiles(Vector3 position)
    {
        return GetGapSection(position, TypeSection.WaterBottom);
    }

    public List<Tile> GetHoleSurfaceTiles(Vector3 position)
    {
        return GetGapSection(position, TypeSection.HoleSurfase);
    }

    public List<Tile> GetHoleBottomTiles(Vector3 position)
    {
        return GetGapSection(position, TypeSection.HoleBottom);
    }

    public List<Tile> GetGroundSurfaceTiles(Vector3 position)
    {
        DTOSprite dtoSprite = _groundPlace.GetPlace();
        return new List<Tile>
        {
            new Tile(dtoSprite.Sprite, position, dtoSprite.Prefab)
        };
    }

    public List<Tile> GetGroundBottomTiles(Vector3 position)
    {
        DTOSprite dtoSprite = _groundPlace.GetBotoomPlace();
        return new List<Tile>
        {
            new Tile(dtoSprite.Sprite, position, dtoSprite.Prefab)
        }; 
    }

    public List<Tile> GetGroundUpTiles(Vector3 position, int offsetHeight)
    {
        return GetGroundShiftTiles(position, offsetHeight);
    }

    public List<Tile> GetGroundDownTiles(Vector3 position, int offsetHeight)
    {
        return GetGroundShiftTiles(position, -offsetHeight);
    }

    private List<Tile> GetGroundShiftTiles(Vector3 position, int offsetHeight)
    {
        List<Tile> tiles = new List<Tile>();
        bool isUp = false;
        if (offsetHeight > 0)
        {
            isUp = true;
        }
        DTOSprite dtoSprite = _groundPlace.GetShiftPlace(isUp);
        tiles.Add(new Tile(dtoSprite.Sprite, position, dtoSprite.Prefab));

        dtoSprite = _groundPlace.GetShiftPlace(isUp, dtoSprite.Sprite);
        position = new Vector3(position.x, position.y + offsetHeight, 0);
        tiles.Add(new Tile(dtoSprite.Sprite, position, dtoSprite.Prefab));
        return tiles;
    }

    private List<Tile> GetGapSection(Vector3 position, TypeSection type)
    {
        List<Tile> tiles = new List<Tile>();
        DTOSprite dtoSprite;
        Sprite sprite = null;
        for (int i = 0; i < 3; i++)
        {
            dtoSprite = GetDTOSprite(sprite, type);
            sprite = dtoSprite.Sprite;
            tiles.Add(new Tile(dtoSprite.Sprite, position, dtoSprite.Prefab));
            if (i != 3)
            { 
                position.x++;
            }
        }
        return tiles;
    }

    private DTOSprite GetDTOSprite(Sprite sprite, TypeSection type)
    {
        if (type == TypeSection.HoleBottom)
        {
            return _holePlace.GetBotoomPlace(sprite);
        }
        else if (type == TypeSection.HoleSurfase)
        {
            return _holePlace.GetPlace(sprite);
        }
        else if (type == TypeSection.WaterBottom)
        {
            return _waterPlace.GetBotoomPlace(sprite);
        }
        else
        {
            return _waterPlace.GetPlace(sprite);
        }
    }
}
