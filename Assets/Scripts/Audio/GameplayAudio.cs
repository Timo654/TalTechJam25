using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    [SerializeField] GameObject[] audioDirections; // L, M, R
    private void Start()
    {
        AudioManager.Instance.PlaySound(WWiseEvents.Instance.PlaySwitcher);
        AudioManager.Instance.SetSwitch(WWiseEvents.Instance.GameMusic1);
    }
    private void OnEnable()
    {
        EntityScript.EntityAttacked += PlayAudio;
        ChaosCounter.UpdateStreak += HandleStreak;
    }

    private void OnDisable()
    {
        EntityScript.EntityAttacked -= PlayAudio;
        ChaosCounter.UpdateStreak -= HandleStreak;
    }

    private void HandleStreak(uint currentStreak)
    {
        if (currentStreak <= 5)
        {

            AudioManager.Instance.SetRTPCValue("Drumset1Mute", 0);
            AudioManager.Instance.SetRTPCValue("Drumset2Mute", 0);
        }
        else if (currentStreak > 5 && currentStreak <= 10) // Use && instead of "and"
        {
            AudioManager.Instance.SetRTPCValue("Drumset1Mute", 100);
            AudioManager.Instance.SetRTPCValue("Drumset2Mute", 0);
        } 
        else 
        {
            AudioManager.Instance.SetRTPCValue("Drumset1Mute", 0);
            AudioManager.Instance.SetRTPCValue("Drumset2Mute", 100);
        }
    }

    private void PlayAudio(EntityType _, ItemType type, int laneID, int __)
    {
        //Debug.Log(type);
        switch (type)
        {
            case ItemType.Pedestrian:
                break;
            case ItemType.Trashcan:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.BinHit, audioDirections[laneID]);
                break;
            case ItemType.Scooter:
                break;
            case ItemType.StreetLamp:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.PostHit, audioDirections[laneID]);
                break;
            case ItemType.TrafficSign:
                break;
            case ItemType.Bench:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.BenchHit, audioDirections[laneID]);
                break;
        }
    }
}