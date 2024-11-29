using UnityEngine;
using TMPro;

public class PaintingPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject[] paintings; // Array of all paintings (drag your painting objects here)
    [SerializeField] private Transform[] placementSlots; // slots where paintings can be placed (empty game objects as slots)
    [SerializeField] private int[] correctOrder; //Array representing the correct order of painting indices
    private int[] currentOrder; //Tracks the current order of paintings placed in the slots
    private bool puzzleSolved = false; // flag to check if the puzzle is solved
    public AudioSource revealSound;
    public TextMeshProUGUI revealText;
    public string puzzleCompletionMessage = "9";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 void Start()
    {
        currentOrder = new int[placementSlots.Length];
        for (int i = 0; i < currentOrder.Length; i++)
        {
            currentOrder[i] = -1; // -1 represents an empty slot
        }
    }
    //call this function when a player places a painting in a slot
public void PlacePainting(int paintingIndex, int slotIndex)
{
    paintings[paintingIndex].transform.position = placementSlots[slotIndex].position;
paintings[paintingIndex].transform.rotation = placementSlots[slotIndex].rotation;

Rigidbody paintingRb = paintings[paintingIndex].GetComponent<Rigidbody>();
if (paintingRb != null)
{
    paintingRb.isKinematic = true; //Disable physics after placement
}
if (puzzleSolved) return;

//place the painting in the corresponding slot
currentOrder[slotIndex] = paintingIndex;

//check if all paintings are placed
if (AllPaintingsPlaced())
{
    Debug.Log($"CurrentOrder: {string.Join(", ", currentOrder)}");
    CheckPuzzleSolution();
}
}
//check if all paintings have been placed
private bool AllPaintingsPlaced()
{
    foreach (int painting in currentOrder)
    {
        if (painting == -1) return false; // if any slot is still empty, return false
    }
    return true;
}
// check if the current order matches the correct order
private void CheckPuzzleSolution()
{
    for (int i = 0; i < correctOrder.Length; i++)
    {
        if (currentOrder[i] != correctOrder[i])
        {
            Debug.Log("Incorrect order! puzzle is not solved");
            return; // Exit the method if any painting is in the wrong place

        }
    }
    PuzzleComplete(); // if all paintings are in the correct order, complete the puzzle
}
//called when the puzzle is solved
private void PuzzleComplete()
{
    puzzleSolved = true;
    Debug.Log("Puzzle solved! The next clue is unlocked");
     if (revealSound != null)
        {
            revealSound.Play(); // play the sound when the puzzle is solved
        }
         if (revealText != null)
        {
            revealText.text = "9";
            revealText.color = Color.green; //set the desired message
            
        }
    //Add code here to unlock the next clue or puzzle
}
public int GetPaintingIndex(GameObject painting)
    {
        for (int i = 0; i < paintings.Length; i++)
        {
            if (paintings[i] == painting)
            {
                return i; // Return the index of the painting
            }
        }

        Debug.LogError($"Painting {painting.name} not found in the paintings array.");
        return -1; // Return -1 if the painting is not found
    }
}
