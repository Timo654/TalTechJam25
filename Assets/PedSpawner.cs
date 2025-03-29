using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] entityPrefabs; // TODO - weighted chances
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
            //Debug.Log("spawned ped");
            var gameObj = Instantiate(entityPrefabs[Random.Range(0, entityPrefabs.Length)], transform); // todo weighted chances
            // Set position and rotation
            gameObj.transform.localPosition = new Vector3(
                randomLanes[Random.Range(0, randomLanes.Length)], // TODO  weighted chances
                Mathf.Abs(transform.position.y) + 25f,
                0f
            );
            gameObj.transform.localRotation = Quaternion.Euler(-50f, 0f, 0f);
            var sr = gameObj.GetComponentInChildren<SpriteRenderer>();
            sr.color = new Color(1, 1, 1, 0);
            sr.DOFade(1f, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
