using UnityEngine;

public class DTOSprite
{
    public Sprite Sprite { get; set; }
    public GameObject Prefab { get; set; }

    public DTOSprite(Sprite sprite, GameObject prefab)
    {
        Sprite = sprite;
        Prefab = prefab;
    }
}