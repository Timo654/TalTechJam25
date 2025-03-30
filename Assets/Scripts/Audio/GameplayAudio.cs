using UnityEngine;
using System.Collections;

public class GameplayAudio : MonoBehaviour
{
    [SerializeField] GameObject[] audioDirections; // L, M, R

    uint endineStreak = 0;

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

        AudioManager.Instance.SetRTPCValue("Drumset1Mute", 0);
        AudioManager.Instance.SetRTPCValue("Drumset2Mute", 0);
        
        EntityScript.EntityAttacked -= PlayAudio;
        ChaosCounter.UpdateStreak -= HandleStreak;
    }

    private void HandleStreak(uint currentStreak)
    {
        if (currentStreak <= 5)
        {
            if ((endineStreak != currentStreak) && (currentStreak == 0)) {
                    
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.LoseCourage);

                StartCoroutine(SoundWithDelay3(1.0f));

            } else {

                AudioManager.Instance.SetRTPCValue("Drumset1Mute", 0);
                AudioManager.Instance.SetRTPCValue("Drumset2Mute", 0);

            }
        }
        else if (currentStreak > 5 && currentStreak <= 10) 
        {   
            if (currentStreak == 6) {
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.GetCourage);
            }
            endineStreak = currentStreak;

            StartCoroutine(SoundWithDelay1(1.0f));

        } 
        else 
        {
            if (currentStreak == 11) {
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.GetCourage);
            }
            endineStreak = currentStreak;

            StartCoroutine(SoundWithDelay2(1.0f));
        }
    }

    private void PlayAudio(EntityType _, ItemType type, int laneID, int __)
    {
        //Debug.Log(type);
        switch (type)
        {
            case ItemType.Pedestrian:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.PedHit, audioDirections[laneID]);
                break;
            case ItemType.Trashcan:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.BinHit, audioDirections[laneID]);
                break;
            case ItemType.Scooter:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.ScooterHit, audioDirections[laneID]);
                break;
            case ItemType.StreetLamp:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.PostHit, audioDirections[laneID]);
                break;
            case ItemType.TrafficSign:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.TrafficSignHit, audioDirections[laneID]);
                break;
            case ItemType.Bench:
                AudioManager.Instance.PlaySound(WWiseEvents.Instance.BenchHit, audioDirections[laneID]);
                break;
        }
    }


    IEnumerator SoundWithDelay1(float delay)
    {
        yield return new WaitForSeconds(delay);

        AudioManager.Instance.SetRTPCValue("Drumset1Mute", 100);
        AudioManager.Instance.SetRTPCValue("Drumset2Mute", 0);
    }

    IEnumerator SoundWithDelay2(float delay)
    {
        yield return new WaitForSeconds(delay);

        AudioManager.Instance.SetRTPCValue("Drumset1Mute", 0);
        AudioManager.Instance.SetRTPCValue("Drumset2Mute", 100);
    }

    IEnumerator SoundWithDelay3(float delay)
    {
        yield return new WaitForSeconds(delay);

        AudioManager.Instance.SetRTPCValue("Drumset1Mute", 0);
        AudioManager.Instance.SetRTPCValue("Drumset2Mute", 0);
    }
    
}