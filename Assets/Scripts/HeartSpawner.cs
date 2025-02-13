using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab;  // Heart prefab to spawn
    public int numberOfHearts = 5;  // Number of hearts to spawn at a time in each area
    public RectTransform[] spawnAreas; // Array of RectTransforms for multiple spawn areas
    public string WinningScene;

    private int heartsCollected = 0; // Track total number of hearts collected
    public TextMeshProUGUI heartCounterText;    // Reference to the UI Text that displays heart count

    private Vector2 spawnAreaMin;
    private Vector2 spawnAreaMax;

    void Start()
    {
        // Start spawning the hearts in each area
        SpawnHeartsInAllAreas();
        heartCounterText.text = "0";
    }

    void SpawnHeartsInAllAreas()
    {
        // Loop through each spawn area and spawn hearts in each area
        foreach (RectTransform spawnArea in spawnAreas)
        {
            // Set the spawn area limits based on the RectTransform of the current spawn area
            SetSpawnArea(spawnArea);

            // Spawn hearts at random positions within the selected spawn area
            for (int i = 0; i < numberOfHearts; i++)
            {
                float spawnPosX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                float spawnPosY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
                Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 1f); // 2D game (use Vector3 for 3D)
                GameObject heart = Instantiate(heartPrefab, spawnPosition, Quaternion.identity);

                // Dynamically add the Heart script to the heart
                Heart heartScript = heart.AddComponent<Heart>();
                heartScript.heartSpawner = this;  // Link the HeartSpawner to the Heart script

                // Ensure the heart has a collider (EdgeCollider2D in this case)
                if (heart.GetComponent<EdgeCollider2D>() == null)
                {
                    heart.AddComponent<EdgeCollider2D>();  // Add an EdgeCollider if it's missing
                }

                // Ensure the heart has a Rigidbody2D (set it to kinematic for no physics simulation)
                if (heart.GetComponent<Rigidbody2D>() == null)
                {
                    Rigidbody2D rb = heart.AddComponent<Rigidbody2D>();
                    rb.isKinematic = true;  // Make it kinematic if you don't want the physics engine to affect it
                }
            }
        }
    }

    // Set the spawn area limits based on a given RectTransform
    void SetSpawnArea(RectTransform spawnArea)
    {
        // Getting the corners of the RectTransform
        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);

        // Use the minimum and maximum x and y values from the corners
        spawnAreaMin = corners[0];  // Bottom-left corner
        spawnAreaMax = corners[2];  // Top-right corner
    }

    // Call this function when a heart is collected
    public void HeartCollected()
    {
        Debug.Log("Heart collected!");
        heartsCollected++;  // Increment the total heart counter

        // Update the heart counter text
        heartCounterText.text = heartsCollected.ToString();

        // If all hearts in all areas have been collected, respawn hearts in each area
        if (heartsCollected >= numberOfHearts * spawnAreas.Length)
        {
            SpawnHeartsInAllAreas();  // Spawn hearts again in all areas
        }

        if( heartsCollected >= 100)
        {
            SceneManager.LoadScene(WinningScene);
        }
    }
}
