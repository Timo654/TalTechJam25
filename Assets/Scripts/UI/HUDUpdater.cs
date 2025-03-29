using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class HUDUpdater : MonoBehaviour
{
    private TextMeshProUGUI m_scoreText;
    private TextMeshProUGUI m_timerText;

    private void Awake()
    {
        m_scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        m_timerText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        ChaosCounter.UpdateScore += UpdateScoreUI;
        GameManager.OnGameTimeChanged += UpdateTimerUI;
    }

    private void OnDisable()
    {
        ChaosCounter.UpdateScore -= UpdateScoreUI;
        GameManager.OnGameTimeChanged -= UpdateTimerUI;
    }

    // fancier - lerp shit
    private void UpdateTimerUI(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;
        m_timerText.text = $"Time: {minutes:D2}:{seconds:D2}";
    }

    private void UpdateScoreUI(int score)
    {
        m_scoreText.text = $"Score: {score}";
    }
}

