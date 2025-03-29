using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityScript : MonoBehaviour
{
    [SerializeField] private EntityType entityType;
    [SerializeField] private ItemType itemType;
    public static event Action<EntityType, ItemType, int> EntityAttacked; // last is lane id
    private Animator animator;
    // TODO - use an event to change the speed
    public float movementSpeed = 10f;
    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.flipX = Random.value > 0.5f;
    }

    private void OnEnable()
    {
        GameManager.BoostSpeed += IncreaseSpeed;
    }
    private void OnDisable()
    {
        GameManager.BoostSpeed -= IncreaseSpeed;
    }

    private void IncreaseSpeed(float increment)
    {
        movementSpeed += increment;
    }

    public void ConfigOnSpawn(float increment) 
    {
        IncreaseSpeed(increment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        animator.SetTrigger("Destroy");
        float xPosToUse = 0f;
        int laneID = 0;
        switch (transform.localPosition.x)
        {
            case -3f:
                laneID = 0;
                break;
            case 0f:
                if (Random.value < 0.5f) xPosToUse = -3.5f;
                else xPosToUse = 4f;
                laneID = 1;
                break;
            case 3f:
                laneID = 2;
                break;
            default:
                // use 0f
                break;
        }
        EntityAttacked?.Invoke(entityType, itemType, laneID);
        transform.localPosition = new Vector3(xPosToUse, transform.localPosition.y, transform.localPosition.z);

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