using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pedPrefab;
    private float[] randomLanes = {-3.5f, 0f, 4f};
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
            Debug.Log(transform.position);
            // why the FUCK does this not work
            gameObj.transform.SetLocalPositionAndRotation(new Vector3(randomLanes[Random.Range(0, randomLanes.Length - 1)], Mathf.Abs(transform.position.y) + 20f, 0f), Quaternion.Euler(new Vector3(-50f, 0, 0)));

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
