using UnityEngine;
using UnityEngine.UI; // Include this for using UI components
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class SpawnManager : MonoBehaviour
{

    public RectTransform canvasRectTransform;
    public GameObject[] letterPrefabs; // Array of letter prefabs (I, N, N, O, V, O)
    public GameObject bombPrefab; // Bomb prefab
    public float letterRespawnDelay = 10f; // Delay before respawning the letter if not collected
    public float bombSpawnInterval = 3f; // Interval for spawning bombs
    public float destroyDelay = 15f; // Time before destroying bombs
    public float letterDestroyDelay = 10f; // Time before destroying letters if not collected
    public int maxBombsOnScreen = 5; // Maximum number of bombs allowed on the screen
    public Image[] letterImages; // Array of Images for UI letters
    private Dictionary<string, List<int>> collectedLetters = new Dictionary<string, List<int>>();
    private GameObject activeLetter; // Reference to the currently active letter
    private List<GameObject> activeBombs = new List<GameObject>(); // List to track active bombs
    private int currentLetterIndex = 0; // Track the current letter index


    public GameObject logoPrefab;
    public float logoSpawnInterval = 5f; // Interval for spawning logos
    public float logoDestroyDelay = 10f;
    public int maxLogosOnScreen = 3;
    private List<GameObject> activeLogos = new List<GameObject>();



    void Start()
    {
        
      //  InitializeCollectedLetters();
       // StartCoroutine(SpawnManagerRoutine());
        StartCoroutine(SpawnBombsRoutine());
        StartCoroutine(SpawnLogosRoutine());



    }

    private IEnumerator SpawnBombsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(bombSpawnInterval);
            if (activeBombs.Count < maxBombsOnScreen)
            {
                SpawnBomb();
            }
        }
    }

    private void SpawnBomb()
    {
        // Generate a random position for the bomb
        float xPosition = Random.Range(0, canvasRectTransform.rect.width) - (canvasRectTransform.rect.width / 2);
        Vector3 spawnPosition = new Vector3(xPosition, canvasRectTransform.rect.height / 2, 0);

        // Instantiate the bomb prefab and set its position
        GameObject bomb = Instantiate(bombPrefab, canvasRectTransform);
        bomb.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        activeBombs.Add(bomb); // Add to the list of active bombs

        // Start a coroutine to destroy the bomb after a delay
        StartCoroutine(DestroyBombAfterDelay(bomb));
    }

    private IEnumerator DestroyBombAfterDelay(GameObject bomb)
    {
        yield return new WaitForSeconds(destroyDelay);
        if (activeBombs.Contains(bomb))
        {
            activeBombs.Remove(bomb); // Remove from the list of active bombs
            Destroy(bomb); // Destroy the bomb
        }
    }

    private IEnumerator SpawnLogosRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(logoSpawnInterval);
            if (activeLogos.Count < maxLogosOnScreen)
            {
                SpawnLogo();
            }
        }
    }

    private void SpawnLogo()
    {
        float xPosition = Random.Range(0, canvasRectTransform.rect.width) - (canvasRectTransform.rect.width / 2);
        Vector3 spawnPosition = new Vector3(xPosition, canvasRectTransform.rect.height / 2, 0);

        GameObject logo = Instantiate(logoPrefab, canvasRectTransform);
        logo.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        activeLogos.Add(logo);

        StartCoroutine(DestroyLogoAfterDelay(logo));
    }

    private IEnumerator DestroyLogoAfterDelay(GameObject logo)
    {
        yield return new WaitForSeconds(logoDestroyDelay);
        if (activeLogos.Contains(logo))
        {
            activeLogos.Remove(logo);
            Destroy(logo);
        }
    }




















    //private void InitializeCollectedLetters()
    //{
    //    foreach (var letterPrefab in letterPrefabs)
    //    {
    //        string letter = letterPrefab.GetComponent<Collectible>().letter; // Get the letter from the prefab
    //        if (!collectedLetters.ContainsKey(letter))
    //        {
    //            collectedLetters[letter] = new List<int>(); // Initialize list for each unique letter
    //        }
    //    }
    //}

    //private IEnumerator SpawnManagerRoutine()
    //{
    //    while (currentLetterIndex < letterPrefabs.Length)
    //    {
    //        SpawnLetter(currentLetterIndex);
    //        yield return new WaitForSeconds(letterRespawnDelay);
    //    }

    //    // All letters collected
    //    CompleteCollection();
    //}

    //private void SpawnLetter(int index)
    //{
    //    // Generate a random position along the top of the canvas
    //    float xPosition = Random.Range(0, canvasRectTransform.rect.width) - (canvasRectTransform.rect.width / 2);
    //    Vector3 spawnPosition = new Vector3(xPosition, canvasRectTransform.rect.height / 2, 0);

    //    // Instantiate the current letter prefab and set its position
    //    activeLetter = Instantiate(letterPrefabs[index], canvasRectTransform);
    //    activeLetter.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

    //    // Start coroutine to destroy the letter after a delay if not collected
    //    StartCoroutine(DestroyLetterAfterDelay(activeLetter));
    //}

    //public void CollectLetter(string letter, int instanceIndex)
    //{
    //    // Update the count for the collected letter instance
    //    if (collectedLetters.ContainsKey(letter))
    //    {
    //        collectedLetters[letter].Add(instanceIndex); // Add the instance index of the collected letter
    //        UpdateLetterImages(letter, instanceIndex); // Update the UI image based on collected letter instance

    //        // Check if all instances of the letter have been collected
    //        if (collectedLetters[letter].Count >= CountTotalInstances(letter))
    //        {
    //            // Move to the next letter index
    //            currentLetterIndex++;
    //            if (currentLetterIndex < letterPrefabs.Length)
    //            {
    //                // Respawn the next letter
    //                SpawnLetter(currentLetterIndex);
    //            }
    //            else
    //            {
    //                // All letters collected
    //                CompleteCollection();
    //            }
    //        }

    //        // Deactivate the active letter object instead of destroying it
    //        if (activeLetter != null)
    //        {
    //            activeLetter.SetActive(false);
    //        }
    //    }
    //}

    //private void UpdateLetterImages(string letter, int instanceIndex)
    //{
    //    // Update the UI based on the collected letter instance
    //    foreach (Image image in letterImages)
    //    {
    //        if (image.name == letter) // Assuming the image name matches the letter
    //        {
    //            Color color = image.color;
    //            // Set the alpha to 1 for the specific instance collected
    //            color.a = 1f; // Fully opaque for this instance
    //            image.color = color;
    //            break; // Exit the loop after updating the letter
    //        }
    //    }
    //}

    //private int CountTotalInstances(string letter)
    //{
    //    int count = 0;
    //    foreach (var prefab in letterPrefabs)
    //    {
    //        if (prefab.GetComponent<Collectible>().letter == letter)
    //        {
    //            count++;
    //        }
    //    }
    //    return count;
    //}

    //private void CompleteCollection()
    //{
    //    Debug.Log("All letters collected! Trigger final action.");
    //    // Add your desired action here (e.g., display a message, load the next level, etc.)
    //}


    //private IEnumerator DestroyLetterAfterDelay(GameObject letter)
    //{
    //    yield return new WaitForSeconds(letterDestroyDelay);
    //    if (letter != null && letter == activeLetter) // Only destroy if it's the currently active letter
    //    {
    //        Destroy(letter); // Destroy the letter if not collected
    //        activeLetter = null; // Clear the reference
    //    }
    //}
}
