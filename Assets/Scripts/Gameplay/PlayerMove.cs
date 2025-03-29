using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform[] lanes; // left to right if you dont respect this it will explode probably
    private int currentLane = 1; // middle
    private void OnEnable()
    {
        InputHandler.OnMoveInput += HandleMovement;
    }

    private void OnDisable()
    {
        InputHandler.OnMoveInput -= HandleMovement;
    }

    private void HandleMovement(Vector2 vector)
    {
        if (vector.x < 0)
        {
            currentLane--;
            if (currentLane < 0) currentLane = 0;
        }
        else
        {
            currentLane++;
            if (currentLane >= lanes.Length) currentLane = lanes.Length - 1;
        }

        // id 0 = -1, id 1 = 0 id 2 = +1, why the fuck does it not want to work with a cleaner solutionm
        switch (currentLane)
        {
            case 0:
                transform.localPosition = new Vector3(lanes[currentLane].position.x - 1.5f, transform.localPosition.y, transform.localPosition.z);
                break;
            case 1:
                transform.localPosition = new Vector3(lanes[currentLane].position.x, transform.localPosition.y, transform.localPosition.z);
                break;
            case 2:
                transform.localPosition = new Vector3(lanes[currentLane].position.x + 1.5f, transform.localPosition.y, transform.localPosition.z);
                break;
        }
    }
}
