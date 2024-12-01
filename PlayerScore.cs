using UnityEngine;
using UnityEngine.UI;  // Required for displaying the score on UI

public class PlayerScore : MonoBehaviour
{
    public int totalScore = 0;   // Total score the player has
    public Text scoreText;       // Reference to the UI Text component to display the score

    // This function will be called by StreetObject when it's shot
    public void AddPoints(int points)
    {
        totalScore += points;
        UpdateScoreUI();
    }

    // Update the UI text with the current score
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore.ToString();
        }
    }
}
