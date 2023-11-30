using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LootSpawner : MonoBehaviour
{
    public List<LootType> lootTypes = new List<LootType>() {LootType.Food};
    public int min = 1;
    public int max = 4;
    public float radius = 2f;
    public List<GameObject> food = new List<GameObject> ();
    public List<GameObject> casualClothes = new List<GameObject>();
    System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        int a = random.Next (lootTypes.Count);
        int b = random.Next(min, max);
        switch (lootTypes[a])
        {
            case LootType.Food:
                
                for (int i = 0; i <= b; i++)
                {
                    // Get a random position within the specified radius
                    Vector3 randomPosition = transform.position + UnityEngine.Random.insideUnitSphere * radius;

                    // Instantiate a random zombie from the list at the random position
                    int randomIndex = random.Next(food.Count);
                    GameObject zombiePrefab = food[randomIndex];
                    Instantiate(zombiePrefab, randomPosition, Quaternion.identity);
                }
                break;
            case LootType.Casual_Clothes:
                for (int i = 0; i <= b; i++)
                {
                    // Get a random position within the specified radius
                    Vector3 randomPosition = transform.position + UnityEngine.Random.insideUnitSphere * radius;

                    // Instantiate a random zombie from the list at the random position
                    int randomIndex = random.Next(casualClothes.Count);
                    GameObject zombiePrefab = casualClothes[randomIndex];
                    Instantiate(zombiePrefab, randomPosition, Quaternion.identity);
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum LootType
    {
        Food, Casual_Clothes
    }
}
