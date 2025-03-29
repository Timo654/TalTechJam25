using System;
using UnityEngine;

public class ChaosCounter : MonoBehaviour
{
    public static event Action<EndingType> SendEndScore;
    public static event Action<uint> UpdateStreak;
    private uint pedsBullied = 0;
    private uint trashDestroyed = 0;
    private uint scootersDestroyed = 0;
    private uint currentStreak;
    private void OnEnable()
    {
        EntityScript.EntityAttacked += CountEntity;
        GameManager.GameActive += OnGameEnd;
    }

    private void OnDisable()
    {
        EntityScript.EntityAttacked -= CountEntity;
        GameManager.GameActive -= OnGameEnd;
    }

    private void OnGameEnd(bool active)
    {
        if (!active)
        {
            EndingType currentEnd;
            if (trashDestroyed == 0 && pedsBullied + scootersDestroyed >= 15)
            {
                currentEnd = EndingType.Good;
            }
            else if (trashDestroyed > 20)
            {
                currentEnd = EndingType.Bad;
            }
            else currentEnd = EndingType.Neutral;
            SendEndScore?.Invoke(currentEnd);
        }
    }


    private void CountEntity(EntityType type, ItemType _)
    {
        switch (type)
        {
            case EntityType.Pedestrian:
                pedsBullied++;
                currentStreak++;
                break;
            case EntityType.Trash:
                trashDestroyed++;
                currentStreak = 0;
                break;
            case EntityType.Scooter:
                scootersDestroyed++;
                currentStreak++;
                break;
            default:
                Debug.LogWarning($"Unknown entity {type}");
                break;
        }
        UpdateStreak?.Invoke(currentStreak);
        //Debug.Log($"PEDS BULLIED {pedsBullied}, TRASH DESTROYED {trashDestroyed}");
    }
}
