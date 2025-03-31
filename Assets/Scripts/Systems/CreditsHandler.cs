using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsHandler : MonoBehaviour
{
    public static event Action<bool> AllowInput;
    IDisposable listener;
    private Animator creditsAnimator;
    [SerializeField] private GameObject creditsText;
    [SerializeField] private TextMeshPro funnyText;
    private void Awake()
    {
        creditsAnimator = GetComponent<Animator>();
    }
    private void OnSkipCredits(InputControl control)
    {
        OnCreditsEnd();
    }

    private void OnDisable()
    {
        PlayerMove.OnLaneChanged -= LeaveCredits;
    }

    private void LeaveCredits(int lane)
    {
        if (lane == 1) // middle
            return;
        AllowInput?.Invoke(false);
        PlayerMove.OnLaneChanged -= LeaveCredits;
        StartCoroutine(DelaySceneSwitch(lane));
    }

    IEnumerator DelaySceneSwitch(int lane)
    {
        AudioManager.Instance.PlaySound(WWiseEvents.Instance.BinHit, gameObject);
        yield return new WaitForSecondsRealtime(0.5f);
        if (lane == 0) LevelChanger.Instance.FadeToLevel("Gameplay");
        else if (lane == 2) LevelChanger.Instance.FadeToLevel("MainMenu");
    }
    private void Start()
    {
        // AudioManager.Instance.PlaySound(WWiseEvents.Instance.PlaySwitcher);
        AudioManager.Instance.SetSwitch(WWiseEvents.Instance.CreditsMusic);
        AllowInput?.Invoke(true);
        Time.timeScale = 1f;
        LevelChanger.Instance.FadeIn();
        AudioManager.Instance.SetRTPCValue("SwooshTurnOff", 0);
    }
    public void OnCreditsEnd()
    {
        creditsText.SetActive(false);
        if (SaveManager.Instance.runtimeData.previousSceneName == "MainMenu")
        {
            LevelChanger.Instance.FadeToLevel("MainMenu");
            return;
        }
        int endingCount = 0;
        if (SaveManager.Instance.gameData.f_pacifistCleared) endingCount++;
        if (SaveManager.Instance.gameData.f_goodCleared) endingCount++;
        if (SaveManager.Instance.gameData.f_neutralCleared) endingCount++;
        if (SaveManager.Instance.gameData.f_trashCleared) endingCount++;
        string endingText = string.Empty;
        if (endingCount == 4)
            endingText = "The Group congratulates you on your accomplishments.";
        else if (endingCount > 4)
            endingText = "You should not have been able to clear this many endings... Tell me your ways.";
        else if (endingCount < 4)
        {
            endingText += "You have cleared ";
            switch (endingCount)
            {
                case 0:
                    endingText += "not a single ending..?";
                    break;
                case 1:
                    endingText += "one ending out of four.";
                    break;
                case 2:
                    endingText += "two endings out of four.";
                    break;
                case 3:
                    endingText += "three endings out of four.";
                    break;
            }
        }
        funnyText.text = funnyText.text.Replace("PLACEHOLDER_TEXT", endingText);

        creditsAnimator.SetTrigger("ShowChoice");
    }

    public void OnChoiceShown()
    {
        PlayerMove.OnLaneChanged += LeaveCredits;
    }
}
