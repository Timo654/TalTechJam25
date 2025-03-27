using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] private BusType volumeType;

    private Slider volumeSlider;
    private AudioManager audioManager;
    private bool firstTime = true; // workaround for test audio playing on menu open
    private void Awake()
    {
        audioManager = AudioManager.Instance;
        volumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        switch (volumeType)
        {
            case BusType.Music:
                volumeSlider.value = SaveManager.Instance.systemData.MusicVolume / 5f * 100f;
                break;
            case BusType.SFX:
                volumeSlider.value = SaveManager.Instance.systemData.SFXVolume / 5f * 100f;
                break;
            case BusType.UI:
                volumeSlider.value = SaveManager.Instance.systemData.UIVolume / 5f * 100f;
                break;
            case BusType.Master:
                volumeSlider.value = SaveManager.Instance.systemData.MasterVolume / 5f * 100f;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        audioManager.SetBusVolume(volumeSlider.value * 5f / 100f, volumeType);
        switch (volumeType)
        {
            case BusType.Music:
                SaveManager.Instance.systemData.MusicVolume = volumeSlider.value * 5f / 100f;
                break;
            case BusType.SFX:
                SaveManager.Instance.systemData.SFXVolume = volumeSlider.value * 5f / 100f;
                audioManager.PlaySound(WWiseEvents.Instance.TestSound);
                break;
            case BusType.UI:
                SaveManager.Instance.systemData.UIVolume = volumeSlider.value * 5f / 100f;
                audioManager.PlaySound(WWiseEvents.Instance.ButtonClick); // ui test sound
                break;
            case BusType.Master:
                SaveManager.Instance.systemData.MasterVolume = volumeSlider.value * 5f / 100f;
                break;
        }     
    }
}



