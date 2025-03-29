using DG.Tweening;
using System;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public static event Action OnTutorialClosed;
    private CanvasGroup tutorialCG;

    private void Awake()
    {
        tutorialCG = GetComponent<CanvasGroup>();
    }
    public void OnConfirmClicked()
    {
        tutorialCG.DOFade(0f, 0.25f).SetUpdate(true).OnComplete(() => {
            OnTutorialClosed?.Invoke();
            tutorialCG.gameObject.SetActive(false);
        });
    }
}
