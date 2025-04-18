using System;
using UnityEngine;

public class ChaosCounter : MonoBehaviour
{
    public static event Action<EndingType, int> SendEndScore;
    public static event Action<uint> UpdateStreak;
    public static event Action<int> UpdateScore;
    private uint pedsBullied = 0;
    private uint trashDestroyed = 0;
    private uint scootersDestroyed = 0;
    private uint currentStreak;
    private int score;
    bool gameActive = false;
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
        gameActive = active;
        if (!active)
        {
            EndingType currentEnd;
            if (SaveManager.Instance.runtimeData.gameType == GameType.Endless)
                currentEnd = EndingType.Endless;
            else if (trashDestroyed == 0 && pedsBullied + scootersDestroyed >= 15)
                currentEnd = EndingType.Good;
            else if (trashDestroyed > 20)
                currentEnd = EndingType.Bad;
            else if (trashDestroyed == 0 && pedsBullied + scootersDestroyed == 0) 
                currentEnd = EndingType.Pacifist;
            else currentEnd = EndingType.Neutral;
            SendEndScore?.Invoke(currentEnd, score);
        }
    }


    private void CountEntity(EntityType type, ItemType _, int __, int ___)
    {
        if (!gameActive) return;
        switch (type)
        {
            case EntityType.Pedestrian:
                pedsBullied++;
                currentStreak++;
                score += 75;
                break;
            case EntityType.Trash:
                trashDestroyed++;
                currentStreak = 0;
                score -= 100;
                break;
            case EntityType.Scooter:
                scootersDestroyed++;
                currentStreak++;
                score += 75;
                break;
            default:
                Debug.LogWarning($"Unknown entity {type}");
                break;
        }
        score += (int)currentStreak * 13;
        UpdateStreak?.Invoke(currentStreak);
        UpdateScore?.Invoke(score);
        //Debug.Log($"PEDS BULLIED {pedsBullied}, TRASH DESTROYED {trashDestroyed}");
    }
}
