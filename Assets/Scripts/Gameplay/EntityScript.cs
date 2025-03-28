using System;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    [SerializeField] private EntityType entityType;
    public static event Action<EntityType> EntityAttacked;
    private Animator animator;
    // TODO - use an event to change the speed
    public float movementSpeed = 10f;
    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggerered");
        Debug.Log(collision.gameObject.name);
        animator.SetTrigger("Destroy");
        EntityAttacked?.Invoke(entityType);
    }

    private void Update()
    {
        // not ideal but THEY DO despawn eventually...
        var vect = (transform.position - Camera.main.transform.position).normalized;
        var dot = Vector3.Dot(Camera.main.transform.forward, vect);
        if (dot < 0)
        {
            Debug.Log("off screen, destroying...");
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
    Trash
}