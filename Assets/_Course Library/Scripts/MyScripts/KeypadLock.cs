using UnityEngine;
using TMPro;
public class KeypadLock : MonoBehaviour
{
    [Header("Keypad UI")]
    public TMP_Text inputField; // Text input field to display the entered code
    public string correctCode = "6091"; // The correct code to unlock the door
    private string currentInput = ""; // holds the current input
    public AudioSource revealSound;


    [Header("Door")]
    public GameObject door; //The door to unlock

// Append the clicked number to the current input
public void AddToInput(string number)
    {
        //add the clicked number to the current input
        currentInput += number;
        inputField.text = currentInput;
    }
    public void ClearInput()
    {
        //clear the current input
        currentInput = "";
        inputField.text = currentInput;
    }
    public void SubmitCode()
    {
        //check if the entered code matches the correct code
        if (currentInput == correctCode)
        {
        UnlockDoor();
    }
    else
    {
Debug.Log("Incorrect code. Try again!");
ClearInput();
    }
}
private void UnlockDoor()
{
    //Unlock the door ( you can animate it or just disaable the lock)
    Debug.Log("Door unlocked!");
    door.SetActive(false); // Or animate the door opening
    if (revealSound != null)
        {
            revealSound.Play(); // play the sound when the puzzle is solved
        }
}
}

