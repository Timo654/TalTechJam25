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
    }
    public void PlaySound(AK.Wwise.Event soundEvent)
    {
        PlaySound(soundEvent, gameObject);
    }

    public void PlaySound(AK.Wwise.Event soundEvent, GameObject gameObj)
    {
        soundEvent.Post(gameObj);
    }

}
