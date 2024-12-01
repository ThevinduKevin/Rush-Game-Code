using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Make sure to include the UI namespace

public class gameOver : MonoBehaviour
{
    public float settime = 1200f; // Time after which the game over happens
    public float gameovertime = 120f;
    public Text scoreText; // Reference to the UI Text component to display the score
    public  GameObject scoreBoard;
    private void Start()
    {
        // Initially hide the popup
        scoreBoard.SetActive(false);
        // Start the coroutine to show the popup after the set time
        StartCoroutine(ShowPopupAfterTime());

        // Ensure the cursor is visible and unlocked from the beginning if needed
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    // Coroutine that waits for the set time and then shows the Game Over UI
    private IEnumerator ShowPopupAfterTime()
    {
        // Wait for the specified time (settime)
        yield return new WaitForSeconds(settime);

        scoreBoard.SetActive(true);

        yield return new WaitForSeconds(gameovertime);

        // Load the Game Over scene (replace "GameOverScene" with your actual scene name if necessary)
        SceneManager.LoadScene("test");

        // Ensure the cursor is visible and unlocked when the Game Over scene is shown
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Optional: If you want to close the popup when a button is clicked
    public void ClosePopup()
    {

        // Optionally hide the cursor again or lock it if necessary
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Hide the cursor if desired after closing the popup
    }

    // Method to update the score text when transitioning to the Game Over scene
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            // Display the total score stored in ScoreManager
            scoreText.text = "Score: " + ScoreManager.Instance.TotalScore;
        }
    }

    // Called when the scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the score when the scene is loaded
        UpdateScoreDisplay();
    }

    // Ensure the scene loaded event is hooked up properly
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
