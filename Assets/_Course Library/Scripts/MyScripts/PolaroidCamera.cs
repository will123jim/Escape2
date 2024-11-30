using UnityEngine;

public class PolaroidCamera : MonoBehaviour
{
    [SerializeField] private Camera puzzleCamera; // Reference to the in-game camera
    [SerializeField] private RenderTexture renderTexture; // RenderTexture for displaying the camera view
    [SerializeField] private GameObject polaroidPrefab; // Prefab for the Polaroid photo
    [SerializeField] private Transform photoSpawnPoint; // Where the Polaroid will appear
    [SerializeField] private GameObject markerPrefab; // Marker prefab for debugging

    public Camera playerCamera; // Reference to the player's main camera
    public AudioSource revealSound; // Sound to play when puzzle is solved
    public float detectionRange = 10f; // Adjustable in the Inspector

    private void Start()
    {
        if (puzzleCamera != null)
        {
            puzzleCamera.targetTexture = renderTexture;
        }
        else
        {
            Debug.LogError("Puzzle camera not assigned!");
        }
    }

    public void TakePhoto()
    {
        // Render the camera view to the RenderTexture
        puzzleCamera.targetTexture = renderTexture;
        Texture2D capturedPhoto = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;

        puzzleCamera.Render();

        // Read the RenderTexture into the Texture2D
        capturedPhoto.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        capturedPhoto.Apply();

        RenderTexture.active = null;

        // Check if the camera is focused on the correct object
        if (CheckTargetInPhoto())
        {
            GeneratePolaroid(capturedPhoto);
        }
        else
        {
            Debug.Log("Incorrect target. Try again!");
        }
    }

    private bool CheckTargetInPhoto()
    {
        // Raycast from the center of the RenderTexture
        Ray ray = puzzleCamera.ScreenPointToRay(new Vector3(renderTexture.width / 2, renderTexture.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, detectionRange))
        {
            Debug.Log($"Raycast hit object: {hit.collider.name}, Position: {hit.point}");

            if (markerPrefab != null)
            {
                Instantiate(markerPrefab, hit.point, Quaternion.identity);
            }

            // Check if the hit object is the correct puzzle object
            PuzzleObject puzzleObject = hit.collider.GetComponent<PuzzleObject>();
            if (puzzleObject != null && puzzleObject.isTargetObject)
            {
                Debug.Log("Correct puzzle item detected!");
                return true;
            }
        }

        Debug.Log("Object detected, but it's not the correct target.");
        return false;
    }

    private void GeneratePolaroid(Texture2D photoTexture)
    {
        // Spawn the Polaroid at the designated location
        GameObject polaroid = Instantiate(polaroidPrefab, photoSpawnPoint.position, photoSpawnPoint.rotation);
        Renderer renderer = polaroid.GetComponentInChildren<Renderer>();

        if (renderer != null)
        {
            renderer.material.mainTexture = photoTexture; // Apply the photo texture to the Polaroid
        }

        if (revealSound != null)
        {
            revealSound.Play(); // Play the reveal sound
        }

        Debug.Log("Polaroid photo generated!");
    }

    public void DropCamera()
    {
        // Switch back to the player's main camera
        puzzleCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        Debug.Log("Switched back to player camera.");
    }
}
