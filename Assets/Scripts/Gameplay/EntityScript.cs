using System;
using Unity.VisualScripting;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    [SerializeField] private EntityType entityType;
    [SerializeField] private Sprite brokenSprite;
    public static event Action<EntityType> EntityAttacked;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggerered");
        Debug.Log(collision.gameObject.name);
        spriteRenderer.sprite = brokenSprite;
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
}


public enum EntityType
{
    Pedestrian,
    Trash
}