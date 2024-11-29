using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpinGlobe : MonoBehaviour
{
    private bool isBeingGrabbed = false;        // Tracks if the globe is currently grabbed
    private Transform interactorTransform;     // The transform of the controller grabbing the globe
    private Vector3 lastInteractorRotation;    // Tracks the last rotation of the interactor

    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;          // Adjust for desired spin sensitivity

    void Update()
    {
        if (isBeingGrabbed && interactorTransform != null)
        {
            // Calculate the rotation delta based on the interactor's movement
            Vector3 currentInteractorRotation = interactorTransform.eulerAngles;
            Vector3 rotationDelta = currentInteractorRotation - lastInteractorRotation;

            // Apply rotation around the Y-axis (horizontal spin)
            transform.Rotate(Vector3.up, rotationDelta.y * rotationSpeed, Space.World);

            // Store the current rotation for the next frame
            lastInteractorRotation = currentInteractorRotation;
        }
    }

    // Called when the globe is grabbed
    public void OnGrab(SelectEnterEventArgs args)
    {
        isBeingGrabbed = true;
        interactorTransform = args.interactorObject.transform;

        // Initialize the last rotation to the current controller's rotation
        lastInteractorRotation = interactorTransform.eulerAngles;
    }

    // Called when the globe is released
    public void OnRelease(SelectExitEventArgs args)
    {
        isBeingGrabbed = false;
        interactorTransform = null;
    }
}

