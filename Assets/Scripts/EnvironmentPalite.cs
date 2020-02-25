using System.Collections.Generic;
using UnityEngine;

public enum TypePalites
{
    Ground,
    Hole,
    Water
}

[CreateAssetMenu(fileName = "New SpritePalite", menuName = "SpritePalite", order = 51)]
public class EnvironmentPalite : ScriptableObject
{
    [SerializeField] private TypePalites _typePalite;
    [SerializeField] private Sprite _surfaceSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rigthSprite;

    [SerializeField] private Sprite _bottomSprite;
    [SerializeField] private Sprite _bottomLeftSprite;
    [SerializeField] private Sprite _bottomRigthSprite;

    [SerializeField] private Sprite _angelLeftSprite;
    [SerializeField] private Sprite _angelRigthSprite;

    public Sprite SurfaceSprite { get => _surfaceSprite; }
    public Sprite LeftSprite { get => _leftSprite; }
    public Sprite RigthSprite { get => _rigthSprite; }
    public Sprite BottomSprite { get => _bottomSprite; }
    public Sprite BottomLeftSprite { get => _bottomLeftSprite; }
    public Sprite BottomRigthSprite { get => _bottomRigthSprite; }
    public Sprite AngelLeftSprite { get => _angelLeftSprite; }
    public Sprite AngelRigthSprite { get => _angelRigthSprite; }
    public TypePalites TypePalite { get => _typePalite; }
    
    public Sprite NextExtraSprite(Sprite sprite)
    {
        if (TypePalites.Ground == _typePalite)
        {
            if (sprite == _angelLeftSprite)
            {
                return LeftSprite;
            }
            if (sprite == _rigthSprite)
            {
                return AngelRigthSprite;
            }
        }
        else
        {
            if (sprite == _rigthSprite)
            {
                return SurfaceSprite;
            }
            if (sprite == _surfaceSprite)
            {
                return LeftSprite;
            }
        }
        return null;
    }

    public Sprite GetBottomSprite(Sprite sprite)
    {
        if (sprite == _leftSprite)
        {
            return BottomLeftSprite;
        }
        if (sprite == _rigthSprite)
        {
            return BottomRigthSprite;
        }
        if (sprite == _surfaceSprite)
        {
            return BottomSprite;
        }
        return null;
    }
}
