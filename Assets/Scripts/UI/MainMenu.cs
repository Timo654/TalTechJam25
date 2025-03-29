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
        UICommon.FadeInCG(optionsMenuCG, 0.2f);
    }

    public void OnLeaveSettings()
    {
        UICommon.FadeOutCG(optionsMenuCG, 0.2f);
    }

    public void OnCreditsPressed()
    {
        LevelChanger.Instance.FadeToLevel("Credits");
    }
}
