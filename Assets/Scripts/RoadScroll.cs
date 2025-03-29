using UnityEngine;

public class RoadScroll : MonoBehaviour
{
    public float scrollSpeed = 8f; // Speed of scrolling
    public GameObject[] roadParts; // Array holding road sprites
    private float spriteHeight = 32f; // Height of one road sprite, actual is 34.5f but i wanted overlap
    private int countPerLane = 3;

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
        scrollSpeed += increment;
    }

    void FixedUpdate()
    {
        foreach (GameObject roadPart in roadParts)
        {
            // Update the local position based on scroll speed

            roadPart.transform.localPosition += scrollSpeed * Time.fixedDeltaTime * new Vector3(0, -1, 0);
            // If the road part moves out of view, reposition it to the top
            if (roadPart.transform.localPosition.y < -spriteHeight)
            {
                //Debug.Log($"moved {roadPart.name}, spriteHeight {spriteHeight}");
                roadPart.transform.localPosition = new Vector3(roadPart.transform.localPosition.x, roadPart.transform.localPosition.y + spriteHeight * countPerLane, roadPart.transform.localPosition.z); // Stack the parts vertically
            }
        }
    }
}
