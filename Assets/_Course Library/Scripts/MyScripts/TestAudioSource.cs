using UnityEngine;

public class TestAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            Debug.Log("Playing audio for test...");
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource not found on this GameObject.");
        }
    }
}
