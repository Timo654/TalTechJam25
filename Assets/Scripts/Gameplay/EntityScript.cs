using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityScript : MonoBehaviour
{
    [SerializeField] private EntityType entityType;
    [SerializeField] private ItemType itemType;
    public static event Action<EntityType, ItemType, int, int> EntityAttacked; // 3rd is lane id, 4th is lane that they get pushed to
    private Animator animator;
    private int currentPlayerLane = 1;
    // TODO - use an event to change the speed
    public float movementSpeed = 10f;
    private bool hasEnteredCollision = false;
    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.flipX = Random.value > 0.5f;
    }

    private void OnEnable()
    {
        GameManager.BoostSpeed += IncreaseSpeed;
        PlayerMove.OnLaneChanged += CheckLane;
    }
    private void OnDisable()
    {
        GameManager.BoostSpeed -= IncreaseSpeed;
        PlayerMove.OnLaneChanged -= CheckLane;
    }

    private void CheckLane(int val)
    {
        currentPlayerLane = val;
        if (hasEnteredCollision)
            PlayerAttacked();
    }

    private void IncreaseSpeed(float increment)
    {
        movementSpeed += increment;
    }

    public void ConfigOnSpawn(float boostIncrement, int playerLane)
    {
        currentPlayerLane = playerLane;
        IncreaseSpeed(boostIncrement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasEnteredCollision = true;
        PlayerAttacked();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasEnteredCollision = false;
    }

    // TODO - check for event where lane switches, then double check if lanes match
    private void PlayerAttacked()
    {
        int laneID = GetLaneID();
        if (currentPlayerLane != laneID) return;
        hasEnteredCollision = false;
        var laneData = GetForcedLane(laneID);
        animator.SetTrigger("Destroy");
        EntityAttacked?.Invoke(entityType, itemType, laneID, laneData.Item1);
        transform.localPosition = new Vector3(laneData.Item2, transform.localPosition.y, transform.localPosition.z);
    }

    private int GetLaneID()
    {
        int laneID = 0;
        switch (transform.localPosition.x)
        {
            case -3.5f:
                laneID = 0;
                break;
            case 0f:
                laneID = 1;
                break;
            case 4f:
                laneID = 2;
                break;
            default:
                // use 0f
                break;
        }
        return laneID;
    }

    private (int, float) GetForcedLane(int laneID)
    {
        int forcedLane = 1;
        float xPosToUse = 0f;
        switch (laneID)
        {
            case 0:
                forcedLane = 1;
                break;
            case 1:
                if (Random.value < 0.5f)
                {
                    xPosToUse = -3.5f;
                    forcedLane = 0;
                }
                else
                {
                    xPosToUse = 4f;
                    forcedLane = 2;
                }
                break;
            case 2:
                forcedLane = 1;
                break;
            default:
                break;
        }
        return (forcedLane, xPosToUse);
    }
    private void Update()
    {
        // not ideal but THEY DO despawn eventually...
        var vect = (transform.position - Camera.main.transform.position).normalized;
        var dot = Vector3.Dot(Camera.main.transform.forward, vect);
        if (dot < 0)
        {
            //Debug.Log("off screen, destroying...");
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.localPosition += movementSpeed * Time.fixedDeltaTime * new Vector3(0, -1, 0);
    }
}


public enum EntityType
{
    Pedestrian,
    Trash,
    Scooter
}

// similar but more precise
public enum ItemType
{
    Pedestrian,
    Trashcan,
    Scooter,
    StreetLamp,
    TrafficSign,
    Bench
}