using UnityEngine;
using UnityEngine.Events;

public class PrefabController : MonoBehaviour
{
    public UnityEvent onEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<Ground>() != null) || (collision.gameObject.GetComponent<Hole>() != null))
        {
            onEvent.Invoke();
        }
        Destroy(collision.gameObject);
    }
}
