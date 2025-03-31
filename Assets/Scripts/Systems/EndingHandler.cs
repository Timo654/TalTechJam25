using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingHandler : MonoBehaviour
{
    [SerializeField] private EndingData debugEndingData;
    private Image endingImage;
    private TextMeshProUGUI endingText;
    private bool running = true;
    private Sprite endSprite1;
    private Sprite endSprite2;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreGratsText;
    // Start is called before the first frame update
    private void Awake()
    {
        endingImage = transform.GetChild(0).GetComponent<Image>();
        endingText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        highScoreGratsText = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        SaveManager.Instance.runtimeData.previousSceneName = SceneManager.GetActiveScene().name;
    }
    void Start()
    {
        EndingData endingData = SaveManager.Instance.runtimeData.currentEnding;
        if (endingData == null)
        {
            Debug.LogWarning("no ending found, loading debug ending...");
            endingData = debugEndingData;
        }
        endingImage.sprite = endingData.endingSprite;
        endingText.text = endingData.endingText;

        if (endingData.endingSprite2 != null)
        {
            endSprite1 = endingData.endingSprite;
            endSprite2 = endingData.endingSprite2;
            StartCoroutine(AnimateEnding());
        }
        //AudioManager.Instance.PlaySound(); // TODO - play ending audio
        scoreText.text = $"Score: {SaveManager.Instance.runtimeData.currentScore}";
        switch (SaveManager.Instance.runtimeData.highScoreType)
        {
            case HighScoreType.None:
                highScoreGratsText.text = "";
                break;
            case HighScoreType.Best:
                highScoreGratsText.text = "New best score!";
                break;
            case HighScoreType.Worst:
                highScoreGratsText.text = "New worst score!";
                break;
            case HighScoreType.First:
                highScoreGratsText.text = "Your first score!";
                break;
        }
        LevelChanger.Instance.FadeIn();
        StartCoroutine(Ending());
    }

    IEnumerator AnimateEnding()
    {
        while (running)
        {
            yield return new WaitForSeconds(0.5f);
            endingImage.sprite = endSprite2;
            yield return new WaitForSeconds(0.5f);
            endingImage.sprite = endSprite1;
        }

    }
    IEnumerator Ending()
    {
        if (SaveManager.Instance.runtimeData.gameType == GameType.Endless)
        {
            yield return new WaitForSecondsRealtime(10f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(5f); // TODO - maybe adjust ending timing or load from data
        }
        running = false;
        LevelChanger.Instance.FadeToLevel("Credits");
    }
}
