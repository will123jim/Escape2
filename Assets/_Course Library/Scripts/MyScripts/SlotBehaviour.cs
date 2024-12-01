using UnityEngine;

public class SlotBehavior : MonoBehaviour
{
    public int slotID; // Unique ID for the slot
    private Renderer _slotRenderer; // Renderer for visual highlight
    public bool isCorrect = false; // Track if the slot is correctly filled

    private void Start()
    {
        _slotRenderer = GetComponentInChildren<Renderer>();
        if (_slotRenderer == null)
        {
            Debug.LogError($"{gameObject.name} is missing a Renderer for highlighting.");
        }
    }

    public void HighlightSlot(bool highlight)
    {
        if (_slotRenderer != null)
        {
            _slotRenderer.material.color = highlight ? Color.green : Color.white; // Green for proximity
        }
    }

    public void HighlightCorrectPlacement()
    {
        if (_slotRenderer != null)
        {
            _slotRenderer.material.color = Color.blue; // Blue for correct position
        }

        isCorrect = true; // Mark slot as correctly filled
        Debug.Log($"{gameObject.name} is marked as correctly filled.");
    }

    public void ResetSlot()
    {
        if (_slotRenderer != null)
        {
            _slotRenderer.material.color = Color.white; // Reset color
        }

        isCorrect = false; // Mark slot as not filled
        Debug.Log($"{gameObject.name} is reset to not filled.");
    }
}
