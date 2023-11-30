using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieSpawn : MonoBehaviour
{
    public int min = 1;
    public int max = 4;
    public float radius = 2f;  // Changed to float for consistency
    public List<GameObject> entities = new List<GameObject>();
    public LayerMask mask;
    System.Random random = new System.Random();

    void Start()
    {
        int a = random.Next(min, max);
        for (int i = 0; i < a; i++)
        {
            SpawnRandomZombie();
        }
    }

    void SpawnRandomZombie()
    {
        // Get a random position within the specified radius
        Vector3 randomPosition = transform.position + UnityEngine.Random.insideUnitSphere * radius;

        // Instantiate a random zombie from the list at the random position
        int randomIndex = random.Next(entities.Count);
        GameObject zombiePrefab = entities[randomIndex];
        Instantiate(zombiePrefab, randomPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional logic here if needed
    }
}
