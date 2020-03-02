using UnityEngine;

public class DrawTile : MonoBehaviour
{
    public void Draw(Tile tile, GameObject parent)
    {
        InstantiateTile(tile.Sprite, tile.Position, tile.Template, parent);
    }

    private void InstantiateTile(Sprite sprite, Vector3 position, GameObject template, GameObject parent)
    {
        GameObject gameObject = Instantiate(template, position, Quaternion.identity, parent.transform);
        if (sprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
