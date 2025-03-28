using UnityEngine;

public class EntityScroll : MonoBehaviour
{
    // TODO - use an event to change the speed
    public float movementSpeed = 2f;
    private void FixedUpdate()
    {
        transform.localPosition += movementSpeed * Time.fixedDeltaTime * new Vector3(0, -1, 0);
    }
}
