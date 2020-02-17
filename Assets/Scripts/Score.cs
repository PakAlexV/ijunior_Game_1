using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _score = 0;

    public void AddScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
}
