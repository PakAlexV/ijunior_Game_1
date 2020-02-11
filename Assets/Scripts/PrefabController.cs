using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrefabController : MonoBehaviour
{
    public UnityEvent OnEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string name = collision.gameObject.GetComponent<SpriteRenderer>().sprite.name;

        if ((collision.gameObject.tag == "land") ||(collision.gameObject.tag == "hole"))
        {
            OnEvent.Invoke();
        }
        Destroy(collision.gameObject);
    }
}
