using UnityEngine;

public class PolaroidCamera : MonoBehaviour
{
    [SerializeField] private Camera puzzleCamera; // Reference to the in-game camera
    [SerializeField] private RenderTexture renderTexture; //RenderTexture for captuting the photo
    [SerializeField] private GameObject polaroidPrefab; // Polaroid prefab
    [SerializeField] private Transform photoSpawnPoint; //Where the polaroid will appear
    [SerializeField] private GameObject markerPrefab; //marker prefab for debugging
    public Camera playerCamera; // reference to the players main camera
    public AudioSource revealSound;

    public float detectionRange = 10f; // adjustable in the inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakePhoto()
    {
        //Render the camera view to the RenderTexture
        puzzleCamera.targetTexture = renderTexture;
        Texture2D capturedPhoto = new Texture2D (renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;

        puzzleCamera.Render();

        //Read the RenderTexture into the Texture2D
        capturedPhoto.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        capturedPhoto.Apply();

        RenderTexture.active = null;
        puzzleCamera.targetTexture = null;

        //Check if the camera is focused on the correct object
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
    Ray ray = puzzleCamera.ScreenPointToRay(new Vector3(renderTexture.width / 2, renderTexture.height / 2, 0));


    
    if (Physics.Raycast(ray, out RaycastHit hit, detectionRange))
    {
        Debug.Log($"Raycast hit object: {hit.point}, Object: {hit.collider.name}");
        if (markerPrefab != null)
        {
            GameObject markerInstance = Instantiate(markerPrefab, hit.point, Quaternion.identity);
            Debug.Log("Marker paced at position: {hit.point}");
        }
       // cjeck if the hit object has the puzzleobject component
       PuzzleObject puzzleObject = hit.collider.GetComponent<PuzzleObject>();

        if (puzzleObject != null && puzzleObject.isTargetObject)
        {
            Debug.Log("Correct puzzleitem detected");
            return true;
        }
        else
        {
        Debug.Log("object detected but tag doesnt match");
        }
    }
    
    return false;
}
private void GeneratePolaroid(Texture2D photoTexture)
{
    GameObject polaroid = Instantiate(polaroidPrefab, photoSpawnPoint.position, photoSpawnPoint.rotation);
    Renderer renderer = polaroid.GetComponentInChildren<Renderer>();
    if (renderer != null)
    {
        renderer.material.mainTexture = photoTexture;
    }
     if (revealSound != null)
        {
            revealSound.Play(); // play the sound when the puzzle is solved
        }
    Debug.Log("Polaroid photo generated!");
}
public void DropCamera()
{
    puzzleCamera.gameObject.SetActive(false);
    playerCamera.gameObject.SetActive(true);
    Debug.Log("Switched back to player camera");
}

}
