using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpeningScript : MonoBehaviour
{
    [SerializeField] private Image openingImage;
    //[SerializeField] EventReference openingAudio;
    private InputAction skipOpening;
    private bool hasEnded = false;
    // Start is called before the first frame update
    void Awake()
    {
        //var playerControls = new PlayerControls();
        // TODO - wtf I do with this
        //skipOpening = playerControls.UI.SkipOpening;
    }

    private void OnEnable()
    {
        skipOpening.Enable();
        skipOpening.performed += OnSkipOpening;
    }
    private void OnDisable()
    {
        skipOpening.Disable();
        skipOpening.performed += OnSkipOpening;
    }
    private void Start()
    {
        //AudioManager.Instance.PlaySound(); // opening audio
        LevelChanger.Instance.FadeIn();
        // TODO - play an animation instead and call an end function when done

        //openingImage.DOFade(1f, 20f).SetEase(Ease.InOutSine).SetUpdate(true).OnComplete(()=>LoadNextLevel());
    }

    private void OnSkipOpening(InputAction.CallbackContext context)
    {
        if (!hasEnded)
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        openingImage.DOKill();
        hasEnded = true;

        LevelChanger.Instance.FadeToLevel("Gameplay");
    }
}
