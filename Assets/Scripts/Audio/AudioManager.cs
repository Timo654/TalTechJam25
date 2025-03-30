using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AK.Wwise.Bank[] banks;

    private void Start()
    {
        foreach (var bank in banks)
        {
            bank.Load();
        }
        SetBusVolume(SaveManager.Instance.systemData.MasterVolume, BusType.Master);
        SetBusVolume(SaveManager.Instance.systemData.SFXVolume, BusType.SFX);
        SetBusVolume(SaveManager.Instance.systemData.MusicVolume, BusType.Music);
        SetBusVolume(SaveManager.Instance.systemData.UIVolume, BusType.UI);
    }
    public void PlaySound(AK.Wwise.Event soundEvent)
    {
        PlaySound(soundEvent, gameObject);
    }

    public void PlaySound(AK.Wwise.Event soundEvent, GameObject gameObj)
    {
        soundEvent.Post(gameObj);
    }

    public void SetBusVolume(float value, BusType bus)
    {
        AkUnitySoundEngine.SetRTPCValue($"{bus}_volume" , Mathf.Clamp01(value));
    }

    public void SetRTPCValue(string param, float value)
    {
        AkUnitySoundEngine.SetRTPCValue(param, value);
    }

    public void SetSwitch(AK.Wwise.Switch switchEvent)
    {
        SetSwitch(switchEvent, gameObject);
    }

    public void SetSwitch(AK.Wwise.Switch switchEvent, GameObject gameObj)
    {
        switchEvent.SetValue(gameObj);
    }
    
    public void SetParameter(const char * , BusType bus)
    {
        AkUnitySoundEngine.SetRTPCValue($"{bus}_volume" , Mathf.Clamp01(value));
    }

}

public enum BusType
{
    Master,
    Music,
    SFX,
    UI
}