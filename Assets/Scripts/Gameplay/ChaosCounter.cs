using System;
using UnityEngine;

public class ChaosCounter : MonoBehaviour
{
    private uint pedsBullied = 0;
    private uint trashDestroyed = 0;

    private void OnEnable()
    {
        EntityScript.EntityAttacked += CountEntity;
    }

    private void OnDisable()
    {
        EntityScript.EntityAttacked -= CountEntity;
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
            default:
                Debug.LogWarning($"Unknown entity {type}");
                break;
        }
        //Debug.Log($"PEDS BULLIED {pedsBullied}, TRASH DESTROYED {trashDestroyed}");
    }
}
