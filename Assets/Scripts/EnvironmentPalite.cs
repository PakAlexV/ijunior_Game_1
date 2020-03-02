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

    [SerializeField] private GameObject _template;

    public Sprite SurfaceSprite { get => _surfaceSprite; }
    public Sprite LeftSprite { get => _leftSprite; }
    public Sprite RigthSprite { get => _rigthSprite; }
    public Sprite BottomSprite { get => _bottomSprite; }
    public Sprite BottomLeftSprite { get => _bottomLeftSprite; }
    public Sprite BottomRigthSprite { get => _bottomRigthSprite; }
    public Sprite AngelLeftSprite { get => _angelLeftSprite; }
    public Sprite AngelRigthSprite { get => _angelRigthSprite; }
    public TypePalites TypePalite { get => _typePalite; }
    public GameObject Template { get => _template; }
}
