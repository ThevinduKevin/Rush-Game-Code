using UnityEngine;

public class StreetObject : MonoBehaviour
{
    public int pointValue = 10; // Points awarded for shooting the object
    public float disappearDelay = 2f; // Time before the object is fully removed
    public ParticleSystem disappearEffect; // Optional disappearance effect
    public AudioClip shotSound; // Sound to play when shot

    private Renderer objectRenderer;
    private Collider objectCollider;
    private AudioSource audioSource;
    private PlayerScore playerScore; // Reference to the PlayerScore script

    void Start()
    {
        // Cache the object's Renderer and Collider components
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        // Add an AudioSource component if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the audio source settings (adjust as needed)
        audioSource.playOnAwake = false;

        // Find the PlayerScore script (assuming it's attached to the player)
        playerScore = FindObjectOfType<PlayerScore>();
    }

    public void OnShot()
    {
        // Award points
        if (playerScore != null)
        {
            playerScore.AddPoints(pointValue); // Add points to the player's score
        }
        else
        {
            Debug.LogWarning("PlayerScore script not found.");
        }

        // Play disappearance effect if assigned
        if (disappearEffect != null)
        {
            Instantiate(disappearEffect, transform.position, Quaternion.identity);
        }

        // Play sound effect if assigned
        if (shotSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shotSound);
        }

        // Disable visibility and collider
        if (objectRenderer != null)
            objectRenderer.enabled = false;
        if (objectCollider != null)
            objectCollider.enabled = false;

        // Schedule object destruction
        Invoke(nameof(RemoveObject), disappearDelay);
    }

    private void RemoveObject()
    {
        // Fully remove the object from the scene
        Destroy(gameObject);
    }
}
