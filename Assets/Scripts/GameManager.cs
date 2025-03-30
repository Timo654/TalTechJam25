using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen;
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
        //LevelChanger.OnFadeInFinished += StartGame;
        GameActive += SetActive;
        ChaosCounter.SendEndScore += EndGame;
        TutorialScreen.OnTutorialClosed += StartGame;
    }
    private void OnDisable()
    {
        //LevelChanger.OnFadeInFinished -= StartGame;
        GameActive -= SetActive;
        ChaosCounter.SendEndScore -= EndGame;
        TutorialScreen.OnTutorialClosed -= StartGame;
    }

    private void SetActive(bool val)
    {
        gameActive = val;
    }

    private void Awake()
    {
        Time.timeScale = 0f;
        tutorialScreen.SetActive(true);
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

                switch (endingType)
                {
                    case EndingType.Good:
                        SaveManager.Instance.gameData.f_goodCleared = true;
                        break;
                    case EndingType.Bad:
                        SaveManager.Instance.gameData.f_trashCleared = true;
                        break;
                    case EndingType.Neutral:
                        SaveManager.Instance.gameData.f_neutralCleared = true;
                        break;
                    case EndingType.Pacifist:
                        SaveManager.Instance.gameData.f_pacifistCleared = true;
                        break;
                }
                break;
            }
        }
        SaveManager.Instance.gameData.f_hasClearedOnce = true;
        LevelChanger.Instance.FadeToLevel("Ending");
    }
}
