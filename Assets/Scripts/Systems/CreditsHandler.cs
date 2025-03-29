using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CreditsHandler : MonoBehaviour
{
    IDisposable listener;
    private void Awake()
    {
        listener = InputSystem.onAnyButtonPress.CallOnce(OnSkipCredits);
    }

    private void OnSkipCredits(InputControl control)
    {
        OnCreditsEnd();
    }

    private void Start()
    {
        // play credits audio
        LevelChanger.Instance.FadeIn();
    }
    public void OnCreditsEnd()
    {
        listener.Dispose();
        LevelChanger.Instance.FadeToLevel("MainMenu");
    }
}
