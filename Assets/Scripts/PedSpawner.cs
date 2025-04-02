using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] entityPrefabs; // TODO - weighted chances
    [SerializeField] private GameObject latvia;
    private float[] randomLanes = { -3.5f, 0f, 4f };
    private float boostIncrement = 0f;
    private int playerLane = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelaySpawner());
    }

    private void OnEnable()
    {
        GameManager.BoostSpeed += IncrementBoost;
        PlayerMove.OnLaneChanged += UpdatePlayerLane;
    }

    private void OnDisable()
    {
        GameManager.BoostSpeed -= IncrementBoost;
        PlayerMove.OnLaneChanged -= UpdatePlayerLane;
    }

    private void UpdatePlayerLane(int val)
    {
        playerLane = val;
    }

    private void IncrementBoost(float increment)
    {
        boostIncrement += increment;
    }

    private IEnumerator DelaySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //Debug.Log("spawned ped");
            GameObject randomPrefab = entityPrefabs[Random.Range(0, entityPrefabs.Length)];
            if (randomPrefab.name == "scooter")
            {
                if (Random.Range(0, 100) < 5)
                {
                    randomPrefab = latvia;
                }
            }
            var gameObj = Instantiate(randomPrefab, transform); // todo weighted chances
            // Set position and rotation
            gameObj.transform.localPosition = new Vector3(
                randomLanes[Random.Range(0, randomLanes.Length)], // TODO  weighted chances
                Mathf.Abs(transform.position.y) + 25f,
                0f
            );
            gameObj.GetComponent<EntityScript>().ConfigOnSpawn(boostIncrement, playerLane);
            // Set entitys anim speed
            Animator anim = gameObj.GetComponent<Animator>();
            anim.speed += boostIncrement / 2;
            
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
