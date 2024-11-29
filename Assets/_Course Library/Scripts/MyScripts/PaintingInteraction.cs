using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaintingInteraction : MonoBehaviour
{
    public Transform defaultPosition;    // Starting position on the wall
    public float snapRadius = 0.5f;      // Radius within which the painting snaps to a slot
    public LayerMask slotLayer;          // Layer for the slots

    private Rigidbody paintingRigidbody;

    void Start()
    {
        paintingRigidbody = GetComponent<Rigidbody>();
    }

    public void OnReleased(SelectExitEventArgs args)
    {
        // Find the nearest slot within the snap radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, snapRadius, slotLayer);

        if (colliders.Length > 0)
        {
            // Snap to the first slot found
            Transform slot = colliders[0].transform;
            transform.position = slot.position;
            transform.rotation = slot.rotation;

            Debug.Log($"{gameObject.name} snapped to {slot.name}");
        }
        else
        {
            // Return to default position if no slot is nearby
            transform.position = defaultPosition.position;
            transform.rotation = defaultPosition.rotation;

            Debug.Log($"{gameObject.name} returned to default position.");
        }
    }
}

