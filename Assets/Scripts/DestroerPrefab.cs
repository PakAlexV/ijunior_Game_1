using UnityEngine;
using UnityEngine.Events;

public class DestroerPrefab : MonoBehaviour
{
    public UnityEvent OnDestroyPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<Ground>() != null) || (collision.gameObject.GetComponent<Hole>() != null))
        {
            OnDestroyPrefab.Invoke();
        }
        Destroy(collision.gameObject);
    }
}
