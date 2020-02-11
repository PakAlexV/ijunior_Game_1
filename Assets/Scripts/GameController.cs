﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _panelAbout = default;
    [SerializeField] private GameObject _panelGameOver = default;

    public void GameOver()
    {
        Time.timeScale = 0;
        _panelGameOver.SetActive(true);
    }

    public void ReloadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void NewGameBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void AboutBtn()
    {
        _panelAbout.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void OnClick()
    {
        _panelAbout.SetActive(false);
    }

}
