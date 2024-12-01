using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int TotalScore { get; private set; } // Total score

    private void Awake()
    {
        // Ensure that there is only one instance of ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure it doesn't get destroyed when changing scenes
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this one
        }
    }

    // Method to add points to the score
    public void AddScore(int points)
    {
        TotalScore += points;
    }

    // Optional: Method to reset the score (if needed)
    public void ResetScore()
    {
        TotalScore = 0;
    }
}
