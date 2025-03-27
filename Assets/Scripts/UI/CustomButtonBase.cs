using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CustomButtonBase : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public ButtonType type;
    public bool disableSelectAudio;
    private bool pointerDown = false;
    private bool isSelected = false;
    private bool muteAudio = false;

    private bool toggleFirst = true; // only for toggles
    private bool isFirstBase = true;
    Selectable currentItem;
    public virtual void Awake()
    {
        if (gameObject.GetComponent<CustomButtonBase>() != this) isFirstBase = false;
        currentItem = GetComponent<Selectable>();
        if (TryGetComponent(out Button currentButton))
        {
            currentButton.onClick.AddListener(ClickSound);
        }

        if (TryGetComponent(out Toggle currentToggle))
        {
            currentToggle.onValueChanged.AddListener(ToggleSound);
        }
    }

    private void ToggleSound(bool value)
    {
        if (toggleFirst)
        {
            toggleFirst = false;
            return;
        }
        if (!isFirstBase) return;
        if (value) AudioManager.Instance.PlaySound(WWiseEvents.Instance.ButtonClick);
        else AudioManager.Instance.PlaySound(WWiseEvents.Instance.ButtonBack);
    }

    private void ClickSound()
    {
        if (!isFirstBase) return;
        if (muteAudio) return;
        switch (type)
        {
            case ButtonType.Normal:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.ButtonClick);
                break;
            case ButtonType.Back:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.ButtonBack);
                break;
        }
    }
    void OnEnable()
    {
        toggleFirst = false; // reset the value

        if (EventSystem.current.currentSelectedGameObject == gameObject) Selected();
        else Normal();
        //LevelChanger.OnGameplayLevelLoaded += MuteButton;
    }

    void OnDisable()
    {
        //LevelChanger.OnGameplayLevelLoaded -= MuteButton;
        Normal();
    }
    /*void MuteButton(LevelData levelData)
    {
        if (levelData.levelType == LevelType.Credits)
        {
            muteAudio = true;
        }
    }*/

    public virtual void Selected()
    {
        isSelected = true;
        if (pointerDown) return;
    }

    public virtual void Normal()
    {

    }

    public virtual void Pressed()
    {
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        Selected();
    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        if (pointerDown) return;
        Normal();
        if (!isFirstBase) return;
        if (!currentItem.interactable || disableSelectAudio || muteAudio) return;
        AudioManager.Instance.PlaySound(WWiseEvents.Instance.ButtonHover);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        Pressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        if (isSelected)
        {
            Selected();
        }
        else
        {
            Normal();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (pointerDown) return;
        if (EventSystem.current.currentSelectedGameObject == gameObject) return;
        if (currentItem.interactable && currentItem.navigation.mode != Navigation.Mode.None)
        {
            currentItem.Select();
            if (!isFirstBase) return;
            if (!muteAudio) AudioManager.Instance.PlaySound(WWiseEvents.Instance.ButtonHover);
        }
    }
}

public enum ButtonType
{
    Normal,
    Back
}