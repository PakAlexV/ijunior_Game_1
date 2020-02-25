using UnityEngine;
using UnityEngine.Events;

public class DestroyerLandscape : MonoBehaviour
{
    [SerializeField] private UnityEvent _landscapeDestroyed;

    public event UnityAction LandscapeDestroyed
    {
        add => _landscapeDestroyed.AddListener(value);
        remove => _landscapeDestroyed.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<Ground>() != null) || (collision.gameObject.GetComponent<Hole>() != null))
        {
            _landscapeDestroyed?.Invoke();
        }
        Destroy(collision.gameObject);
    }
}
