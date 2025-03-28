using System;
using Unity.VisualScripting;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    [SerializeField] private EntityType entityType;
    [SerializeField] private Sprite brokenSprite;
    public static event Action<EntityType> PedAttacked;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("hit player!!!!!!!!!");
            PedAttacked?.Invoke(entityType);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggerered");
        Debug.Log(collision.gameObject.name);
        spriteRenderer.sprite = brokenSprite;
    }

    private void Update()
    {
        return;
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