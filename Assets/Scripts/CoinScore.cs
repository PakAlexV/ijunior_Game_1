using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _score = 0;

    public void AddScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
}
