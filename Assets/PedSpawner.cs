using System.Collections;
using UnityEngine;

public class PedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pedPrefab;
    private float[] randomLanes = { -3.5f, 0f, 4f };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelaySpawner());
    }

    private IEnumerator DelaySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("spawned ped");
            var gameObj = Instantiate(pedPrefab, transform);
            // Calculate the spawn position dynamically
            Vector3 spawnPosition = new Vector3(
                randomLanes[Random.Range(0, randomLanes.Length)], // Random lane
                Mathf.Abs(transform.position.y) + 20f,           // Always spawn above the top of the screen
                0f                                              // Z-axis remains the same
            );

            // Set position and rotation
            gameObj.transform.localPosition = spawnPosition; // Use the calculated spawn position
            gameObj.transform.localRotation = Quaternion.Euler(-50f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
