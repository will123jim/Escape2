using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    private void Start()
    {
        AudioListener.pause = false; // Force audio to play
        Debug.Log("AudioListener unmuted in Play Mode.");
    }
}
