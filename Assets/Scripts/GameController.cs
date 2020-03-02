using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _panelAbout;
    [SerializeField] private GameObject _panelGameOver;

    public void OnGameOver()
    {
        Time.timeScale = 0;
        _panelGameOver.SetActive(true);
    }

    public void OnOpenMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void OnNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnOpenPanelAbout()
    {
        _panelAbout.SetActive(true);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnClosePanelAbout()
    {
        _panelAbout.SetActive(false);
    }    
}
