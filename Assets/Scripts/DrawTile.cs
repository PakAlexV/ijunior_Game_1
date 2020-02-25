using UnityEngine;

public class DrawTile : MonoBehaviour
{
    public void Draw(Tile tile)
    {
        InstantiateTile(tile.Sprite, tile.Position, tile.Parent, tile.Prefab);
    }

    private void InstantiateTile(Sprite sprite, Vector3 position, GameObject parent, GameObject prefab)
    {
        GameObject gameObject = Instantiate(prefab, position, Quaternion.identity, parent.transform);
        if (sprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
