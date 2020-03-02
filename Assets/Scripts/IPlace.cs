using UnityEngine;

public interface IPlace
{    
    EnvironmentPalite EnvironmentPalite { get; set; }
    DTOSprite GetPlace(Sprite sprite = null);
    DTOSprite GetBotoomPlace(Sprite sprite = null);
    GameObject TemplateSurface { get; set; }
    GameObject TemplateBottom { get; set; }
}
