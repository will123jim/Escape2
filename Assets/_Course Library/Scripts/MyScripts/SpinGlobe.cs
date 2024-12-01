using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpinGlobe : MonoBehaviour
{
    private Transform interactorTransform; // The transform of the interactor grabbing the globe
    private Vector3 lastInteractorRotation; // To track the last rotation of the interactor

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f; // Sensitivity of globe spinning

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody is properly configured
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezePosition; // Freeze position but allow rotation
        }
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        // Get the interactor's transform
        interactorTransform = args.interactorObject.transform;

        // Initialize rotation tracking
        if (interactorTransform != null)
        {
            lastInteractorRotation = interactorTransform.eulerAngles;
        }

        Debug.Log($"Globe grabbed by: {args.interactorObject.transform.name}");
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        interactorTransform = null;

        Debug.Log($"Globe released by: {args.interactorObject.transform.name}");
    }

    void Update()
    {
        if (interactorTransform != null)
        {
            // Calculate the change in rotation
            Vector3 currentInteractorRotation = interactorTransform.eulerAngles;
            Vector3 rotationDelta = currentInteractorRotation - lastInteractorRotation;

            // Apply rotation to the globe (Y-axis spin)
            transform.Rotate(Vector3.up, rotationDelta.y * rotationSpeed, Space.World);

            // Store the current rotation for the next frame
            lastInteractorRotation = currentInteractorRotation;
        }
    }
}
