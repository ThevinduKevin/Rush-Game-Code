using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class gameOverUIcontroller : MonoBehaviour
{
    private Button switchSceneButton; // Reference to the 'again' button
    private Button exitButton; // Reference to the 'exit' button

    private void Start()
    {
        // Get the root Visual Element from the UIDocument
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Find the 'again' button by its name from the UXML
        switchSceneButton = root.Q<Button>("again");

        if (switchSceneButton == null)
        {
            Debug.LogError("Button 'again' not found in the UXML.");
        }
        else
        {
            Debug.Log("Button 'again' found successfully.");
            // Attach the event handler to the 'again' button's clicked event
            switchSceneButton.clicked += OnSwitchSceneButtonClicked;
        }

        // Find the 'exit' button by its name from the UXML
        exitButton = root.Q<Button>("exit");

        if (exitButton == null)
        {
            Debug.LogError("Button 'exit' not found in the UXML.");
        }
        else
        {
            Debug.Log("Button 'exit' found successfully.");
            // Attach the event handler to the 'exit' button's clicked event
            exitButton.clicked += OnExitButtonClicked;
        }
    }

    // Event handler for the 'again' button click
    private void OnSwitchSceneButtonClicked()
    {
        Debug.Log("Switching scene to Scene 1.");
        // Load Scene0 when the 'again' button is clicked
        SceneManager.LoadScene(1);
    }

    // Event handler for the 'exit' button click
    private void OnExitButtonClicked()
    {
        Debug.Log("Exiting the game.");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #else
            Application.Quit(); // Quit the game in a build
        #endif
    }
}
