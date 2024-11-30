using UnityEngine;

public class RecordPlayerDebug : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Assign your AudioSource in the Inspector

    // This method will be triggered by the XR Simple Interactor's Activate method
    public void OnActivate()
    {
        Debug.Log("Direct Interactor activated the record player."); // Log activation

        if (audioSource != null)
        {
            Debug.Log("AudioSource found.");

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("Audio is now playing.");
            }
            else
            {
                Debug.Log("Audio is already playing.");
            }
        }
        else
        {
            Debug.LogError("AudioSource is not assigned in the inspector.");
        }
    }
}
