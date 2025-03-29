using System;
using UnityEngine;

public class ChaosCounter : MonoBehaviour
{
    public static event Action<EndingType> SendEndScore;
    private uint pedsBullied = 0;
    private uint trashDestroyed = 0;
    private uint scootersDestroyed = 0;

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


    private void CountEntity(EntityType type)
    {
        switch (type)
        {
            case EntityType.Pedestrian:
                pedsBullied++;
                break;
            case EntityType.Trash:
                trashDestroyed++;
                break;
            case EntityType.Scooter:
                scootersDestroyed++;
                break;
            default:
                Debug.LogWarning($"Unknown entity {type}");
                break;
        }
        //Debug.Log($"PEDS BULLIED {pedsBullied}, TRASH DESTROYED {trashDestroyed}");
    }
}
