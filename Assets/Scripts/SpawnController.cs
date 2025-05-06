using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] spritePrefabs;  // Array to hold your sprite prefab
    public float spawnDelay = 0.1f;     // Delay between spawns when holding space
    private float lastSpawnTime;        // Time when the last sprite was spawned

    void Update()
    {
        // Check if Space key is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnSprite();
        }
    }

    void SpawnSprite()
    {
        // Calculate random X position at the top of the screen
        float randomX = Random.Range(0f, 1f);  // Random X position within screen width (normalized)
       
        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(randomX, 1.1f, 10f)); // 10f is the z position
      

        // Choose a random sprite prefab from the array
        GameObject spritePrefab = spritePrefabs[Random.Range(0, spritePrefabs.Length)];

        // Instantiate the sprite at the calculated position
        GameObject newSprite = Instantiate(spritePrefab , spawnPosition, Quaternion.identity);
      

        // Optionally, you can add velocity or other properties to the spawned sprite here

        // Update last spawn time
        lastSpawnTime = Time.time;
    }
}
