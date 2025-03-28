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
        // id 0 = -1, id 1 = 0 id 2 = +1, why the fuck does it not want to work with a cleaner solutionm
        switch (currentLane)
        {
            case 0:
                transform.position = new Vector2(lanes[currentLane].position.x - 1, transform.position.y);
                break;
            case 1:
                transform.position = new Vector2(lanes[currentLane].position.x, transform.position.y);
                break;
            case 2:
                transform.position = new Vector2(lanes[currentLane].position.x + 1, transform.position.y);
                break;
        }
        
    }
}
