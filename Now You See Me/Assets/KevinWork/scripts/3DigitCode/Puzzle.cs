using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle : MonoBehaviour
{
    int firNum = 0;
    int secNum = 0;
    int thiNum = 0;

    public TextMeshProUGUI firDigit;
    public TextMeshProUGUI secDigit;
    public TextMeshProUGUI thiDigit;

    private void Update()
    {
        // Update the UI with the current digit values
        firDigit.text = firNum.ToString();
        secDigit.text = secNum.ToString();
        thiDigit.text = thiNum.ToString();
    }

    // Method to close the puzzle UI
    public void Close()
    {
        gameObject.SetActive(false);
    }

    // Increment and decrement methods with range checking (0-9)
    public void AddFir() { firNum = Mathf.Clamp(firNum + 1, 0, 9); }
    public void AddSec() { secNum = Mathf.Clamp(secNum + 1, 0, 9); }
    public void AddThi() { thiNum = Mathf.Clamp(thiNum + 1, 0, 9); }
    public void RemoveFir() { firNum = Mathf.Clamp(firNum - 1, 0, 9); }
    public void RemoveSec() { secNum = Mathf.Clamp(secNum - 1, 0, 9); }
    public void RemoveThi() { thiNum = Mathf.Clamp(thiNum - 1, 0, 9); }

    // Submit method to check if the password is correct
    public void Submit()
    {
        // Check if the entered digits match the correct password (3-4-8)
        if (firNum == 3 && secNum == 4 && thiNum == 8)
        {
            Debug.Log("密码正确！");
            // Add any logic here to indicate success, such as triggering an event or playing a sound
            Close();  // Close the puzzle UI
        }
        else
        {
            Debug.Log("密码错误，请重试！");
            // Provide feedback to the user for an incorrect password
        }
    }

    // Method to handle the back button (e.g., returning to the previous screen or gameplay)
    public void Back()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen
        Cursor.visible = false;  // Hide the cursor
        Close();  // Close the UI
    }
}
