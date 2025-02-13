using UnityEngine;

public class Heart : MonoBehaviour
{
    public HeartSpawner heartSpawner; // Reference to the HeartSpawner script

    // Called when the collider is triggered
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debugging to see if the heart is touching the player
        Debug.Log("Heart triggered by: " + other.gameObject.name);

        // Check if the collider that triggered the event is the player
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            // Increase the heart count
            heartSpawner.HeartCollected();

            // Destroy the heart object after being collected
            Destroy(gameObject);
        }
    }
}
