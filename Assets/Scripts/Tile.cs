using UnityEngine;

public class Tile
{
    public Sprite Sprite { get; set; }
    public Vector3 Position { get; set; }
    public GameObject Template { get; set; }

    public Tile(Sprite sprite, Vector3 position, GameObject template)
    {
        Sprite = sprite;
        Position = position;
        Template = template;
    }
}
