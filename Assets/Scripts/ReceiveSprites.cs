using System.Collections.Generic;
using UnityEngine;

public class ReceiveSprites : MonoBehaviour
{
    [SerializeField] private EnvironmentPalite _water;
    [SerializeField] private EnvironmentPalite _ground;
    [SerializeField] private EnvironmentPalite _hole;

    private List<EnvironmentPalite> _environmentPalitesList;


    public bool GetUpLandscape(Sprite sprite)
    {
        return sprite == _ground.AngelLeftSprite ? true : false;
    }

    public bool GetDownLandscape(Sprite sprite)
    {
        return sprite == _ground.RigthSprite ? true : false;
    }

    public bool CanBottomSprite(Sprite sprite)
    {
        if ((sprite == _ground.RigthSprite) || (sprite == _ground.LeftSprite))
        {
            return false;
        }
        return true;
    }

    public bool IsSurface(Sprite sprite)
    {
        return sprite == _ground.SurfaceSprite ? true : false;
    }

    public bool IsGap(Sprite sprite)
    {
        if ((sprite == _water.SurfaceSprite) || (sprite == _hole.SurfaceSprite))
        {
            return true;
        }
        return false;
    }

    public Sprite GetBottomSprite(Sprite sprite)
    {
        Sprite bottomSprite = null;
        foreach (EnvironmentPalite palite in _environmentPalitesList)
        {
            bottomSprite = palite.GetBottomSprite(sprite);
            if (bottomSprite != null)
            {
                break;
            }
        }
        if (bottomSprite == null)
        {
            return _ground.BottomSprite;
        }
        return bottomSprite;
    }

    public Sprite GetNextSprite(Sprite currentSprite, float currentHeight)
    {
        if (currentSprite == null)
        {
            currentSprite = _ground.SurfaceSprite;
        }
        Sprite nextSprite;
        nextSprite = GetExtraSprite(currentSprite);
        
        float maxHeightLandscape = 2;
        float minHeightLandscape = -2;
        if (nextSprite == null)
        {
            if (currentHeight == maxHeightLandscape)
            {
                nextSprite = GetNextRandomUpperSprite();
            }
            else if (currentHeight == minHeightLandscape)
            {
                nextSprite = GetNextRandomLowerSprite();
            }
            else
            {
                nextSprite = GetNextRandomMidleSprite();
            }
        }

        return nextSprite;
    }

    private void Start()
    {
        _environmentPalitesList = new List<EnvironmentPalite>() { _ground, _hole, _water };
    }

    private Sprite GetExtraSprite(Sprite sprite)
    {
        Sprite extraSprite = null;
        foreach (EnvironmentPalite palite in _environmentPalitesList)
        {
            extraSprite = palite.NextExtraSprite(sprite);
            if (extraSprite != null)
            {
                break;
            }
        }
        return extraSprite;
    }

    private Sprite GetNextRandomUpperSprite()
    {
        int percentChance = 50;
        if (GetPercentBool(percentChance))
        {
            return _ground.RigthSprite;
        }
        return _ground.SurfaceSprite;
    }

    private Sprite GetNextRandomLowerSprite()
    {
        int percentChance = 50;
        if (GetPercentBool(percentChance))
        {
            return _ground.AngelLeftSprite;
        }
        return _ground.SurfaceSprite;
    }

    private Sprite GetNextRandomMidleSprite()
    {
        int _chanceLandscape = Random.Range(1, 10);
        switch (_chanceLandscape)
        {
            case 1:
                return _hole.RigthSprite;
            case 2:
                return _water.RigthSprite;
            case 3:
            case 4:
                return _ground.RigthSprite;
            case 5:
            case 6:
                return _ground.AngelLeftSprite;
            default:
                break;
        }
        return _ground.SurfaceSprite;
    }

    private bool GetPercentBool(int percent)
    {
        int randomChance = Random.Range(0, 100);
        return percent > randomChance ? true : false;
    }
}
