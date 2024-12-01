 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreDisplay : MonoBehaviour
{
    private PlayerScore score;
    private int pScore;
    private Label scoreCount;
    

    void Start()
    {
        // Find the PlayerScore component
        score = FindObjectOfType<PlayerScore>();
        if (score == null)
        {
            Debug.LogError("PlayerScore component not found in the scene!");
            return;
        }

        // Find the UI Document and get the root visual element
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component not found on this GameObject!");
            return;
        }

        var root = uiDocument.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("Root Visual Element not found in the UIDocument!");
            return;
        }

        // Query the Label for displaying the score
        scoreCount = root.Q<Label>("playerScore");
        if (scoreCount == null)
        {
            Debug.LogError("Label 'playerScore' not found in the UI Document!");
        }
    }

    void Update()
    {
       // if (score == null || scoreCount == null) return;
   
        // Update the score in the UI
        Debug.Log("score is in ui"+ pScore);
        pScore = score.totalScore;
        scoreCount.text = "Score:" + " " + pScore.ToString();
         
    }
}
