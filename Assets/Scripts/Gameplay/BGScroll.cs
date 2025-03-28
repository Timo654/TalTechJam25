using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float movementSpeed = 2f;
    private void FixedUpdate()
    {
        transform.localPosition += movementSpeed * Time.fixedDeltaTime * new Vector3(0, -1, -1);
    }
}
