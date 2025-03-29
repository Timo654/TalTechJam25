using UnityEngine;

public class RoadScroll : MonoBehaviour
{
    public float scrollSpeed = 5f; // Speed of scrolling
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
            Vector3 newPos = roadPart.transform.localPosition;
            newPos.y -= scrollSpeed * Time.fixedDeltaTime;

            // If the road part moves out of view, reposition it to the top
            if (newPos.y < -spriteHeight)
            {
                //Debug.Log($"moved {roadPart.name}, spriteHeight {spriteHeight}");
                newPos.y += spriteHeight * countPerLane; // Stack the parts vertically
            }

            roadPart.transform.localPosition = newPos;
        }
    }
}
