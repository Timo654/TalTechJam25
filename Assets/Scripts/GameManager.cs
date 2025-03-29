using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EndingData[] endings;
    public static event Action<bool> GameActive;
    public static event Action<int> OnGameTimeChanged;
    public static event Action<float> BoostSpeed;
    private bool gameActive = false;
    private float currentTime = 60f;
    private int lastTimeValue;
    private int currentPhase = 0;

    private void OnEnable()
    {
        LevelChanger.OnFadeInFinished += StartGame;
        GameActive += SetActive;
        ChaosCounter.SendEndScore += EndGame;
    }
    private void OnDisable()
    {
        LevelChanger.OnFadeInFinished -= StartGame;
        GameActive -= SetActive;
        ChaosCounter.SendEndScore -= EndGame;
    }

    private void SetActive(bool val)
    {
        gameActive = val;
    }

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        LevelChanger.Instance.FadeIn();
        // PLAY GAMEPLAY MUSIC HERE
    }
    private void StartGame()
    {
        GameActive.Invoke(true);
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (!gameActive) return;
        if (currentTime <= 0)
        {
            GameActive?.Invoke(false);
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        if (lastTimeValue != (int)currentTime)
        {
            lastTimeValue = (int)currentTime;
            OnGameTimeChanged?.Invoke(lastTimeValue);
        }

        if (currentPhase == 0 && currentTime < 30f)
        {
            BoostSpeed?.Invoke(5f);
            currentPhase++; // enter phase 2, which is faster
            // SWITCH TO PHASE 2 THEME HERE
            AudioManager.Instance.SetSwitch(WWiseEvents.Instance.GameMusic2);
        }
    }

    private void EndGame(EndingType endingType)
    {
        foreach (EndingData ending in endings)
        {
            if (ending.endingType == endingType)
            {
                SaveManager.Instance.runtimeData.currentEnding = ending;
                break;
            }
        }
        SaveManager.Instance.gameData.hasClearedOnce = true;
        LevelChanger.Instance.FadeToLevel("Ending");
    }
}
