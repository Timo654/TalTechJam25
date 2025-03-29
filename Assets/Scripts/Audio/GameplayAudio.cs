using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    private void Start()
    {
        // start music here
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
        // TODO
    }

    private void PlayAudio(EntityType _, ItemType type)
    {
        switch (type)
        {
            case ItemType.Pedestrian:
                break;
            case ItemType.Trashcan:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.BinHit);
                break;
            case ItemType.Scooter:
                break;
            case ItemType.StreetLamp:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.PostHit);
                break;
            case ItemType.TrafficSign:
                break;
            case ItemType.Bench:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.BenchHit);
                break;
        }
    }
}