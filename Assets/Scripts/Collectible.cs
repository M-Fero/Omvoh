using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string letter; // This should match the letter the prefab represents
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                if (gameObject.CompareTag("Letter")) // Collect letter
                {
                    // Assuming the index of the letter prefab corresponds to the current instance
                    int instanceIndex = System.Array.IndexOf(spawnManager.letterPrefabs, gameObject);
                  //  spawnManager.CollectLetter(letter, instanceIndex); // Collect the letter
                }
                else if (gameObject.CompareTag("Bomb")) // Handle bomb collection
                {
                    Debug.Log("Bomb collected!");
                }
            }
            // Set the collectible to inactive instead of destroying it
            gameObject.SetActive(false);
        }
    }
}
