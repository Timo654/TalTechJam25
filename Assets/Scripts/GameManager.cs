using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<bool> GameActive;
    private void OnEnable()
    {
        LevelChanger.OnFadeInFinished += StartGame;
    }
    private void OnDisable()
    {
        LevelChanger.OnFadeInFinished -= StartGame;
    }

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        LevelChanger.Instance.FadeIn();
    }
    private void StartGame()
    {
        GameActive.Invoke(true);
        Time.timeScale = 1.0f;
    }
}
