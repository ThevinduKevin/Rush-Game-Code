using UnityEngine;

public class MouseLockManager : MonoBehaviour
{
    public bool isGameOver = false; // Flag to indicate game over state

    void Start()
    {
        LockMouse(); // Lock the mouse when the game starts
    }

    void Update()
    {
        if (isGameOver)
        {
            UnlockMouse(); // Unlock the mouse when the game ends
        }
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor
        Cursor.visible = false; // Hides the cursor
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
        Cursor.visible = true; // Makes the cursor visible
    }
}
