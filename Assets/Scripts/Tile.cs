using UnityEngine;

public class Tile 
{
    private Sprite _sprite;
    private Vector3 _position;
    private GameObject _prefab;
    private GameObject _parent;

    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public Vector3 Position { get => _position; set => _position = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public GameObject Parent { get => _parent; set => _parent = value; }

    public Tile(Sprite sprite, Vector3 position, GameObject prefab, GameObject parent)
    {
        _sprite = sprite;
        _position = position;
        _prefab = prefab;
        _parent = parent;
    }
}
