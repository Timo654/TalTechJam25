using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform[] lanes; // left to right if you dont respect this it will explode probably
    private int currentLane = 1; // middle
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int prevLane = currentLane;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane--;
            if (currentLane < 0) currentLane = 0;
        }


        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane++;
            if (currentLane >= lanes.Length) currentLane = lanes.Length - 1;
        }
            


        if (prevLane == currentLane) return;

        transform.position = new Vector2(lanes[currentLane].position.x, transform.position.y);
    }
}
