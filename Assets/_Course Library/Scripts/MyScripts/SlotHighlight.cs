using UnityEngine;

public class SlotHighlight : MonoBehaviour
{
    public GameObject highlightObject; // Assign a child object that represents the visual highlight

    void Start()
    {
        // Ensure the highlight is initially hidden
        if (highlightObject != null)
        {
            highlightObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Show the highlight when a painting is nearby
        if (other.CompareTag("Painting"))
        {
            if (highlightObject != null)
            {
                highlightObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the highlight when the painting leaves
        if (other.CompareTag("Painting"))
        {
            if (highlightObject != null)
            {
                highlightObject.SetActive(false);
            }
        }
    }
}
