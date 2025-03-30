using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private EndingData[] endings;
    public static event Action<bool> GameActive;
    public static event Action<int> OnGameTimeChanged;
    public static event Action<int> OnLivesChanged;
    public static event Action<float> BoostSpeed;
    private bool gameActive = false;
    private float currentTime = 60f;
    private int lastTimeValue;
    private int currentPhase = 0;
    private int lives = 3;
    private GameType gameType;
    private bool boostCooldown;
    private void OnEnable()
    {
        //LevelChanger.OnFadeInFinished += StartGame;
        GameActive += SetActive;
        ChaosCounter.SendEndScore += EndGame;
        TutorialScreen.OnTutorialClosed += StartGame;
        if (gameType == GameType.Endless)
        {
            EntityScript.EntityAttacked += VerifyHealth;
        }
    }
    private void OnDisable()
    {
        //LevelChanger.OnFadeInFinished -= StartGame;
        GameActive -= SetActive;
        ChaosCounter.SendEndScore -= EndGame;
        TutorialScreen.OnTutorialClosed -= StartGame;
        EntityScript.EntityAttacked -= VerifyHealth;
    }

    private void VerifyHealth(EntityType entityType, ItemType _, int __, int ___)
    {
        if (entityType == EntityType.Trash)
        {
            lives--;
            OnLivesChanged?.Invoke(lives);
            if (lives <= 0)
            {
                GameActive?.Invoke(false);
            }
        }
    }

    private void SetActive(bool val)
    {
        gameActive = val;
    }

    private void Awake()
    {
        if (SaveManager.Instance.runtimeData.gameType == GameType.None)
            SaveManager.Instance.runtimeData.gameType = GameType.Normal; // use normal if unspecified

        gameType = SaveManager.Instance.runtimeData.gameType;
        switch (gameType)
        {
            case GameType.Normal:
                currentTime = 60f;
                break;
            case GameType.Endless:
                currentTime = 0f;
                StartCoroutine(CooldownBoost());
                break;
        }

        Time.timeScale = 0f;
        tutorialScreen.SetActive(true);
    }

    private void Start()
    {
        // set defaults
        OnGameTimeChanged?.Invoke((int)currentTime); 
        OnLivesChanged?.Invoke(lives);
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

        switch (gameType)
        {
            case GameType.Normal:
                NormalUpdate();
                break;
            case GameType.Endless:
                EndlessUpdate();
                break;
        }
    }

    private void NormalUpdate()
    {
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

    private void EndlessUpdate()
    {
        currentTime += Time.deltaTime;
        if (lastTimeValue != (int)currentTime)
        {
            lastTimeValue = (int)currentTime;
            OnGameTimeChanged?.Invoke(lastTimeValue);
        }

        if (currentTime % 30 == 0 && !boostCooldown)
        {
            BoostSpeed?.Invoke(5f);
            currentPhase++; // enter phase 2, which is faster
            // SWITCH TO PHASE 2 THEME HERE
            AudioManager.Instance.SetSwitch(WWiseEvents.Instance.GameMusic2);
            StartCoroutine(CooldownBoost());
        }
    }

    IEnumerator CooldownBoost()
    {
        boostCooldown = true;
        yield return new WaitForSeconds(5f);
        boostCooldown = false;
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


public enum GameType
{
    None,
    Normal,
    Endless
}