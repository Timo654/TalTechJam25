using System;
using TMPro;
using UnityEngine;

public class HUDUpdater : MonoBehaviour
{
    private float scoreCounterSpeed = 100f;
    private TextMeshProUGUI m_scoreText;
    private TextMeshProUGUI m_timerText;
    private TextMeshProUGUI m_lifeText;
    private float visualScore;
    private int targetScore;
    private void Start()
    {
        m_scoreText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        m_timerText = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        if (SaveManager.Instance.runtimeData.gameType != GameType.Endless) transform.GetChild(2).gameObject.SetActive(false);
        else m_lifeText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (targetScore != visualScore)
        {
            visualScore = Mathf.MoveTowards(visualScore, targetScore, scoreCounterSpeed * Time.deltaTime);
            m_scoreText.text = $"{$"{Mathf.RoundToInt(visualScore):0000}"}";
        }
    }
    private void OnEnable()
    {
        ChaosCounter.UpdateScore += UpdateScoreUI;
        GameManager.OnGameTimeChanged += UpdateTimerUI;
        GameManager.OnLivesChanged += UpdateLivesUI;
    }

    private void OnDisable()
    {
        ChaosCounter.UpdateScore -= UpdateScoreUI;
        GameManager.OnGameTimeChanged -= UpdateTimerUI;
        GameManager.OnLivesChanged -= UpdateLivesUI;
    }

    private void UpdateLivesUI(int lives)
    {
        m_lifeText.text = lives.ToString();
    }

    // fancier - lerp shit
    private void UpdateTimerUI(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;
        m_timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    private void UpdateScoreUI(int score)
    {
        targetScore = score;
    }
}

