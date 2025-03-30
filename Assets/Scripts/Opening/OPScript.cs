using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class OPScript : MonoBehaviour
{
    [SerializeField] Sprite[] openingSprites;
    private InputAction skipButton;
    int currentSprite = 0;
    public string nextScene = "GameScene";
    bool inputBlocked = false;
    Image _nextImage;
    Image _image;
    private CustomInputActions inputActions;

    void Start()
    {
        _image = transform.GetChild(2).GetComponent<Image>();
        _image.sprite = openingSprites[currentSprite];
        _nextImage = transform.GetChild(1).GetComponent<Image>();
        LevelChanger.Instance.FadeIn();
    }
    void Update()
    {
        bool mobileTouch = false;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began) mobileTouch = true;
        }
        if ((Input.anyKeyDown || mobileTouch) & !inputBlocked)
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
