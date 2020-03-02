using System.Collections.Generic;
using UnityEngine;

public class GapPlace : IPlace
{
    private GameObject _prefabGap;

    public EnvironmentPalite EnvironmentPalite { get; set; }
    public GameObject TemplateSurface { get; set; }
    public GameObject TemplateBottom { get; set; }

    public GapPlace(EnvironmentPalite environmentPalite, GameObject prefabSurface, GameObject prefabBottom, GameObject prefabGap)
    {
        EnvironmentPalite = environmentPalite;
        TemplateSurface = prefabSurface;
        TemplateBottom = prefabBottom;
        _prefabGap = prefabGap;
    }


    public DTOSprite GetBotoomPlace(Sprite sprite)
    {
        return GetSpriteBottom(sprite);
    }

    public DTOSprite GetPlace(Sprite sprite)
    {
        return GetSpriteSurface(sprite);
    }

    private DTOSprite GetSpriteSurface(Sprite sprite)
    {
        if (sprite == EnvironmentPalite.RigthSprite)
        {
            return new DTOSprite(EnvironmentPalite.SurfaceSprite, _prefabGap);
        }
        if (sprite == EnvironmentPalite.SurfaceSprite)
        {
            return new DTOSprite(EnvironmentPalite.LeftSprite, TemplateSurface);
        }
        return new DTOSprite(EnvironmentPalite.RigthSprite, TemplateSurface);
    }

    private DTOSprite GetSpriteBottom(Sprite sprite)
    {
        if (sprite == EnvironmentPalite.BottomRigthSprite)
        {
            return new DTOSprite(EnvironmentPalite.BottomSprite, _prefabGap);
        }
        if (sprite == EnvironmentPalite.BottomSprite)
        {
            return new DTOSprite(EnvironmentPalite.BottomLeftSprite, TemplateBottom);
        }
        return new DTOSprite(EnvironmentPalite.BottomRigthSprite, TemplateBottom);
    }
}
