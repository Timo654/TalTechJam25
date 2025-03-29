using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class OpeningScript : MonoBehaviour
{
    [SerializeField] GameObject skipText;
    private bool hasEnded = false;

    IDisposable listener;

    private void Start()
    {
        if (SaveManager.Instance.gameData.f_hasClearedOnce) // TODO - display in UI as well
        {
            listener = InputSystem.onAnyButtonPress.CallOnce(OnSkipOpening);
        }
        skipText.SetActive(SaveManager.Instance.gameData.f_hasClearedOnce);
        //AudioManager.Instance.SetSwitch(WWiseEvents.Instance.GameMusic1);
        LevelChanger.Instance.FadeIn();
    }

    private void OnSkipOpening(InputControl control)
    {
        LoadNextLevel();
    }

    void LoadNextLevel()
    {
        if (hasEnded) return;
        hasEnded = true;
        LevelChanger.Instance.FadeToLevel("Gameplay");
    }
}
