using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float movementSpeed = 2f;
    private void FixedUpdate()
    {
        transform.localPosition += movementSpeed * Time.fixedDeltaTime * Vector3.down;
    }
}
