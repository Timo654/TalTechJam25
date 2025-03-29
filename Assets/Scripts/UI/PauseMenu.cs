using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenuCG;
    [SerializeField] private CanvasGroup optionsMenuCG;
    [SerializeField] private GameObject firstSelectedUIElement;
    public static event Action<bool> OnPauseGame;
    private bool isPaused;
    
    private void Start()
    {
        OnPauseGame?.Invoke(false); // game start
    }

    private void OnEnable()
    {
        InputHandler.OnPauseInput += HandlePause;
    }

    private void OnDisable()
    {
        InputHandler.OnPauseInput -= HandlePause;
    }
    public void OnRestart()
    {
        Debug.Log("TODO!!");
        //LevelChanger.Instance.FadeToLevel(SceneManager.GetActiveScene().name);
    }
    public void OnQuit()
    {
        Debug.Log("TODO!!");
        //LevelChanger.Instance.FadeToLevel("MainMenu");
    }

    public void OnOptions()
    {
        UICommon.FadeInCG(optionsMenuCG, 0.2f);
    }
    public void HandlePause()
    {
        if (optionsMenuCG != null && optionsMenuCG.alpha > 0f)
        {
            UICommon.FadeOutCG(optionsMenuCG, 0.2f);
            return;
        }
        OnPauseGame?.Invoke(!isPaused);
        if (isPaused) UnpauseGame();
        else PauseGame();
    }

    private void UnpauseGame()
    {
        pauseMenuCG.DOKill();
        pauseMenuCG.blocksRaycasts = false;
        pauseMenuCG.DOFade(0f, 0.2f).SetUpdate(true).OnComplete(() => pauseMenuCG.gameObject.SetActive(false));
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void PauseGame()
    {
        if (Time.timeScale < 1.0f) return;
        Time.timeScale = 0f;
        pauseMenuCG.DOKill();
        pauseMenuCG.alpha = 0f;
        pauseMenuCG.gameObject.SetActive(true);
        pauseMenuCG.blocksRaycasts = true;
        pauseMenuCG.DOFade(1f, 0.2f).SetUpdate(true).OnComplete(() => optionsMenuCG.interactable = true);
        EventSystem.current.SetSelectedGameObject(firstSelectedUIElement);


        isPaused = true;

    }

}
