using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public static event Action OnTutorialClosed;
    private CanvasGroup tutorialCG;

    private void Awake()
    {
        tutorialCG = GetComponent<CanvasGroup>();
        if (BuildConsts.isMobile)
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Switch lanes by swiping left and right.";
    }
    public void OnConfirmClicked()
    {
        tutorialCG.DOFade(0f, 0.25f).SetUpdate(true).OnComplete(() => {
            OnTutorialClosed?.Invoke();
            tutorialCG.gameObject.SetActive(false);
        });
    }
}
