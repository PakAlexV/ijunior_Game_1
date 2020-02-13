using UnityEngine;

[CreateAssetMenu(fileName = "New SpritePalite", menuName = "SpritePalite", order = 51)]
public class EnvironmentPalite : ScriptableObject
{
    [SerializeField] private Sprite topSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rigthSprite;
    [SerializeField] private Sprite bottomSprite;
    [SerializeField] private Sprite angelLeftSprite;
    [SerializeField] private Sprite angelRigthSprite;
    [SerializeField] private Sprite bottomLeftSprite;
    [SerializeField] private Sprite bottomRigthSprite;

    public Sprite TopSprite { get => topSprite; }
    public Sprite LeftSprite { get => leftSprite; }
    public Sprite RigthSprite { get => rigthSprite; }
    public Sprite BottomSprite { get => bottomSprite; }
    public Sprite AngelLeftSprite { get => angelLeftSprite; }
    public Sprite AngelRigthSprite { get => angelRigthSprite; }
    public Sprite BottomLeftSprite { get => bottomLeftSprite; }
    public Sprite BottomRigthSprite { get => bottomRigthSprite; }
}
