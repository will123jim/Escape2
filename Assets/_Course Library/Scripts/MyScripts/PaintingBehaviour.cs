using UnityEngine;

public class PaintingBehavior : MonoBehaviour
{
    public int paintingID; // Unique ID for the painting
    private Transform currentSlot; // The slot the painting should snap to
    private bool isSnapped = false; // Track if the painting is snapped
    private Rigidbody rb; // Rigidbody for movement control

    private PaintingPuzzleManager puzzleManager; // Cached reference to the puzzle manager

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError($"{gameObject.name} is missing a Rigidbody!");
        }

        // Cache the Puzzle Manager reference
        puzzleManager = FindFirstObjectByType<PaintingPuzzleManager>();
        if (puzzleManager == null)
        {
            Debug.LogError("PaintingPuzzleManager not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slot") && !isSnapped)
        {
            SlotBehavior slot = other.GetComponent<SlotBehavior>();
            if (slot != null && slot.slotID == paintingID)
            {
                currentSlot = other.transform; // Assign the slot
                slot.HighlightCorrectPlacement(); // Highlight slot as blue
                Debug.Log($"{gameObject.name} detected correct slot: {currentSlot.name}");
            }
            else if (slot != null)
            {
                slot.HighlightSlot(true); // Highlight slot as green
                Debug.Log($"{gameObject.name} detected a slot, but it's not the correct one.");
            }
        }

        // Check puzzle state automatically
        if (puzzleManager != null)
        {
            Debug.Log("Notifying Puzzle Manager to check puzzle state.");
            puzzleManager.CheckPuzzle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slot"))
        {
            SlotBehavior slot = other.GetComponent<SlotBehavior>();
            if (slot != null)
            {
                slot.HighlightSlot(false); // Reset slot highlight
                Debug.Log($"{gameObject.name} exited slot: {other.name}");
            }

            if (currentSlot != null && other.transform == currentSlot)
            {
                currentSlot = null; // Clear the current slot reference
            }
        }

        // Check puzzle state automatically
        if (puzzleManager != null)
        {
            Debug.Log("Notifying Puzzle Manager to check puzzle state.");
            puzzleManager.CheckPuzzle();
        }
    }
}
