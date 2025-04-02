using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class OPScript : MonoBehaviour
{
    [SerializeField] Sprite[] openingSprites;
    int currentSprite = 0;
    public string nextScene = "GameScene";
    bool inputBlocked = false;
    Image _nextImage;
    Image _image;
    private bool keyPressedThisFrame = false;
    IDisposable disposable;
    void Start()
    {
        _image = transform.GetChild(2).GetComponent<Image>();
        _image.sprite = openingSprites[currentSprite];
        _nextImage = transform.GetChild(1).GetComponent<Image>();
        LevelChanger.Instance.FadeIn();
        disposable = InputSystem.onAnyButtonPress
        .Call(ctrl => ButtonPressed(ctrl));
    }

    void ButtonPressed(InputControl ctrl)
    {
        keyPressedThisFrame = true;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        disposable.Dispose();
        EnhancedTouchSupport.Disable();
    }
    void Update()
    {
        bool mobileTouch = false;
        if (Touch.activeTouches.Count > 0)
        {
            if (Touch.activeTouches[0].phase == TouchPhase.Began) mobileTouch = true;
            else mobileTouch = false;
        }
        if ((keyPressedThisFrame || mobileTouch) & !inputBlocked)
        {
            currentSprite += 1;
            if (currentSprite < openingSprites.Length)
            {
                StartCoroutine(FadeSprites(openingSprites[currentSprite]));
            }
            else
            {
                OnOpeningEnd();
            }
        }
        keyPressedThisFrame = false;
    }
    public void OnOpeningEnd()
    {
        LevelChanger.Instance.FadeToLevel(nextScene);
    }

    IEnumerator FadeSprites(Sprite newSprite)
    {
        _nextImage.sprite = newSprite;
        _image.DOFade(0.0f, 1f).SetUpdate(true);
        inputBlocked = true;
        yield return new WaitForSecondsRealtime(1f);
        _image.DOKill();
        _image.sprite = newSprite;
        _image.DOFade(1.0f, 0f).SetUpdate(true);
        inputBlocked = false;
    }
}
