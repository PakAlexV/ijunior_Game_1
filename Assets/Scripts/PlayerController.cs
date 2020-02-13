using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _selfTransform;
    [SerializeField] private Rigidbody2D _selfRigidbody;
    [SerializeField][Range(2, 7)] private int _jumpForce = 5;
    [SerializeField][Range(1, 5)] private int _speed = 2;
    private Vector3 _nextPos;
    private bool _canJump;

    public UnityEvent getCoins;
    public UnityEvent gameOver;

    private void Start()
    {
        _nextPos = new Vector3(_selfTransform.position.x + 1f, _selfTransform.position.y, 0);
    }
    
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && _canJump)
        {
            _canJump = false;
            _selfRigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _selfTransform.position = Vector3.MoveTowards(_selfTransform.position, _nextPos, Time.deltaTime * _speed);
        }
        _selfTransform.position = Vector3.MoveTowards(_selfTransform.position, _nextPos, Time.deltaTime * _speed);
        if (_selfTransform.position.x - _nextPos.x < 0.3f)
        {
            _nextPos.x = _nextPos.x + 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null) { _canJump = true; }
        if (collision.gameObject.GetComponent<Coin>() != null)
        {
            getCoins.Invoke();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<Hole>() != null) || (collision.gameObject.GetComponent<Barrier>() != null))
        {
            Time.timeScale = 0;
            gameOver.Invoke();
        }        
    }
}
