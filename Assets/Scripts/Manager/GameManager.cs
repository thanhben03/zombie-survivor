using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject endGameMenu;
    public GameObject winGameMenu;

    private GameObject player;
    public AudioSource audioSource { get; private set; }
    public event Action<GameObject> OnPlayerReady;

    void Awake()
    {
        Instance = this;
        endGameMenu.SetActive(false);
        this.audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        endGameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0f;
    }

    public void WinGame()
    {
        winGameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public GameObject GetCurrentPlayer()
    {
        return player;
    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
        OnPlayerReady?.Invoke(player);
    }
}
