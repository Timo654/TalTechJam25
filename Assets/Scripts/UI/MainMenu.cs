using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup optionsMenuCG;
    [SerializeField] private GameObject endlessButton;
    private GameObject lastSelect;
    void Start()
    {
        endlessButton.SetActive(SaveManager.Instance.gameData.f_hasClearedOnce);
        AudioManager.Instance.PlaySound(WWiseEvents.Instance.PlaySwitcher);
        AudioManager.Instance.SetSwitch(WWiseEvents.Instance.MenuMusic);
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
        SaveManager.Instance.runtimeData.gameType = GameType.Normal;
        LevelChanger.Instance.FadeToLevel("Opening");
    }

    public void OnEndlessPressed()
    {
        SaveManager.Instance.runtimeData.gameType = GameType.Endless;
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
