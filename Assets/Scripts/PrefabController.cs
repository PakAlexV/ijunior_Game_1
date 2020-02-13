using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrefabController : MonoBehaviour
{
    public UnityEvent OnEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<Ground>() != null) || (collision.gameObject.GetComponent<Hole>() != null))
        {
            OnEvent.Invoke();
        }
        Destroy(collision.gameObject);
    }
}
