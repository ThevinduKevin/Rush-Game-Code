using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text scoreText; // Reference to the Text component to display the score

    private void Start()
    {
        // Set the text component to show the total score from ScoreManager
        if (scoreText != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.TotalScore;
        }
    }
}
