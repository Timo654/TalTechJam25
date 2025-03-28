using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup optionsMenuCG;
    [SerializeField] private CanvasGroup creditsMenuCG;
    private GameObject lastSelect;
    void Start()
    {
        //AudioManager.Instance.PlaySound() // PLAY MENU MUSIC
        LevelChanger.Instance.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void OnPlayPressed()
    {
        LevelChanger.Instance.FadeToLevel("Gameplay");
    }

    public void OnQuitPressed()
    {
        LevelChanger.Instance.FadeToDesktop();
    }


    public void OnOptionsPressed()
    {
        // TODO - disable menu cg
        FadeInCG(optionsMenuCG, 0.2f);
    }

    public void OnLeaveSettings()
    {
        FadeOutCG(optionsMenuCG, 0.2f);
    }

    public void OnCreditsPressed()
    {
        // TODO - disable menu cg
        FadeInCG(creditsMenuCG, 0.2f);
    }

    public void OnLeaveCredits()
    {
        FadeOutCG(creditsMenuCG, 0.2f);
    }

    private bool FadeInCG(CanvasGroup canvasGroup, float duration)
    {
        if (canvasGroup.alpha > 0f) return false; // already fading in
        canvasGroup.DOKill(); // if fading out still
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1f, duration).OnComplete(() => canvasGroup.interactable = true); // maybe make it interactable mid transition idk

        return true;
    }

    private bool FadeOutCG(CanvasGroup canvasGroup, float duration)
    {
        if (canvasGroup.alpha < 1f) return false; // already fading out
        canvasGroup.DOKill(); // if fading in still
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0f, duration); // maybe make it interactable mid transition idk

        return true;
    }

}
