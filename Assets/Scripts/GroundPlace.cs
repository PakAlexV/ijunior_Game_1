using System.Collections.Generic;
using UnityEngine;

public class GroundPlace : IPlace
{
    public EnvironmentPalite EnvironmentPalite { get; set; }
    public GameObject TemplateSurface { get; set; }
    public GameObject TemplateBottom { get; set; }

    public GroundPlace(EnvironmentPalite environmentPalite, GameObject prefabSurface, GameObject prefabBottom)
    {
        EnvironmentPalite = environmentPalite;
        TemplateSurface = prefabSurface;
        TemplateBottom = prefabBottom;
    }
    
    public DTOSprite GetBotoomPlace(Sprite sprite = null)
    {
        return new DTOSprite(EnvironmentPalite.BottomSprite, TemplateBottom);
    }

    public DTOSprite GetPlace(Sprite sprite = null)
    {
        return new DTOSprite(EnvironmentPalite.SurfaceSprite, TemplateSurface);
    }

    public DTOSprite GetShiftPlace(bool isUp, Sprite sprite = null)
    {
        if (isUp)
        {
            return GetUpSprite(sprite);
        }
        return GetDownSprite(sprite);
    }

    private DTOSprite GetUpSprite(Sprite sprite)
    {
        if (sprite == EnvironmentPalite.AngelLeftSprite)
        {
            return new DTOSprite(EnvironmentPalite.LeftSprite, TemplateSurface);
        }
        return new DTOSprite(EnvironmentPalite.AngelLeftSprite, TemplateBottom);
    }

    private DTOSprite GetDownSprite(Sprite sprite)
    {
        if (sprite == EnvironmentPalite.RigthSprite)
        {
            return new DTOSprite(EnvironmentPalite.AngelRigthSprite, TemplateBottom);
        }
        return new DTOSprite(EnvironmentPalite.RigthSprite, TemplateSurface);
    }
}